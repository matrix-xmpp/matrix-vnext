/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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
using Xunit;
using Shouldly;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Ping;
using Matrix.Xmpp.Stream;

namespace Matrix.Tests.Xml
{
    public class ExtensionsTest
    {
        [Fact]
        public void TestMatchPredicates()
        {
            var pIq = new IqQuery<PingIq> {Type = IqType.Get, Id = "foo"};

            pIq.IsMatch(iq => iq.HasAttribute("id")).ShouldBeTrue();
            pIq.IsMatch(iq => iq.HasAttribute("foo")).ShouldBeFalse();


            pIq.IsMatch(el => el.OfType<Iq>()).ShouldBeTrue();

            pIq.IsMatch(el => el.OfType<Iq>() && el.Cast<Iq>().Id == "foo").ShouldBeTrue();
            pIq.IsMatch(el => el.OfType<Iq>() && el.Cast<Iq>().Id == "foo" && el.Cast<Iq>().Type == IqType.Get)
                .ShouldBeTrue();
            pIq.IsMatch(el => el.OfType<Iq>() && el.Cast<Iq>().Id == "foo" && el.Cast<Iq>().Type == IqType.Result)
                .ShouldBeFalse();
            

            Func<XmppXElement, bool> predicate1 = e => e.OfType<StreamFeatures>();
            var elSf = XmppXElement.LoadXml("<stream:features xmlns:stream='http://etherx.jabber.org/streams'/>");
            elSf.ShouldBeOfType<StreamFeatures>();
            elSf.IsMatch(predicate1).ShouldBeTrue();

            var iq2 = new Iq {Type = IqType.Get};

            iq2.IsMatch(el =>
                el.OfType<Iq>()
                && el.Cast<Iq>().Type == IqType.Get
                && el.Cast<Iq>().Query.OfType<Ping>()
            ).ShouldBeFalse();

            iq2.IsMatch(el =>
                el.OfType<Iq>()
                && el.Cast<Iq>().Type == IqType.Get
            ).ShouldBeTrue();
        }
    }
}
