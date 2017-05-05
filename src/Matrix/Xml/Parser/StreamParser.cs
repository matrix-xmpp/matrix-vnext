/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using STE = System.Text.Encoding;
using Matrix.XpNet;
namespace Matrix.Xml.Parser
{
    /// <summary>
    /// Stream Parser is a lighweight streaming Xml parser designed for XMPP Xml streams.
    /// It may also work for other Xml streams, but its optimized for XMPP streams only.
    /// So use it on your own risk for other Xml streams.
    /// </summary>
    public class StreamParser
    {
        bool isCData;
        string cData;

        int depth = 0;
        XmppXElement root;
        XmppXElement current;

        readonly STE utf = STE.UTF8;
        readonly Encoding utf8Encoding = new UTF8Encoding();
        readonly NamespaceStack nsStack = new NamespaceStack();

        ByteBuffer bufferAggregate = new ByteBuffer();


        public event Action<XmppXElement>   OnStreamStart;
        public event Action<XmppXElement>   OnStreamElement;
        public event Action                 OnStreamEnd;
        public event Action<Exception>      OnStreamError;

        /// <summary>
        /// Reset the XML Stream
        /// </summary>
        public void Reset()
        {
            depth = 0;
            root = null;
            current = null;
            isCData = false;

            bufferAggregate = null;
            bufferAggregate = new ByteBuffer();

            nsStack.Clear();
        }

        /// <summary>
        /// Gets the _depth.
        /// </summary>
        /// <value>The _depth.</value>
        public long Depth => depth;

        public void Write(byte[] buf)
        {
            Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// Write bytes into the parser.
        /// </summary>
        /// <param name="buf">The bytes to put into the parse stream</param>
        /// <param name="offset">Offset into buf to start at</param>
        /// <param name="length">Number of bytes to write</param>
        /// <exception cref="System.NotImplementedException">Token type not implemented:  + tok</exception>
        public void Write(byte[] buf, int offset, int length)
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
            bufferAggregate.Write(copy);

            byte[] b = bufferAggregate.GetBuffer();
            int off = 0;
            var ct = new ContentToken();
            try
            {
                while (off < b.Length)
                {
                    Tokens tok;
                    if (isCData)
                        tok = utf8Encoding.TokenizeCdataSection(b, off, b.Length, ct);
                    else
                        tok = utf8Encoding.TokenizeContent(b, off, b.Length, ct);

                    switch (tok)
                    {
                        case Tokens.PartialToken:
                        case Tokens.PartialChar:
                        case Tokens.ExtensibleToken:
                            return;

                        case Tokens.EmptyElementNoAtts:
                        case Tokens.EmptyElementWithAtts:
                            StartTag(b, off, ct, tok);
                            EndTag(b, off, ct, tok);
                            break;
                        case Tokens.StartTagNoAtts:
                        case Tokens.StartTagWithAtts:
                            StartTag(b, off, ct, tok);
                            break;
                        case Tokens.EndTag:
                            EndTag(b, off, ct, tok);
                            break;
                        case Tokens.DataChars:
                        case Tokens.DataNewline:
                            AddText(utf.GetString(b, off, ct.TokenEnd - off));
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
                            if (current != null)
                            {
                                // <!-- 4
                                //  --> 3
                                int start = off + 4 * utf8Encoding.MinBytesPerChar;
                                int end = ct.TokenEnd - off -
                                    7 * utf8Encoding.MinBytesPerChar;
                                string text = utf.GetString(b, start, end);
                                current.Add(text);
                            }
                            break;
                        case Tokens.CdataSectOpen:
                            isCData = true;
                            break;
                        case Tokens.CdataSectClose:
                            CloseCDataSection();
                            isCData = false;
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
                OnStreamError?.Invoke(ex);
            }
            finally
            {
                bufferAggregate.RemoveFirst(off);
            }
        }

        private void StartTag(byte[] buf, int offset,
            ContentToken ct, Tokens tok)
        {
            depth++;
            int colon;
            string name;
            string prefix;

            var attributes = new Dictionary<string, string>();

            nsStack.Push();

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
                    name = utf.GetString(buf, start, end - start);

                    start = ct.GetAttributeValueStart(i);
                    end = ct.GetAttributeValueEnd(i);
                    //val = _utf.GetString(buf, start, end - start);

                    val = NormalizeAttributeValue(buf, start, end - start);
                    // <foo b='&amp;'/>
                    // <foo b='&amp;amp;'
                    // TODO: if val includes &amp;, it gets double-escaped
                    if (name.StartsWith("xmlns:"))
                    {
                        // prefixed namespace declaration
                        colon = name.IndexOf(':');
                        prefix = name.Substring(colon + 1);
                        nsStack.AddNamespace(prefix, val);
                        attributes.Add(name, val);
                    }
                    else if (name == "xmlns")
                    {
                        // namespace declaration
                        nsStack.AddNamespace(string.Empty, val);
                        attributes.Add(name, val);
                    }
                    else
                    {
                        // normal attribute
                        attributes.Add(name, val);
                    }
                }
            }

            name = utf.GetString(buf,
                offset + utf8Encoding.MinBytesPerChar,
                ct.NameEnd - offset - utf8Encoding.MinBytesPerChar);

            colon = name.IndexOf(':');
            string ns;
            prefix = null;
            if (colon > 0)
            {
                prefix = name.Substring(0, colon);
                name = name.Substring(colon + 1);
                ns = nsStack.LookupNamespace(prefix);
            }
            else
            {
                ns = nsStack.DefaultNamespace;
            }

            XmppXElement newel = Factory.GetElement(prefix, name, ns);

            foreach (string attrname in attributes.Keys)
            {
                colon = attrname.IndexOf(':');
                if (colon > 0)
                {
                    prefix = attrname.Substring(0, colon);
                    name = attrname.Substring(colon + 1);
                    ns = nsStack.LookupNamespace(prefix);
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

            if (root == null)
            {
                root = newel;
                OnStreamStart?.Invoke(root);
            }
            else
            {
                current?.Add(newel);
                current = newel;
            }
        }

        private void EndTag(byte[] buf, int offset, ContentToken ct, Tokens tok)
        {
            // TODO we don't validate Xml right now
            // could check here if end tag name equals the start tag name
            depth--;
            nsStack.Pop();

            if (current == null)
            {
                OnStreamEnd?.Invoke();
                return;
            }

            var parent = current.Parent as XmppXElement;
            if (parent == null)
            {
                OnStreamElement?.Invoke(current);
            }
            current = parent;
        }

        private string NormalizeAttributeValue(byte[] buf, int offset, int length)
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
                    Tokens tok = utf8Encoding.TokenizeAttributeValue(b, off, b.Length, ct);

                    switch (tok)
                    {
                        case Tokens.PartialToken:
                        case Tokens.PartialChar:
                        case Tokens.ExtensibleToken:
                            return null;

                        case Tokens.AttributeValueS:
                        case Tokens.DataChars:
                        case Tokens.DataNewline:
                            val += (utf.GetString(b, off, ct.TokenEnd - off));
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
                OnStreamError?.Invoke(ex);
            }
            finally
            {
                buffer.RemoveFirst(off);
            }
            return val;
        }

        private void CloseCDataSection()
        {               
            var cdataNode = new XCData(cData);
            if (current == null)
                root?.Add(cdataNode);
            else
                current.Add(cdataNode);

            cData = string.Empty;
        }

        /// <summary>
        /// Add a Text or CDATA node
        /// </summary>
        /// <param name="text">value(content of the node</param>
        private void AddText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            if (isCData)
            {
                cData += text;                
            }
            else
            {
                if (current == null)
                    root?.Add(text);
                else
                    current?.Add(text);
            }
        }
    }
}
