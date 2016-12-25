using System;
using System.Collections.Generic;
using System.Xml.Linq;
using STE = System.Text.Encoding;
using XpNet;
namespace Matrix.Xml.Parser
{
    /// <summary>
    /// Stream Parser is a lighweight streaming Xml parser designed for XMPP Xml streams.
    /// It may also work for other Xml streams, but its optimized for XMPP streams only.
    /// So use it on your own risk for other Xml streams.
    /// </summary>
    public class StreamParser
    {
        bool _isCData;
        int _depth;
        XmppXElement _root;
        XmppXElement _current;

        readonly STE _utf = STE.UTF8;
        readonly Encoding _utf8Encoding = new UTF8Encoding();
        readonly NamespaceStack _nsStack = new NamespaceStack();

        ByteBuffer _bufferAggregate = new ByteBuffer();


        public event Action<XmppXElement> OnStreamStart;
        public event Action<XmppXElement> OnStreamElement;
        public event Action OnStreamEnd;
        public event Action<Exception>      OnStreamError;

        /// <summary>
        /// Reset the XML Stream
        /// </summary>
        public void Reset()
        {
            _depth = 0;
            _root = null;
            _current = null;
            _isCData = false;

            _bufferAggregate = null;
            _bufferAggregate = new ByteBuffer();

            //m_buf.Clear(0);
            _nsStack.Clear();
        }

        /// <summary>
        /// Gets the _depth.
        /// </summary>
        /// <value>The _depth.</value>
        public long Depth => _depth;

        public void Write(byte[] buf, List<object> output)
        {
            Write(buf, 0, buf.Length, output);
        }

        /// <summary>
        /// Write bytes into the parser.
        /// </summary>
        /// <param name="buf">The bytes to put into the parse stream</param>
        /// <param name="offset">Offset into buf to start at</param>
        /// <param name="length">Number of bytes to write</param>
        public void Write(byte[] buf, int offset, int length)
        {
            Write(buf, offset, length, null);
        }

        /// <summary>
        /// Write bytes into the parser.
        /// </summary>
        /// <param name="buf">The bytes to put into the parse stream</param>
        /// <param name="offset">Offset into buf to start at</param>
        /// <param name="length">Number of bytes to write</param>
        /// <param name="output">The output.</param>
        /// <exception cref="System.NotImplementedException">Token type not implemented:  + tok</exception>
            public void Write(byte[] buf, int offset, int length, List<object> output)
        {
            // or assert, really, but this is a little nicer.
            if (length == 0)
                return;

            // No locking is required.  Read() won't get called again
            // until this method returns.

            // TODO: only do this copy if we have a partial token at the
            // end of parsing.
            var copy = new byte[length];
            Buffer.BlockCopy(buf, offset, copy, 0, length);
            _bufferAggregate.Write(copy);

            byte[] b = _bufferAggregate.GetBuffer();
            int off = 0;
            var ct = new ContentToken();
            try
            {
                while (off < b.Length)
                {
                    Tokens tok;
                    if (_isCData)
                        tok = _utf8Encoding.TokenizeCdataSection(b, off, b.Length, ct);
                    else
                        tok = _utf8Encoding.TokenizeContent(b, off, b.Length, ct);

                    switch (tok)
                    {
                        case Tokens.PartialToken:
                        case Tokens.PartialChar:
                        case Tokens.ExtensibleToken:
                            return;

                        case Tokens.EmptyElementNoAtts:
                        case Tokens.EmptyElementWithAtts:
                            StartTag(b, off, ct, tok, output);
                            EndTag(b, off, ct, tok, output);
                            break;
                        case Tokens.StartTagNoAtts:
                        case Tokens.StartTagWithAtts:
                            StartTag(b, off, ct, tok, output);
                            break;
                        case Tokens.EndTag:
                            EndTag(b, off, ct, tok, output);
                            break;
                        case Tokens.DataChars:
                        case Tokens.DataNewline:
                            AddText(_utf.GetString(b, off, ct.TokenEnd - off));
                            break;
                        case Tokens.CharReference:
                        case Tokens.MagicEntityReference:
                            AddText(new string(new[] { ct.RefChar1 }));
                            break;
                        case Tokens.CharPairReference:
                            AddText(new string(new[] {ct.RefChar1,
                                                            ct.RefChar2}));
                            break;
                        case Tokens.Comment:
                            if (_current != null)
                            {
                                // <!-- 4
                                //  --> 3
                                int start = off + 4 * _utf8Encoding.MinBytesPerChar;
                                int end = ct.TokenEnd - off -
                                    7 * _utf8Encoding.MinBytesPerChar;
                                string text = _utf.GetString(b, start, end);
                                _current.Add(text);
                            }
                            break;
                        case Tokens.CdataSectOpen:
                            _isCData = true;
                            break;
                        case Tokens.CdataSectClose:
                            _isCData = false;
                            break;
                        case Tokens.XmlDeclaration:
                            // thou shalt use UTF8, and XML version 1.
                            // i shall ignore evidence to the contrary...

                            // TODO: Throw an exception if these assuptions are
                            // wrong
                            break;
                        case Tokens.EntityReference:
                        case Tokens.ProcessingInstruction:

                            throw new NotImplementedException("Token type not implemented: " + tok);
                    }
                    off = ct.TokenEnd;
                }
            }
            catch (Exception ex)
            {
                //xmlStreamEvents.OnError()
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamError, ex));
                OnStreamError?.Invoke(ex);
            }
            finally
            {
                _bufferAggregate.RemoveFirst(off);
            }
        }

        private void StartTag(byte[] buf, int offset,
            ContentToken ct, Tokens tok,
            List<object> output)
        {
            _depth++;
            int colon;
            string name;
            string prefix;

            var attributes = new Dictionary<string, string>();

            _nsStack.Push();

            // if i have attributes
            if ((tok == Tokens.StartTagWithAtts) ||
                (tok == Tokens.EmptyElementWithAtts))
            {
                int start;
                int end;
                string val;
                for (int i = 0; i < ct.GetAttributeSpecifiedCount(); i++)
                {
                    start = ct.GetAttributeNameStart(i);
                    end = ct.GetAttributeNameEnd(i);
                    name = _utf.GetString(buf, start, end - start);

                    start = ct.GetAttributeValueStart(i);
                    end = ct.GetAttributeValueEnd(i);
                    //val = _utf.GetString(buf, start, end - start);

                    val = NormalizeAttributeValue(buf, start, end - start, output);
                    // <foo b='&amp;'/>
                    // <foo b='&amp;amp;'
                    // TODO: if val includes &amp;, it gets double-escaped
                    if (name.StartsWith("xmlns:"))
                    {
                        // prefixed namespace declaration
                        colon = name.IndexOf(':');
                        prefix = name.Substring(colon + 1);
                        _nsStack.AddNamespace(prefix, val);
                        attributes.Add(name, val);
                    }
                    else if (name == "xmlns")
                    {
                        // namespace declaration
                        _nsStack.AddNamespace(string.Empty, val);
                        attributes.Add(name, val);
                    }
                    else
                    {
                        // normal attribute
                        attributes.Add(name, val);
                    }
                }
            }

            name = _utf.GetString(buf,
                offset + _utf8Encoding.MinBytesPerChar,
                ct.NameEnd - offset - _utf8Encoding.MinBytesPerChar);

            colon = name.IndexOf(':');
            string ns;
            prefix = null;
            if (colon > 0)
            {
                prefix = name.Substring(0, colon);
                name = name.Substring(colon + 1);
                ns = _nsStack.LookupNamespace(prefix);
            }
            else
            {
                ns = _nsStack.DefaultNamespace;
            }

            XmppXElement newel = Factory.GetElement(prefix, name, ns);

            foreach (string attrname in attributes.Keys)
            {
                colon = attrname.IndexOf(':');
                if (colon > 0)
                {
                    prefix = attrname.Substring(0, colon);
                    name = attrname.Substring(colon + 1);
                    ns = _nsStack.LookupNamespace(prefix);
                    if (attrname.StartsWith("xmlns:"))
                    {
                        // Namespace Declaration
                        newel.SetAttributeValue(XName.Get(name, ns), attributes[attrname]);
                    }
                    else
                    {
                        // prefixed attribute
                        newel.SetAttributeValue("{" + ns + "}" + name, attributes[attrname]);
                    }
                }
                else
                {
                    newel.SetAttributeValue(XName.Get(attrname, string.Empty), attributes[attrname]);
                }
            }

            if (_root == null)
            {
                _root = newel;
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamStart, _root));
                OnStreamStart?.Invoke(_root);

            }
            else
            {
                if (_current != null)
                    _current.Add(newel);
                _current = newel;
            }
        }

        private void EndTag(byte[] buf, int offset, ContentToken ct, Tokens tok, List<object> output)
        {
            _depth--;
            _nsStack.Pop();

            if (_current == null)
            {
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamEnd));
                OnStreamEnd?.Invoke();
                return;
            }

            var parent = _current.Parent as XmppXElement;
            if (parent == null)
            {
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamElement, _current));
                OnStreamElement?.Invoke(_current);
            }
            _current = parent;
        }

        private string NormalizeAttributeValue(byte[] buf, int offset, int length,
            List<object> output)
        {
            if (length == 0)
                return string.Empty;

            string val = null;
            var buffer = new ByteBuffer();
            var copy = new byte[length];
            Buffer.BlockCopy(buf, offset, copy, 0, length);
            buffer.Write(copy);
            byte[] b = buffer.GetBuffer();
            int off = 0;
            var ct = new ContentToken();
            try
            {
                while (off < b.Length)
                {
                    //tok = m_enc.tokenizeContent(b, off, b.Length, ct);
                    Tokens tok = _utf8Encoding.TokenizeAttributeValue(b, off, b.Length, ct);

                    switch (tok)
                    {
                        case Tokens.PartialToken:
                        case Tokens.PartialChar:
                        case Tokens.ExtensibleToken:
                            return null;

                        case Tokens.AttributeValueS:
                        case Tokens.DataChars:
                        case Tokens.DataNewline:
                            val += (_utf.GetString(b, off, ct.TokenEnd - off));
                            break;
                        case Tokens.CharReference:
                        case Tokens.MagicEntityReference:
                            val += new string(new[] { ct.RefChar1 });
                            break;
                        case Tokens.CharPairReference:
                            val += new string(new[] { ct.RefChar1, ct.RefChar2 });
                            break;
                        case Tokens.EntityReference:
                            throw new NotImplementedException("Token type not implemented: " + tok);
                    }
                    off = ct.TokenEnd;
                }
            }
            catch (Exception ex)
            {
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamError, ex));
                OnStreamError?.Invoke(ex);
            }
            finally
            {
                buffer.RemoveFirst(off);
            }
            return val;
        }

        /// <summary>
        /// Add a Text or CDATA node
        /// </summary>
        /// <param name="text">value(content of the node</param>
        private void AddText(string text)
        {
            if (text == "")
                return;

            if (_isCData)
            {
                var cdata = new XCData(text);
                _current?.Add(cdata);
            }
            else
            {
                _current?.Add(text);
            }
        }
    }
}
