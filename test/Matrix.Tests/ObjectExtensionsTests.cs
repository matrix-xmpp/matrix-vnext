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

using DotNetty.Transport.Bootstrapping;
using Matrix.Network;
using Matrix.Network.Resolver;
using Shouldly;
using Xunit;

namespace Matrix.Tests
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;

    public class ObjectExtensionsTests
    {
        [Fact]
        public void Is_Interface_Implemented_Test()
        {
            INameResolver resolver = new StaticNameResolver("example.com", 5222, true);
            resolver.Implements<IDirectTls>().ShouldBe(true);

            resolver.Cast<IDirectTls>().DirectTls.ShouldBe(true);

            string s = "hello world";
            s.Implements<IDirectTls>().ShouldBe(false);
        }


        [Fact]
        public void Int_IsAnyOf_Should_Return_True()
        {
            1.IsAnyOf(0, 2, 3, 4, 5, 6, 1).ShouldBeTrue();
        }

        [Fact]
        public void String_IsAnyOf_Values_Should_Return_True()
        {
            "foo".IsAnyOf("foo", "bar", "foobar").ShouldBeTrue();
        }

        [Fact]
        public void String_IsAnyOf_Values_Should_Return_False()
        {
            "foo".IsAnyOf("foooo", "bar", "foobar").ShouldBeFalse();
        }

        [Fact]
        public void IqType_IsAnyOf_Values_Should_Return_True()
        {
            var iq = new Iq
            {
                Type = IqType.Result
            };

            iq.Type.IsAnyOf(IqType.Set, IqType.Result).ShouldBeTrue();
        }

        [Fact]
        public void IqType_IsAnyOf_Values_Should_Return_False()
        {
            var iq = new Iq
            {
                Type = IqType.Result
            };

            iq.Type.IsAnyOf(IqType.Error, IqType.Get).ShouldBeFalse();
        }
    }
}
