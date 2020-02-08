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

using Xunit;

using Matrix.Xml;
using Shouldly;
using Matrix.Xmpp.Avatar;
using System.Text;

namespace Matrix.Tests.Xmpp.Avatar
{
    public class DataTests
    {
        [Fact]
        public void TestFactory()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.data.xml"))
                .ShouldBeOfType<Data>();
        }

        [Fact]
        public void TestReadAttributes()
        {
            var data = XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.data.xml")).Cast<Data>();

            data.Bytes.ShouldBe(Encoding.UTF8.GetBytes("Hello World"));
            
        }        
    }
}
