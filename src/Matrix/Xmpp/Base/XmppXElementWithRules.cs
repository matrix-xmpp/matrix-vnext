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
using Matrix.Xml;
using Matrix.Xmpp.AdvancedMessageProcessing;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithRules : XmppXElement
    {
        protected XmppXElementWithRules(string ns, string tagname) : base(ns, tagname) { }

        #region << Rule properties >>
        /// <summary>
        /// Adds a rule.
        /// </summary>
        /// <returns></returns>
        public Rule AddRule()
        {
            var rule = new Rule();
            Add(rule);

            return rule;
        }

        /// <summary>
        /// Adds a rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public void AddRule(Rule rule)
        {
            Add(rule);
        }

        /// <summary>
        /// Gets all rules.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Rule> GetRules()
        {
            return Elements<Rule>();
        }

        /// <summary>
        /// Sets the rules.
        /// </summary>
        /// <param name="rules">The rules.</param>
        public void SetItems(IEnumerable<Rule> rules)
        {
            RemoveAllRules();
            foreach (Rule rule in rules)
                AddRule(rule);
        }

        /// <summary>
        /// Removes all rules.
        /// </summary>
        public void RemoveAllRules()
        {
            RemoveAll<Rule>();
        }
        #endregion
    }
}
