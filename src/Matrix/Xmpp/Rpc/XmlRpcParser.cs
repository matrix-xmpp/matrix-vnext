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
using System.Globalization;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    public class XmlRpcParser
    {
        /// <summary>
        /// parse the response
        /// </summary>
        /// <returns></returns>
        public static Parameters ParseParams(XmppXElement el)
        {
            var al = new Parameters();

            // If an error occurred, the server will return fault
            var fault = el.Element<Fault>();
            if (fault != null)
            {
                var structParameter = ParseStruct(fault.Element<Struct>(true));
                al.Add(new XmlRpcException((Int16)structParameter["faultCode"], (string)structParameter["faultString"]));
            }
            else
            {
                XmppXElement elParams = el.Element<Params>();
                var nl = elParams.Elements<Param>();

                foreach (var p in nl)
                {
                    var value = p.Element<Value>();
                    if (value != null)
                        al.Add(ParseValue(value));
                }
            }
            return al;
        }

        /// <summary>
        /// parse a response struct
        /// </summary>
        /// <param name="el">The el.</param>
        /// <returns></returns>
        private static StructParameter ParseStruct(XmppXElement el)
        {
            #region Xml sample
            //<struct>
            //   <member>
            //      <name>x</name>
            //      <value><i4>20</i4></value>
            //   </member>
            //   <member>
            //      <name>y</name>
            //      <value><string>cow</string></value>
            //   </member>
            //   <member>
            //      <name>z</name>
            //      <value><double>3,14</double></value>
            //   </member>
            //</struct>
            #endregion

            var structParameter = new StructParameter();

            var members = el.Elements<Member>();

            foreach (var member in members)
            {
                var xElementName = member.Element<Name>();
                if (xElementName == null) continue;
                
                var name = xElementName.Value;

                // parse this member value
                var xElementValue = member.Element<Value>();
                if (xElementValue != null)
                    structParameter.Add(name, ParseValue(xElementValue));
            }
            return structParameter;
        }

        /// <summary>
        /// Parse a single response value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static object ParseValue(XmppXElement value)
        {
            object result = null;

            if (value != null)
            {
                if (value.HasElements)
                {
                    var next = value.FirstXmppXElement;
                    string tagName = next.Name.LocalName;
                    if (tagName == "string")
                        result = next.Value;
                    else if (tagName == "boolean")
                        result = next.Value == "1";
                    else if (tagName == "i4")
                        result = Int32.Parse(next.Value);
                    else if (tagName == "int") // occurs in fault
                        result = Int16.Parse(next.Value);
                    else if (tagName == "double")
                    {
                        var numberInfo = new NumberFormatInfo {NumberDecimalSeparator = "."};
                        result = Double.Parse(next.Value, numberInfo);
                    }
                    else if (tagName == "dateTime.iso8601")
                        result = Matrix.Time.JabberDate(next.Value);
                    else if (tagName == "base64")
                        result = Convert.FromBase64String(next.Value);
                    else if (tagName == "struct")
                        result = ParseStruct(next);
                    else if (tagName == "array")
                        result = ParseArray(next);
                }
                else
                {
                    result = value.Value;
                }

            }
            return result;
        }

        /// <summary>
        /// parse a response array
        /// </summary>
        /// <param name="elArray">The el array.</param>
        /// <returns></returns>
        private static Parameters ParseArray(XmppXElement elArray)
        {
            #region Xml sample
            //<array>
            //    <data>
            //        <value><string>one</string></value>
            //        <value><string>two</string></value>
            //        <value><string>three</string></value>
            //        <value><string>four</string></value>
            //        <value><string>five</string></value>
            //    </data>
            //</array>
            #endregion

            var data = elArray.Element<Data>();
            if (data != null)
            {
                var list = new Parameters();
                var values = data.Elements<Value>();

                foreach (var el in values)
                    list.Add(ParseValue(el));
                
                return list;
            }
            return null;
        }
    }
}
