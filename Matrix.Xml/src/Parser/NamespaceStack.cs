/*
 * Copyright (c) 2003-2010 by AG-Software
 * All Rights Reserved.
 * Contact information for AG-Software is available at http://www.ag-software.de
 * 
 * xpnet is a deriviative of James Clark's XP parser.
 * See copying.txt for more info.
 */

using System.Collections.Generic;

namespace Matrix.Xml.Parser
{
    /// <summary>
    /// Namespace stack which takes care of Xml namespaces and namespace inheritance.
    /// </summary>
    public class NamespaceStack
    {
        private readonly Stack<Dictionary<string, string>> _stack = new Stack<Dictionary<string, string>>();

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
        public void Init()
        {
            Push();
            AddNamespace("xmlns", "http://www.w3.org/2000/xmlns/");
            AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
        }

        /// <summary>
        /// Declare a new namespace.
        /// This should be called at the start of each xml element.
        /// </summary>
        public void Push()
        {
            _stack.Push(new Dictionary<string, string>());
        }

        /// <summary>
        /// Remove the current namespace from the stack.
        /// This should be called at the end of each xml element.
        /// </summary>
        public void Pop()
        {
            _stack.Pop();
        }

        /// <summary>
        /// Add a namespace to the current level.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="uri"></param>
        public void AddNamespace(string prefix, string uri)
        {
            _stack.Peek().Add(prefix, uri);
        }

        /// <summary>
        /// Find a namespace by prefix. Goes up all levels for namespace inheritance.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string LookupNamespace(string prefix)
        {
            foreach (Dictionary<string, string> dictionary in _stack)
            {
                if ((dictionary.Count > 0) && (dictionary.ContainsKey(prefix)))
                    return dictionary[prefix];
            }
            return string.Empty;
        }

        /// <summary>
        /// The current default namespace.
        /// </summary>
        public string DefaultNamespace
        {
            get { return LookupNamespace(string.Empty); }
        }


        /// <summary>
        /// Clears the Namespace Stack.
        /// </summary>
        public void Clear()
        {
            _stack.Clear();
            Init();
        }
    }
}