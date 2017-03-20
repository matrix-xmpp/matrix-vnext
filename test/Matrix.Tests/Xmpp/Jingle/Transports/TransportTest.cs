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

using Matrix.Xmpp.Jingle.Transports;
using Xunit;

namespace Matrix.Tests.Xmpp.Jingle.Transports
{
    public class TransportTest
    {
        [Fact]
        public void TestBuildTransport()
        {
            var tp = new TransportIbb { BlockSize = 4096, Sid = "ch3d9s71"};
            tp.ShouldBe(Resource.Get("Xmpp.Jingle.Transports.transport1.xml"));
        }
    }
}
