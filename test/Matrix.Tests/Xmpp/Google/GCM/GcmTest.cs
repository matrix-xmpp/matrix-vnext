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

using Matrix.Xmpp.Google.Mobile;
using Xunit;

namespace Matrix.Tests.Xmpp.Google.GCM
{
    
    public class GcmTest
    {
        [Fact]
        public void BuildGcmMessage()
        {
            var msg = new Matrix.Xmpp.Client.Message {Id = "foo"};
            msg.Add(new Gcm {Value = "{'to':'REGISTRATION_ID'}" });

            msg.ShouldBe(Resource.Get("Xmpp.Google.GCM.message1.xml"));
        }
    }
}
