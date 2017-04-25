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

using System.Collections.Generic;

namespace Matrix.Xml.Parser
{
    /// <summary>
    /// Namespace stack which takes care of Xml namespaces and namespace inheritance.
    /// </summary>
    public class NamespaceStack
    {
        private readonly Stack<Dictionary<string, string>> stack = new Stack<Dictionary<string, string>>();

        /// <summary>
        /// Create a new stack.
        /// </summary>
        public NamespaceStack()
        {
            Init();
        }

        /// <summary>
        /// Initializes the Namespace stack and adds the xml default namespaces.
        /// </summary>
        private void Init()
        {
            Push();
            AddNamespace("xml",     "http://www.w3.org/XML/1998/namespace");
            AddNamespace("xmlns",   "http://www.w3.org/2000/xmlns/");
        }

        /// <summary>
        /// Declare a new namespace.
        /// This should be called at the start of each xml element.
        /// </summary>
        public void Push()
        {
            stack.Push(new Dictionary<string, string>());
        }

        /// <summary>
        /// Remove the current namespace from the stack.
        /// This should be called at the end of each xml element.
        /// </summary>
        public void Pop()
        {
            stack.Pop();
        }

        /// <summary>
        /// Add a namespace to the current level.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="uri"></param>
        public void AddNamespace(string prefix, string uri)
        {
            stack.Peek().Add(prefix, uri);
        }

        /// <summary>
        /// Find a namespace by prefix. Goes up all levels for namespace inheritance.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string LookupNamespace(string prefix)
        {
            foreach (Dictionary<string, string> dictionary in stack)
            {
                if ((dictionary.Count > 0) && (dictionary.ContainsKey(prefix)))
                    return dictionary[prefix];
            }
            return string.Empty;
        }

        /// <summary>
        /// The current default namespace.
        /// </summary>
        public string DefaultNamespace => LookupNamespace(string.Empty);

        /// <summary>
        /// Clears the Namespace Stack.
        /// </summary>
        public void Clear()
        {
            stack.Clear();
            Init();
        }
    }
}
