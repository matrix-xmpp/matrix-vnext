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

namespace Matrix.Tests
{
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Embedded;
    using Shouldly;
    using Xunit;
    using Matrix;
    using Matrix.Attributes;

    public class DotnNettyExtensionsTests
    {
        [Fact]
        public void ShouldAddHandlerAtTheEnd()
        {
            var channel = new EmbeddedChannel();

            channel.Pipeline.Get<FooHandler>().ShouldBe(null);
            channel.Pipeline.AddLast2(new FooHandler());
            channel.Pipeline.Get<FooHandler>().ShouldNotBe(null);
            channel.Pipeline.Get("Foo-Handler").ShouldNotBe(null);
            channel.Pipeline.Get("Foo-Handler").ShouldBeOfType<FooHandler>();
        }

        [Fact]
        public void ShouldContainHandler()
        {
            var channel = new EmbeddedChannel();

            channel.Pipeline.AddLast2(new FooHandler());
            channel.Pipeline.AddLast2(new BarHandler());
            channel.Pipeline.Contains<FooHandler>().ShouldBe(true);
            channel.Pipeline.Contains<BarHandler>().ShouldBe(true);
        }

        [Fact]
        public void ShouldNotContainHandler()
        {
            var channel = new EmbeddedChannel();
            
            channel.Pipeline.AddLast2(new FooHandler());
            channel.Pipeline.Contains<BarHandler>().ShouldBe(false);
        }

        [Fact]
        public void ShouldAddBeforeFooHandler()
        {
            var channel = new EmbeddedChannel();

            channel.Pipeline.AddLast<FooHandler>();
            // insert BarHandler before FooHandler
            channel.Pipeline.AddBefore<BarHandler, FooHandler>();

            var first = channel.Pipeline.First();
            var last = channel.Pipeline.Last();

            channel.Pipeline.First().ShouldBeOfType<BarHandler>();
            channel.Pipeline.Last().ShouldBeOfType<FooHandler>();
        }

        [Fact]
        public void ShouldAddAfterBarHandler()
        {
            var channel = new EmbeddedChannel();

            channel.Pipeline.AddLast<BarHandler>();
            // insert FooHandler after BarHandler
            channel.Pipeline.AddAfter<FooHandler, BarHandler>();

            var first = channel.Pipeline.First();
            var last = channel.Pipeline.Last();

            channel.Pipeline.First().ShouldBeOfType<BarHandler>();
            channel.Pipeline.Last().ShouldBeOfType<FooHandler>();
        }

        [Name("Foo-Handler")]
        public class FooHandler : ChannelHandlerAdapter
        {
        }


        [Name("Bar-Handler")]
        public class BarHandler : ChannelHandlerAdapter
        {
        }
    }
}
