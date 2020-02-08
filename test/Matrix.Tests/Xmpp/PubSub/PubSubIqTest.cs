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

using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class PubSubIqTest
    {
        [Fact]
        public void TestBuildPubSubIq1()
        {
            PubSubIq pIq = new PubSubIq
            {
                Type = Matrix.Xmpp.IqType.Result,
                From = "pubsub.shakespeare.lit",
                To = "hamlet@denmark.lit/elsinore",
                Id = "create2",
                PubSub = {Create = new Matrix.Xmpp.PubSub.Create {Node = "25e3d37dabbab9541f7523321421edc5bfeb2dae"}}
            };


            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.publish_iq1.xml"));
        }

        [Fact]
        public void TestBuildPubSubIq2()
        {
            var pIq = new PubSubIq
            {
                Type = Matrix.Xmpp.IqType.Set,
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "create2"

            };

            pIq.PubSub.Create       = new Matrix.Xmpp.PubSub.Create();
            pIq.PubSub.Configure    = new Matrix.Xmpp.PubSub.Configure();
            
            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.publish_iq2.xml"));
        }
    }
}
