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
using Matrix.Crypt;
using Matrix.Xml;
using Matrix.Xmpp.Jingle.Candidates;

namespace Matrix.Xmpp.Jingle.Transports
{
    [XmppTag(Name = "transport", Namespace = Namespaces.JingleTransportIceUdp)]
    public class TransportIceUdp : XmppXElement
    {
        #region << XML schema >>
        /*
         <xs:element name='transport'>
            <xs:complexType>
              <xs:choice minOccurs='0'>
                <xs:sequence>
                  <xs:element name='candidate' 
                              type='candidateElementType'
                              minOccurs='1' 
                              maxOccurs='unbounded'/>
                </xs:sequence>
                <xs:sequence>
                  <xs:element name='remote-candidate' 
                              type='remoteCandidateElementType'
                              minOccurs='1' 
                              maxOccurs='1'/>
                </xs:sequence>
              </xs:choice>
              <xs:attribute name='pwd' type='xs:string' use='optional'/>
              <xs:attribute name='ufrag' type='xs:string' use='optional'/>
            </xs:complexType>
         </xs:element>
        */
        #endregion
        public TransportIceUdp() : base(Namespaces.JingleTransportIceUdp, "transport")
        {}
        
        /// <summary>
        /// The ice-ufrag attribute which can be 4 to 256 bytes long
        /// </summary>
        public string Ufrag
        {
            get { return GetAttribute("ufrag"); }
            set { SetAttribute("ufrag", value); }
        }

        /// <summary>
        /// The ice-pwd attribute which can be 22 to 256 bytes long
        /// </summary>
        public string Pwd
        {
            get { return GetAttribute("pwd"); }
            set { SetAttribute("pwd", value); }
        }

        public void GeneratePwd()
        {
            Pwd = Randoms.GenerateRandomString(22);
        }

        public void GeneratePwd(int length)
        {            
            Pwd = Randoms.GenerateRandomString(length);
        }

        public void GenerateUfrag()
        {
            Ufrag = Randoms.GenerateRandomString(4);
        }

        public void GenerateUfrag(int length)
        {
            Ufrag = Randoms.GenerateRandomString(length);
        }

        #region << Candidate properties >>
        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <returns></returns>
        public CandidateIceUdp AddCandidate()
        {
            var cand = new CandidateIceUdp();
            Add(cand);

            return cand;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="cand">The cand.</param>
        public void AddCandidate(CandidateIceUdp cand)
        {
            Add(cand);
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        public void AddItems(CandidateIceUdp[] candidates)
        {
            foreach (var cand in candidates)
                Add(cand);
        }
      
        public IEnumerable<CandidateIceUdp> GetCandidates()
        {
            return Elements<CandidateIceUdp>();
        }
        
        /// <summary>
        /// Sets the candidates.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        public void SetCandidates(IEnumerable<CandidateIceUdp> candidates)
        {
            RemoveAllCandidates();
            foreach (CandidateIceUdp cand in candidates)
                AddCandidate(cand);
        }
        
        /// <summary>
        /// Removes all candidates.
        /// </summary>
        public void RemoveAllCandidates()
        {
            RemoveAll<CandidateIceUdp>();
        }
        #endregion
    }
}
