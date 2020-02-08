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

namespace Matrix.Tests.Xmpp.Client
{
    public class Error
    {
        [Fact]
        public void TestErrors()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error3.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.Forbidden);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error4.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.Gone);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error5.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.InternalServerError);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error6.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.ItemNotFound);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error7.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.JidMalformed);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error8.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.BadRequest);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error9.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotAcceptable);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error10.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotAuthorized);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error11.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotModified);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error12.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.PaymentRequired);
            
            var err = XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error1.xml")).Cast<Matrix.Xmpp.Client.Error>();
            err.Text.ShouldBe("dummy text");
        }

        [Fact]
        public void BuildError()
        {
            var expectedXml1 = Resource.Get("Xmpp.Client.error1.xml");
            var expectedXml2 = Resource.Get("Xmpp.Client.error2.xml");

            new Matrix.Xmpp.Client.Error(Matrix.Xmpp.Base.ErrorCondition.BadRequest)
            {
                Text = "dummy text"
            }
            .ShouldBe(expectedXml1);

            new Matrix.Xmpp.Client.Error(Matrix.Xmpp.Base.ErrorCondition.BadRequest)
            .ShouldBe(expectedXml2);
        }
    }
}
