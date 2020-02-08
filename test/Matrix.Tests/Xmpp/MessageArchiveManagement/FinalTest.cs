/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

namespace Matrix.Tests.Xmpp.MessageArchiveManagement
{
    using Matrix.Xml;
    using Matrix.Xmpp.MessageArchiveManagement;
    using Shouldly;
    using Xunit;

    public class FinalTest
    {
        private const string FIN1 = @"<fin xmlns='urn:xmpp:mam:2' complete='true' />";
        private const string FIN2 = @"<fin xmlns='urn:xmpp:mam:2'>
                                <set xmlns='http://jabber.org/protocol/rsm'>
                                  <first index='0'>28482-98726-73623</first>
                                  <last>09af3-cc343-b409f</last>
                                  <count>20</count>
                                </set>
                              </fin>";

        [Fact]
        public void ShouldBeOfTypeFin()
        {
            XmppXElement.LoadXml(FIN1).ShouldBeOfType<Final>();
            XmppXElement.LoadXml(FIN2).ShouldBeOfType<Final>();
        }

        [Fact]
        public void ShouldComtainResultSet()
        {
            XmppXElement.LoadXml(FIN1).Cast<Final>().ResultSet.ShouldBeNull();
            XmppXElement.LoadXml(FIN2).Cast<Final>().ResultSet.ShouldNotBeNull();
        }

        [Fact]
        public void TestCompleteProperty()
        {
            XmppXElement.LoadXml(FIN1).Cast<Final>().Complete.ShouldBe(true);
        }

        [Fact]
        public void TestBuildFin()
        {
            new Final()
            {
                Complete = true
            }.ShouldBe(FIN1);
        }
    }
}
