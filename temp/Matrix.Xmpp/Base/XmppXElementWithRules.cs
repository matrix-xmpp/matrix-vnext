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