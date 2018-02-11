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
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Matrix.Attributes;

namespace Matrix.Xml
{
    /// <summary>
    /// Factory for registering XmppXElement types
    /// </summary>
    public static class Factory
    {
        static Factory()
        {
            RegisterElementsFromAssembly(typeof(Factory).GetTypeInfo().Assembly);
        }

        static readonly Dictionary<string, Type> FactoryTable = new Dictionary<string, Type>();

        /// <summary>
        /// Builds the key for looking up.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="localName">Name of the local.</param>
        /// <returns></returns>
        private static string BuildKey(string ns, string localName)
        {
            return "{" + ns + "}" + localName;
        }

        /// <summary>
        /// Creates an instance of an XmppXElement of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetElement<T>() where T : XmppXElement
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Gets the <see cref="XName"/> of a given <see cref="XmppXElement"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static XName GetXName<T>() where T : XmppXElement
        {           
            var att = typeof(T)
                         .GetTypeInfo()
                         .GetCustomAttributes<XmppTagAttribute>(false)
                             .FirstOrDefault();

           return "{" + att.Namespace + "}" + att.Name;
        }

        public static XmppXElement GetElement(string prefix, string localName, string ns)
        {
            Type t = null;
            string key = BuildKey(ns, localName);
            lock (FactoryTable)
            {
                if (FactoryTable.ContainsKey(key))
                    t = FactoryTable[key];
            }

            XmppXElement ret;
            if (t != null)
            {
                /*
                 * unity webplayer does not support Activator.CreateInstance,
                 * but can create types with compiled lambdas instead.             
                 */
                ret = (XmppXElement)Activator.CreateInstance(t);
            }
            else
                ret = !string.IsNullOrEmpty(ns) ? new XmppXElement(ns, localName) : new XmppXElement(localName);
                             
            return ret;
        }

        #region << register methods >>
        public static void RegisterElement<T>(string localName) where T : XmppXElement
        {
            RegisterElement<T>("", localName);
        }

        /// <summary>
        /// Adds new Element Types to the Hashtable
        /// Use this function also to register your own created Elements.
        /// If a element is already registered it gets overwritten. This behaviour is also useful if you you want to overwrite
        /// classes and add your own derived classes to the factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ns"></param>
        /// <param name="localName"></param>
        public static void RegisterElement<T>(string ns, string localName) where T : XmppXElement
        {
            RegisterElement(typeof(T), ns, localName);           
        }

        /// <summary>
        /// Adds new Element Types to the Hashtable
        /// Use this function also to register your own created Elements.
        /// If a element is already registered it gets overwritten. This behaviour is also useful if you you want to overwrite
        /// classes and add your own derived classes to the factory.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ns"></param>
        /// <param name="localName"></param>
        private static void RegisterElement(Type type, string ns, string localName)
        {
            string key = BuildKey(ns, localName);

            // added thread safety on a user request
            lock (FactoryTable)
            {
                if (FactoryTable.ContainsKey(key))
                    FactoryTable[key] = type;
                else
                    FactoryTable.Add(key, type);
            }
        }
        #endregion

        #region << register over attributes >>
        /// <summary>
        /// Looks in a complete assembly for all XmppXElements and registered them using the XmppTag attribute.
        /// The XmppTag attribute must be present on the classes to register
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterElementsFromAssembly(Assembly assembly)
        {
            var types = GetTypesWithAttribueFromAssembly<XmppTagAttribute>(assembly);

            foreach (var type in types)
                RegisterElement(type.AsType());
        }

        private static void RegisterElement(Type type)
        {
            type
                .GetTypeInfo()
                .GetCustomAttributes<XmppTagAttribute>(false)
                .ToList()
                .ForEach(att =>
                    RegisterElement(type, att.Namespace, att.Name)
                );
        }

        /// <summary>
        /// Registers the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterElement<T>() where T : XmppXElement
        {
            var t = typeof(T).GetTypeInfo();
            var att = t.GetCustomAttributes(typeof(XmppTagAttribute), false).FirstOrDefault() as XmppTagAttribute;

            if (att != null)
                RegisterElement<T>(att.Namespace, att.Name);
        }

        private static IEnumerable<TypeInfo> GetTypesWithAttribueFromAssembly<TAttribute>(Assembly assembly) where TAttribute : Attribute
        {
            return assembly
                .DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(XmppXElement)))
                .Where(t => t.IsDefined(typeof(TAttribute), false));
        }
        #endregion
    }
}
