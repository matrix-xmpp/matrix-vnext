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
using System.IO;
using System.Linq;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
using Matrix.Xml.Parser;

namespace Matrix.Xml
{
    /// <summary>
    /// Represents an XMPP XML element.
    /// </summary>
    public class XmppXElement : XElement
    {
        #region << Constructors >>

        public XmppXElement(XNamespace ns, string tagname) : base(ns + tagname)
        {
        }

        public XmppXElement(string ns, string tagname) : this("{" + ns + "}" + tagname)
        {
        }

        public XmppXElement(string ns, string tagname, object content)
            : this("{" + ns + "}" + tagname, content)
        {
        }

        public XmppXElement(string ns, string tagname, params object[] content)
            : this("{" + ns + "}" + tagname, content)
        {
        }

        public XmppXElement(string ns, string prefix, string tagname)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
        }

        public XmppXElement(string ns, string prefix, string tagname, object content)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
            Add(content);
        }

        public XmppXElement(string ns, string prefix, string tagname, params object[] content)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
            Add(content);
        }

        public XmppXElement(XElement other) : base(other)
        {
        }

        public XmppXElement(XName name) : base(name)
        {
        }

        public XmppXElement(XName name, object content)
            : base(name, content)
        {
        }

        public XmppXElement(XName name, params object[] content)
            : base(name, content)
        {
        }

        public XmppXElement(XStreamingElement other) : base(other)
        {
        }

        #endregion

        /// <summary>
        /// Loads a XmppXElement from file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static XmppXElement LoadFile(string filename)
        {
            if (File.Exists(filename))
            {
                var xml = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(xml))
                    return LoadXml(xml);
            }
            return null;
        }

        /// <summary>
        /// Build a XmppXElement from a Xml string.
        /// </summary>
        /// <param name="xml">The Xml string.</param>
        /// <returns></returns>
        public static XmppXElement LoadXml(string xml)
        {
            return LoadXml(xml, false);
        }

        /// <summary>
        /// Build a XmppXElement from a byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static XmppXElement LoadXml(byte[] data, int index, int count)
        {
            var xml = System.Text.Encoding.UTF8.GetString(data, index, count);
            return LoadXml(xml, false);
        }

        /// <summary>
        //// Build a XmppXElement from a Xml string.
        /// </summary>
        /// <param name="xml">The Xml string.</param>
        /// <param name="removeWhitespace">if set to <c>true</c> whitespaces will be removed on parsing.</param>
        /// <returns></returns>
        public static XmppXElement LoadXml(string xml, bool removeWhitespace)
        {
            XmppXElement stanza = null;
            var sp = new StreamParser();

            sp.OnStreamStart += (el) => stanza = el;
            sp.OnStreamElement += (el) => stanza.Add(el);
            sp.OnStreamError += (ex) => { throw ex; };
            
            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            sp.Write(bytes, 0, bytes.Length);

            return stanza;
        }

        #region HasTag 

        public bool HasTag<T>() where T : XmppXElement
        {
            return Element<T>() != null;
        }

        public bool HasTag(string tagname)
        {
            return GetTag(tagname) != null;
        }

        public bool HasTag(string ns, string tagname)
        {
            return GetTag(ns, tagname) != null;
        }

        public bool HasTag(XNamespace xns, string tagname)
        {
            return GetTag(xns, tagname) != null;
        }

        #endregion

        public void RemoveTag(string tagname)
        {
            RemoveTag(Name.NamespaceName, tagname);
        }

        public void RemoveTag(XNamespace xns, string tagname)
        {
            XElement tag = GetTagXElement(xns, tagname);
            tag?.Remove();
        }

        public void RemoveTag(string ns, string tagname)
        {
            XElement tag = GetTagXElement(ns, tagname);
            tag?.Remove();
        }

        public XElement GetTagXElement(string ns, string tagname)
        {
            if (string.IsNullOrEmpty(ns))
                return Element(tagname);

            XNamespace xns = ns;
            return GetTagXElement(xns, tagname);
        }

        public XElement GetTagXElement(XNamespace xns, string tagname)
        {
            return Element(xns + tagname);
        }

        public XElement GetTagXElement(string tagname)
        {
            return GetTagXElement(Name.NamespaceName, tagname);
        }

        #region GetTag

        public string GetTag<T>() where T : XmppXElement
        {
            var t = Element<T>();
            return t?.Value;
        }

        public string GetTag(string ns, string tagname)
        {
            XNamespace xns = ns;
            return GetTag(xns, tagname);
        }

        public string GetTag(XNamespace ns, string tagname)
        {
            XElement el = GetTagXElement(ns, tagname);
            return el?.Value;
        }

        public string GetTag(string tagname)
        {
            return GetTag(Name.NamespaceName, tagname);
        }

        #endregion

        #region GetTag Jid

        public Jid GetTagJid(string tagname)
        {
            return GetTagJid(Name.Namespace, tagname);
        }

        public Jid GetTagJid(string ns, string tagname)
        {
            XNamespace xns = ns;
            return GetTagJid(xns, tagname);
        }

        public Jid GetTagJid(XNamespace xns, string tagname)
        {
            string val = GetTag(xns, tagname);
            if (val != null)
                return new Jid(val);

            return null;
        }

        #endregion

        #region GetTagDouble

        /// <summary>
        /// Get a Tag of type double (Decimal seperator = ".")
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public double GetTagDouble(string tagname)
        {
            // Parse the double always in english format ==> "." = Decimal seperator
            var nfi = new NumberFormatInfo {NumberGroupSeparator = "."};

            return GetTagDouble(tagname, nfi);
        }

        /// <summary>
        /// Get a Tag of type double with the given iFormatProvider
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="ifp"></param>
        /// <returns></returns>
        public double GetTagDouble(string tagname, IFormatProvider ifp)
        {
            var val = GetTag(tagname);
            return val != null ? Double.Parse(val, ifp) : Double.NaN;
        }

        #endregion

        #region GetTag int

        public int GetTagInt(string tagname)
        {
            string val = GetTag(tagname);

            if (val != null)
            {
                int ret;
                if (int.TryParse(val, out ret))
                    return ret;
            }
            return 0;
        }

        #endregion

        #region GetTag Long

        public long GetTagLong(string tagname)
        {
            string val = GetTag(tagname);

            if (val != null)
            {
                long ret;
                if (long.TryParse(val, out ret))
                    return ret;
            }
            return 0;
        }

        #endregion

        #region GetTag Uri
        /// <summary>
        /// Gets the value of the given tag as <seealso cref="System.Uri"/>
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public Uri GetTagUri(string tagname)
        {
            return (GetTagUri(Name.NamespaceName, tagname));
        }

        /// <summary>
        /// Gets the value of the given tag as <seealso cref="System.Uri"/>
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public Uri GetTagUri(string ns, string tagname)
        {
            if (HasTag(ns, tagname))
            {
                return new Uri(GetTag(ns, tagname));
            }

            return null;
        }
        #endregion

        #region GetTag bool

        public bool GetTagBool(string tagname)
        {
            return (GetTagBool(Name.NamespaceName, tagname));
        }

        public bool GetTagBool(string ns, string tagname)
        {
            if (HasTag(ns, tagname))
            {
                if (GetTag(ns, tagname) == "true")
                    return true;

                return false;
            }

            return false;
        }
        #endregion

        #region GetTag enum
        public object GetTagEnum<T>(string ns, string name)
        {
            string tag = GetTag(ns, name);
            if (String.IsNullOrEmpty(tag))
                return -1;
            try
            {
                return System.Enum.Parse(typeof (T), tag, true);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public T GetTagEnum<T>(string name)
        {
            return (T) GetTagEnum<T>(Name.NamespaceName, name);
        }

        public T GetTagEnumUsingNameAttrib<T>(string name) where T : struct, IConvertible

        {
            return GetTagEnumUsingNameAttrib<T>(Name.NamespaceName, name);
        }

        public T GetTagEnumUsingNameAttrib<T>(string ns, string name) where T : struct, IConvertible

        {
            string tag = GetTag(ns, name);
            if (String.IsNullOrEmpty(tag))
                return (T)((object) -1);
            
            return Enum.ParseUsingNameAttrib<T>(tag);
        }
        #endregion
        
        #region GetTag date
        internal DateTime GetTagDate(string tagname)
        {
            return GetTagDate(Name.NamespaceName, tagname);
        }

        internal DateTime GetTagDate(string ns, string tagname)
        {
            XNamespace xns = ns;
            return GetTagDate(xns, tagname);
        }

        internal DateTime GetTagDate(XNamespace ns, string tagname)
        {
            string date = GetTag(ns, tagname);
            if (date != null)
                return new DateTime(int.Parse(date.Substring(0, 4)),
                                    int.Parse(date.Substring(4, 2)),
                                    int.Parse(date.Substring(6, 2)));
            
            return DateTime.MinValue;
        }        
        #endregion
        
        #region << Fluent stuff >>
        /// <summary>
        /// Goes up one node in the tree to the parent for (fluent API).
        /// </summary>
        /// <returns></returns>
        public XmppXElement Up()
        {
            return Parent as XmppXElement;
        }

        /// <summary>
        /// Sets the text of the current node (fluent API).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public XmppXElement Text(string text)
        {
            Value = text;
            return this;
        }

        #region << AddTag Fluent >>
        /// <summary>
        /// Add a new empty childnode (fluent API).
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XmppXElement AddTag(string tagname)
        {
            return AddTag(tagname, null);
        }

        /// <summary>
        /// Add a new empty childnode as first child (fluent API).
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XmppXElement AddTagFirst(string tagname)
        {
            return AddTagFirst(tagname, null);
        }

        /// <summary>
        /// Adds a childnode (fluent API).
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XmppXElement AddTag(string tagname, string content)
        {
            return AddTag(Name.Namespace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode as the first child (fluent API).
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XmppXElement AddTagFirst(string tagname, string content)
        {
            return AddTagFirst(Name.Namespace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode (fluent API).
        /// </summary>
        /// <param name="ns">the namespace of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XmppXElement AddTag(string ns, string tagname, string content)
        {
            XNamespace nspace = ns;
            return AddTag(nspace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode as the first child (fluent API).
        /// </summary>
        /// <param name="ns">the namespace of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XmppXElement AddTagFirst(string ns, string tagname, string content)
        {
            XNamespace nspace = ns;
            return AddTagFirst(nspace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode (fluent API).
        /// </summary>
        /// <param name="ns"><see cref="XNamespace"/> of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XmppXElement AddTag(XNamespace ns, string tagname, string content)
        {
            return DoAddTag(ns, tagname, content, false);
        }

        /// <summary>
        /// Adds the tag as the first child Element (fluent API).
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        /// <param name="content">The content.</param>        
        /// <returns></returns>
        public XmppXElement AddTagFirst(XNamespace ns, string tagname, string content)
        {
            return DoAddTag(ns, tagname, content, true);
        }
        
        internal XmppXElement DoAddTag(XNamespace ns, string tagname, string content, bool addFirst)
        {
            var newChild = new XmppXElement(ns, tagname);
            if (addFirst)
                AddFirst(newChild);
            else
                Add(newChild);
            
            if (content != null)
                newChild.Value = content;
            
            return newChild;
        }
        #endregion
        
        #endregion

        #region SetTag Date
        internal void SetTag(string ns, string tagname, DateTime date)
        {
            XNamespace xns = ns;
            SetTag(xns, tagname, date.ToString("yyyyMMdd")); 
        }

        internal void SetTag(string tagname, DateTime date)
        {
            SetTag(Name.Namespace, tagname, date);
        }

        internal void SetTag(XNamespace ns, string tagname, DateTime date)
        {
            SetTag(ns, tagname, date.ToString("yyyyMMdd"));
        }
        #endregion

        #region << SetTag & SetTagFirst string >>
        /// <summary>
        /// Add a new empty childnode
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XElement SetTag(string tagname)
        {
            return SetTag(tagname, null);
        }

        /// <summary>
        /// Add a new empty childnode as first child.
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XElement SetTagFirst(string tagname)
        {
            return SetTagFirst(tagname, null);
        }

        /// <summary>
        /// Adds a childnode
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XElement SetTag(string tagname, string content)
        {
            return SetTag(Name.Namespace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode as the first child.
        /// </summary>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new child node</returns>
        /// <remarks>the new element will use the namespace of its parent</remarks>
        public XElement SetTagFirst(string tagname, string content)
        {
            return SetTagFirst(Name.Namespace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode
        /// </summary>
        /// <param name="ns">the namespace of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XElement SetTag(string ns, string tagname, string content)
        {
            XNamespace nspace = ns;
            return SetTag(nspace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode as the first child
        /// </summary>
        /// <param name="ns">the namespace of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XElement SetTagFirst(string ns, string tagname, string content)
        {
            XNamespace nspace = ns;
            return SetTagFirst(nspace, tagname, content);
        }

        /// <summary>
        /// Adds a childnode
        /// </summary>
        /// <param name="ns"><see cref="XNamespace"/> of the new child node</param>
        /// <param name="tagname">the tagname</param>
        /// <param name="content">the value of the new tag</param>
        /// <returns>the new childnode</returns>
        public XElement SetTag(XNamespace ns, string tagname, string content)
        {
            return DoSetTag(ns, tagname, content, false);
        }       

        /// <summary>
        /// Adds the tag as the first child Element.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        /// <param name="content">The content.</param>        
        /// <returns></returns>
        public XElement SetTagFirst(XNamespace ns, string tagname, string content)
        {
            return DoSetTag(ns, tagname, content, true);
        }

        internal XElement DoSetTag(XNamespace ns, string tagname, string content, bool addFirst)
        {
            // check if tag already exists
            XElement newChild = GetTagXElement(ns.NamespaceName, tagname);

            if (newChild == null)
            {
                newChild = new XmppXElement(ns, tagname);
                if (addFirst)
                    AddFirst(newChild);
                else
                    Add(newChild);
            }

            if (content != null)
                newChild.Value = content;


            return newChild;
        }
        #endregion

        #region SetTag Uri
        /// <summary>
        /// Sets or creates the given tag and sets the value to the uri
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public XElement SetTagUri(string tagname, Uri uri)
        {
            return SetTag(Name.NamespaceName, tagname, uri.ToString());
        }

        /// <summary>
        /// /// Sets or creates the given tag and sets the value to the uri
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="tagname"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public XElement SetTagUri(string ns, string tagname, Uri uri)
        {
            return SetTag(ns, tagname, uri.ToString());
        }
        #endregion

        #region SetTag bool
        public XElement SetTag(string tagname, bool content)
        {
            //return SetTag("this.Name.Namespace.NamespaceName", tagname, content);
            return SetTag(Name.NamespaceName, tagname, content);
        }

        public XElement SetTag(string ns, string tagname, bool content)
        {
            return SetTag(ns, tagname, content ? "true" : "false");
        }
        #endregion

        #region SetTag integer
        public XElement SetTag(string ns, string tagname, int content)
        {
            return SetTag(ns, tagname, content.ToString());
        }

        public XElement SetTag(string tagname, int content)
        {
            return SetTag(tagname, content.ToString());
        }
        #endregion

        #region SetTag double
        public void SetTag(string tagname, double dbl, IFormatProvider ifp)
        {
            SetTag(tagname, dbl.ToString(ifp));
        }

        public void SetTag(string tagname, double dbl)
        {
            var nfi = new NumberFormatInfo {NumberGroupSeparator = "."};
            SetTag(tagname, dbl, nfi);
        }
        #endregion

        public virtual string ToString(bool indented)
        {
            return ToString(indented ? SaveOptions.None : SaveOptions.DisableFormatting);
        }
      
        /// <summary>
        /// Returns a collection of the descendant elements for this document or element
        /// in document order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Descendants<T>() where T : XmppXElement
        {
            return base.Descendants(Factory.GetXName<T>()).OfType<T>();          
        }

        /// <summary>
        /// Checks if we have Descendants of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasDescendants<T>() where T : XmppXElement
        {
            var descendants =base.Descendants(Factory.GetXName<T>()).OfType<T>();
            return descendants != null && descendants.Any();
        }

        /// <summary>
        /// Returns a filtered collection of the child elements of this element or document, in document order.
        /// Only elements of the matching Type are included in the collection.
        /// </summary>
        /// <typeparam name="T">XElement type to match</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Elements<T>() where T : XElement
        {
            return Nodes().OfType<T>();
        }

        /*
         * do we need this? I don't think so.
        public IEnumerable<T> Elements<T>(bool ignoreDepth) where T : XElement
        {
            if (ignoreDepth == false)
                return Elements<T>();

            IEnumerable<T> ret = Elements<T>();

            if (ret == null || !ret.Any())
            {
                if (HasElements)
                {
                    foreach (XmppXElement el in Elements<XmppXElement>())
                    {
                        ret = el.Elements<T>();
                        if (ret != null && ret.Any())
                            break;
                        
                        if (el.HasElements)
                            ret = el.Elements<T>(true);

                        if (ret != null && ret.Any())
                            break;
                    }        
                }
            }

            return ret;
        }
        */

        /// <summary>
        /// Gets the first (in document order) child element with the specified Type. This member
        /// searches only the direct children of this element. Grandchildren all ignored.
        /// </summary>
        /// <typeparam name="T">XElement Type to match</typeparam>
        /// <returns></returns>
        public T Element<T>() where T : XElement
        {
            if (Nodes().OfType<T>().Any())
                return Nodes().OfType<T>().First();
            
            return null;
        }

        /// <summary>
        /// Gets the first (in document order) child element with the specified Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ignoreDepth">When true the search is recurive and looks for the given type in the complete Dom of this element.</param>
        /// <returns></returns>
        public T Element<T>(bool ignoreDepth) where T : XmppXElement
        {
            if (ignoreDepth == false)
                return Element<T>();

            T ret = null;

            foreach (XmppXElement el in Elements<XmppXElement>())
            {
                if (el is T)
                {
                    ret = el as T;
                    break;
                }

                ret = el.Element<T>(true);
                if (ret != null)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Replace XElement of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newel"></param>
        public void Replace<T>(T newel) where T : XElement
        {
            Replace(newel, false);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newel">The newel.</param>
        /// <param name="addFirst">if set to <c>true</c> adds the element as first child element.</param>
        public void Replace<T>(T newel, bool addFirst) where T : XElement
        {
            XElement el = Element<T>();
            el?.Remove();

            if (newel != null)
            {
                if (addFirst)
                    AddFirst(newel);
                else
                    Add(newel);
            }
        }

        /// <summary>
        /// the first child element.
        /// </summary>
        /// <returns></returns>
        public XElement FirstElement => Elements().FirstOrDefault();

        /// <summary>
        /// returns the first XmppXElement
        /// </summary>
        public XmppXElement FirstXmppXElement
        {
            get
            {
                if (HasElements)
                {
                    var el = Elements().FirstOrDefault();
                    if (el is XmppXElement)
                        return el as XmppXElement;
                }
                return null;
            }
        }

        /// <summary>
        /// Removes all elements of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveAll<T>() where T : XmppXElement
        {
            Elements<T>().Remove();
        }

        #region << Attribute functions >>

        #region << Get Attribute functions >>
        /// <summary>
        /// Removes a attribute
        /// </summary>
        /// <param name="att">attrubute to remove</param>
        public void RemoveAttribute(string att)
        {
            SetAttributeValue(att, null);
        }

        /// <summary>
        /// Removes a attribute.
        /// </summary>
        /// <param name="xname">The xname.</param>
        public void RemoveAttribute(XName xname)
        {
            SetAttributeValue(xname, null);
        }

        /// <summary>
        /// check if an attribute exists
        /// </summary>
        /// <param name="attname">attribute name to lookup</param>
        /// <returns></returns>
        public bool HasAttribute(string attname)
        {
            return Attribute(attname) != null;
        }

        /// <summary>
        /// checks if an attribute exists for the given namespace
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="attname"></param>
        /// <returns></returns>
        public bool HasAttribute(string ns, string attname)
        {
            if (string.IsNullOrEmpty(ns))
                return HasAttribute(attname);
            
            XNamespace xNamespace = ns;
            return HasAttribute(xNamespace + attname);
        }

        public bool HasAttribute(XName xname)
        {
            return Attribute(xname) != null;            
        }
                       
       
        /// <summary>
        /// Get a attribute as string. Returns 0 if the attribute does not exist.
        /// </summary>
        /// <param name="attname">attribute name to lookup</param>
        /// <returns></returns>
        public string GetAttribute(string attname)
        {
            XName xname = attname;
            return GetAttribute(xname);       
        }

        public string GetAttribute(XName xname)
        {
            if (HasAttribute(xname))
                return Attribute(xname).Value;
            
            return null;
        }

        /// <summary>
        /// Get a "namespaced" attribute
        /// </summary>
        /// <param name="ns">namespace of the attribute</param>
        /// <param name="attname">attributename</param>
        /// <returns></returns>
        public string GetAttribute(string ns, string attname)
        {   
            XName xname;
            if (ns != null)
                xname = "{" + ns + "}" + attname;
            else
                xname = attname;

            return GetAttribute(xname);
        }

        /// <summary>
        /// Get a integer attribute. Returns 0 if the attribute does not exist.
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public int GetAttributeInt(string name)
        {
            if (HasAttribute(name))
                return int.Parse(GetAttribute(name));
            
            return 0;
        }

        /// <summary>
        /// Get a long attribute. Returns 0 if the attribute does not exist.
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public long GetAttributeLong(string name)
        {
            if (HasAttribute(name))
                return long.Parse(GetAttribute(name));
            
            return 0;
        }

        /// <summary>
        /// /// Gets an attribute and convert it to a Uri.
        /// Returns null when the attribute does not exit
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Uri GetAttributeUri(string name)
        {
            if (HasAttribute(name))
                return new Uri(GetAttribute(name));

            return null;
        }

        /// <summary>
        /// Gets an attribute and convert it to a Uri.
        /// Returns null when the attribute does not exit
        /// </summary>
        /// <param name="xname"></param>
        /// <returns></returns>
        public Uri GetAttributeUri(XName xname)
        {
            if (HasAttribute(xname))
            {
                return new Uri(GetAttribute(xname));                  
            }

            return null;
        }

        /// <summary>
        /// Gets an attribute and convert it to a Uri.
        /// Returns null when the attribute does not exit
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Uri GetAttributeUri(string ns, string name)
        {
            return GetAttributeUri("{" + ns + "}" + name);
        }

        /// <summary>
        /// Get a boolean attribute. Returns false if the attribute does not exist.
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public bool GetAttributeBool(string name)
        {
            if (HasAttribute(name))
            {
                string val = GetAttribute(name);
                if (val == "true" || val == "1")
                    return true;
                
                return false;
            }
            
            return false;
        }

        public bool GetAttributeBool(XName xname)
        {
            if (HasAttribute(xname))
            {
                if (GetAttribute(xname) == "true")
                    return true;
                
                return false;
            }
            
            return false;
        }

        public bool GetAttributeBool(string ns, string name)
        {
            return GetAttributeBool("{" + ns + "}" + name);           
        }

        /// <summary>
        /// Get a Jid attribute. Returns null if the attribute does not exist.
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public Jid GetAttributeJid(string name)
        {
            return HasAttribute(name) ? new Jid(GetAttribute(name)) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <param name="ifp"></param>
        /// <returns></returns>
        public double GetAttributeDouble(string name, IFormatProvider ifp)
        {
            if (HasAttribute(name))
            {
                try
                {
                    return double.Parse(GetAttribute(name), ifp);
                }
                catch
                {
                    return double.NaN;
                }
            }
            
            return double.NaN;
        }

        /// <summary>
        /// Gets attribute that was set as B64
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] GetAttributeBase64(string name)
        {
            return Convert.FromBase64String(GetAttribute(name));
        }

        /// <summary>
        /// Get a Attribute of type double (Decimal seperator = ".")
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public double GetAttributeDouble(string name)
        {
            // Parse the double always in english format ==> "." = Decimal seperator
            var nfi = new NumberFormatInfo {NumberGroupSeparator = "."};
            return GetAttributeDouble(name, nfi);
        }

        public DateTime GetAttributeIso8601Date(string name)
        {
            return Time.Iso8601Date(GetAttribute(name));
        }        

        #region GetAttributeEnum
        /// <summary>
        /// Get a Attribute of type enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        /// <remarks>
        /// returns -1 if the enumueration value was not found in the enum, or the attribute does not exist
        /// </remarks>
        public T GetAttributeEnum<T>(string name)
        {
            return GetAttributeEnum<T>(null, name);
        }

        public T GetAttributeEnum<T>(string ns, string name)
        {
            string att = GetAttribute(ns, name);
            if ((att == null))
                return (T)((object) -1);
            try
            {
                return (T) System.Enum.Parse(typeof(T), att, true);                
            }
            catch (Exception)
            {
                return (T)((object)-1);
            }
        }

        public T GetAttributeEnumUsingNameAttrib<T>(string name) where T : struct, IConvertible
        {
            return GetAttributeEnumUsingNameAttrib<T>(null, name);
        }

        public T GetAttributeEnumUsingNameAttrib<T>(string ns, string name) where T : struct, IConvertible
        {
            string att = GetAttribute(ns, name);
            if ((att == null))
                return (T)((object)-1);

            return Enum.ParseUsingNameAttrib<T>(att);
        }
        #endregion

        public IPAddress GetAttributeIPAddress(string name)
        {
            IPAddress ip;
            IPAddress.TryParse(GetAttribute(name), out ip);
            return ip;
        }
        #endregion

        #region << SetAttribute functions >>
        public XmppXElement SetAttribute(string attname, string val)
        {
            SetAttributeValue(attname, val);
            return this;
        }

        /// <summary>
        /// Sets an attribute form an integer"
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value as integer</param>
        public XmppXElement SetAttribute(string name, int value)
        {
            SetAttribute(name, value.ToString());
            return this;
        }

        /// <summary>
        /// Sets an attribute from a Uri
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public XmppXElement SetAttribute(string name, Uri value)
        {
            SetAttribute(name, value.ToString());
            return this;
        }

        /// <summary>
        /// Sets a "long" attribute
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value as long</param>
        public XmppXElement SetAttribute(string name, long value)
        {
            SetAttribute(name, value.ToString());
            return this;
        }

        /// <summary>
        /// Sets a "boolean" attribute, the value is either 'true' or 'false'
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="val">attribute value as boolean</param>
        public XmppXElement SetAttribute(string name, bool val)
        {            
            SetAttribute(name, val ? "true" : "false");
            return this;
        }

        /// <summary>
        /// Set a attribute of type Jid
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="jid">value of the attribute, or null to remove the attribute</param>
        public XmppXElement SetAttribute(string name, Jid jid)
        {
            if (jid != null)
                SetAttribute(name, jid.ToString());
            else
                RemoveAttribute(name);

            return this;
        }

        /// <summary>
        /// Set a "double" attribute with english number format
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value of the attribute as double</param>
        public XmppXElement SetAttribute(string name, double value)
        {
            var nfi = new NumberFormatInfo {NumberGroupSeparator = "."};
            SetAttribute(name, value, nfi);
            return this;
        }

        /// <summary>
        /// Set a "double" attribute with the given FormatProvider
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value of the attribute as double</param>
        /// <param name="ifp">IFormatProvider</param>
        public XmppXElement SetAttribute(string name, double value, IFormatProvider ifp)
        {
            SetAttribute(name, value.ToString(ifp));
            return this;
        }

        /// <summary>
        /// Sets Attribute as base64 string
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public XmppXElement SetAttributeBase64(string name, byte[] value)
        {
            SetAttribute(name, Convert.ToBase64String(value));
            return this;
        }

        public XmppXElement SetAttributeIso8601Date(string name, DateTime dt)
        {
            SetAttribute(name, Time.Iso8601Date(dt));
            return this;
        }
            
        #endregion

        #endregion

        #region << build start and end tag only >>

        /// <summary>
        /// Get only the start Tag of the element
        /// </summary>
        /// <remarks>
        /// The start tag of the following xml:
        /// <code>
        /// &lt;message from=&#39;user@server.org&#39; type=&#39;chat&#39;&gt;&lt;body&gt;Hello World&lt;/body&gt;&lt;/message&gt;
        /// </code>
        /// is
        /// <code>
        /// &lt;message from=&#39;user@server.org&#39; type=&#39;chat&#39;&gt;
        /// </code>
        /// </remarks>
        /// <returns></returns>
        public string StartTag()
        {
            var el = new XmppXElement(this);
            el.Nodes().Remove();
            
            return el.ToString(false).Replace("/>", ">");
        }

        /// <summary>
        /// Get only the end tag of the element
        /// </summary>
        /// <remarks>
        /// The end tag of the following xml:
        /// <code>
        /// &lt;message from=&#39;user@server.org&#39; type=&#39;chat&#39;&gt;&lt;body&gt;Hello World&lt;/body&gt;&lt;/message&gt;
        /// </code>
        /// is
        /// <code>
        /// &lt;/message&gt;
        /// </code>
        /// </remarks>
        /// <returns></returns>
        public string EndTag()
        {
            var el = new XmppXElement(this);
            
            string ret = el.ToString(false);
            int spacePos = ret.IndexOf(' ');

            return "</" + ret.Substring(1, spacePos - 1) + ">";           
        }

        #endregion


        /// <summary>
        /// Adds or removes the given empty tag.
        /// </summary>
        /// <param name="tagname">The tagname.</param>
        /// <param name="add">if set to <c>true</c> add, otherwise remove.</param>
        public void AddOrRemoveTag(string tagname, bool add)
        {
            AddOrRemoveTag(tagname, add, false);
        }

        public void AddFirstOrRemoveTag(string tagname, bool add)
        {
            AddOrRemoveTag(tagname, add, true);
        }

        /// <summary>
        /// Adds or removes the given empty tag.
        /// </summary>
        /// <param name="tagname">The tagname.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        /// <param name="addFirst">if set to <c>true</c> [add first].</param>
        internal void AddOrRemoveTag(string tagname, bool add, bool addFirst)
        {
            if (add)
            {
                if (addFirst)
                    SetTagFirst(tagname);
                else
                    SetTag(tagname);
            }
            else
            {
                RemoveTag(tagname);
            }
        }


        /// <summary>
        /// Gets or sets the XML language.
        /// </summary>
        /// <value>The XML language.</value>
        public string XmlLanguage
        {
            set
            {
                if (value != null)
                    SetAttributeValue(XNamespace.Xml + "lang", value);
                else
                    RemoveAttribute(XNamespace.Xml + "lang");
            }
            get
            {
                return GetAttribute(XNamespace.Xml + "lang");
            }
        }

        #region << internal helper functions for handling namespaces and prefixed attributes >>
        public void AddNameSpaceDeclaration(string prefix, string value)
        {
            SetAttributeValue(XNamespace.Xmlns + prefix, value);
        }

        internal void SetPrefixedAttribute(string prefix, string name, string value)
        {
            var ns = GetNamespaceOfPrefix(prefix);
            if (ns == null)
                throw new ArgumentException("Namespace for given prefix not found");

            SetAttributeValue(ns + name, value);
        }

        /// <summary>
        /// Adds a namespaced attribute (prefix:name="value")
        /// </summary>
        /// <param name="ns">The namespace.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        internal void SetNamespacedAttribute(string ns, string name, string value)
        {
            SetAttributeValue("{" + ns + "}" + name, value);
        }

        private static readonly string[] XmppRootNamespacesStrings = {
                " xmlns=\"jabber:client\"",
                " xmlns=\"jabber:server\"",
                " xmlns=\"jabber:component:accept\""
            };

        /// <summary>
        /// this gets used as a hack to remove the namespace declarations on the stanzas
        /// when the transport is socket.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        internal static string RemoveNamespaceDeclaration(string xml)
        {
            foreach (var ns in XmppRootNamespacesStrings)
            {
                int posStart = xml.IndexOf(ns, StringComparison.Ordinal);
                if (posStart < 0)
                    continue;
                
                xml = xml.Remove(posStart, ns.Length);
                // a valid XMPP stanzas has only one namespace declaration
                // so after one was found and removed return
                break; 
            }
            return xml;
        }
        #endregion
    }
}
