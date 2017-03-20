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

using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class DeleteTest
    {
        [Fact]
        public void BuildPubsibDeleteIq()
        {

            var pIq = new PubSubOwnerIq
            {
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "delete1",
                Type = IqType.Set,
                PubSub = { Delete = new Matrix.Xmpp.PubSub.Owner.Delete { Node = "princely_musings" } }
            };

            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub_delete_iq1.xml"));
        }
    }
}
