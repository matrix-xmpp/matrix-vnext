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
using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp.Jingle.Candidates;

namespace Matrix.Xmpp.Jingle.Transports
{
    [XmppTag(Name = "transport", Namespace = Namespaces.JingleTransportRawUdp)]
    public class TransportRawUdp : XmppXElement
    {
        #region << XML schema >>
        /*
        <xs:element name='transport'>
            <xs:complexType>
              <xs:sequence>
                <xs:element name='candidate' 
                            type='candidateElementType'
                            minOccurs='0'
                            maxOccurs='unbounded'/>
              </xs:sequence>
            </xs:complexType>
        </xs:element>        
        */
        #endregion
        public TransportRawUdp()
            : base(Namespaces.JingleTransportRawUdp, "transport")
        { }


        #region << Candidate properties >>
        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <returns></returns>
        public CandidateRawUdp AddCandidate()
        {
            var cand = new CandidateRawUdp();
            Add(cand);

            return cand;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="cand">The cand.</param>
        public void AddCandidate(CandidateRawUdp cand)
        {
            Add(cand);
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        public void AddItems(CandidateRawUdp[] candidates)
        {
            foreach (var cand in candidates)
                Add(cand);
        }

        public IEnumerable<CandidateRawUdp> GetCandidates()
        {
            return Elements<CandidateRawUdp>();
        }

        /// <summary>
        /// Sets the candidates.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        public void SetCandidates(IEnumerable<CandidateRawUdp> candidates)
        {
            RemoveAllCandidates();
            foreach (CandidateRawUdp cand in candidates)
                AddCandidate(cand);
        }

        /// <summary>
        /// Removes all candidates.
        /// </summary>
        public void RemoveAllCandidates()
        {
            RemoveAll<CandidateRawUdp>();
        }
        #endregion
    }
}
