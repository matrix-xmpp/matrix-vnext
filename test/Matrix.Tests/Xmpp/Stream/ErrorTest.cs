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

using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream
{
    public class ErrorTest
    {
        [Fact]
        public void TestBuildstreamError()
        {
            new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint)
                .ShouldBe(Resource.Get("Xmpp.Stream.stream_error1.xml"));

            new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.InvalidXml)
                .ShouldBe(Resource.Get("Xmpp.Stream.stream_error2.xml"));
        }

        [Fact]
        public void TestShouldbeOfTypeError()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error1.xml")).ShouldBeOfType<Matrix.Xmpp.Stream.Error>();
        }

        [Fact]
        public void TestStreamError1()
        {
            var error = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error1.xml")).Cast<Matrix.Xmpp.Stream.Error>();
            Assert.Equal(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint, true);
        }

        [Fact]
        public void TestStreamError2()
        {
            var error = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error2.xml")).Cast<Matrix.Xmpp.Stream.Error>();
            Assert.Equal(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.InvalidXml, true);
        }
    }
}
