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

using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Xmpp.Ping;

namespace Matrix.Network.Handlers
{
    /// <summary>
    /// This handler automatically replies to incoming XMPP Pings from clients or servers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Name("XmppPing-Handler")]
    public class XmppPingHandler<T> : XmppStanzaHandler where T : Iq
    {
        public XmppPingHandler()
        {
            Handle(
                el =>
                    el.OfType<T>()
                    && el.Cast<T>().Type == IqType.Get
                    && el.Cast<T>().Query.OfType<Ping>(),

                async (context, xmppXElement) =>
                {
                    var iq = xmppXElement.Cast<T>();

                    var resIq = Factory.GetElement<T>();
                    resIq.Id    = iq.Id;
                    resIq.To    = iq.From;
                    resIq.Type  = IqType.Result;

                    await SendAsync(resIq);
                });
        }
    }
}
