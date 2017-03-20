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

using System.Text;
using Xunit;
using Matrix.Xml.Parser;
using Shouldly;

namespace Matrix.Tests.Xml
{
    public class ByteBufferTests
    {
        [Fact]
        public void TestByteBuffer()
        {
            var b1 = Encoding.UTF8.GetBytes("Hello");
            var b2 = Encoding.UTF8.GetBytes(" ");
            var b3 = Encoding.UTF8.GetBytes("World");

            var concat123 = Encoding.UTF8.GetBytes("Hello World");
            var clear3 = Encoding.UTF8.GetBytes("lo World");
            var clear32 = Encoding.UTF8.GetBytes("World");
            var buf = new ByteBuffer();

            buf.Write(b1);
            buf.Write(b2);
            buf.Write(b3);


            buf.GetBuffer().ShouldBe(concat123);

            buf.RemoveFirst(3);
            buf.GetBuffer().ShouldBe(clear3);
            buf.RemoveFirst(3);
            buf.GetBuffer().ShouldBe(clear32);
            buf.RemoveFirst(5);
            buf.RemoveFirst(100);
         }   
    }
}
