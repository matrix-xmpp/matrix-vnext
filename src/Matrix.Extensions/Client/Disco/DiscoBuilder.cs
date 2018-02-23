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

namespace Matrix.Extensions.Client.Disco
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using System;

    public class DiscoBuilder
    {
        /// <summary>
        /// Builds a request to discover information of an entity
        /// </summary>
        /// <param name="to">The xmpp entity to discover</param>
        /// <returns></returns>
        /// <param name="node">Optional node information</param>
        /// <returns></returns>
        public static Iq DiscoverInformation(Jid to, string node = null)
        {
            var discoIq = new DiscoInfoIq { To = to, Type = IqType.Get };
                       
            if (!String.IsNullOrEmpty(node))
            { 
                discoIq.Info.Node = node;
            }

            return discoIq;
        }


        /// <summary>
        /// Builds a request to discover the items of an entity
        /// </summary>
        /// <param name="to">The xmpp entity to discover</param>
        /// <returns></returns>
        /// <param name="node">Optional node information</param>
        /// <returns></returns>
        public static Iq DiscoverItems(Jid to, string node = null)
        {
            var discoItemsIq = new DiscoItemsIq { Type = IqType.Get, To = to };

            if (!String.IsNullOrEmpty(node))
            { 
                discoItemsIq.Items.Node = node;
            }

            return discoItemsIq;
        }
    }
}
