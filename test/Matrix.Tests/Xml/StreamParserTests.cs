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

using Matrix.Xml.Parser;
using System.Text;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xml
{
    public class StreamParserTests
    {
        [Fact]
        public void EventOrderTest()
        {
            var output = new StringBuilder();
            StreamParser parser = new StreamParser();
            parser.OnStreamStart +=
               element =>
                   output.Append("1");

            parser.OnStreamElement +=
                element =>
                    output.Append("2");

            parser.OnStreamEnd += () =>
                output.Append("3");

            parser.OnStreamError +=
                 err => output.Append("4");

            var xml = Resource.Get("Xml.stream1.xml");
            parser.Write(Encoding.UTF8.GetBytes(xml));

            output.ToString().ShouldBe("1223");
        }

        [Fact]
        public void EventOrderWithStreamResetTest()
        {
            var output = new StringBuilder();
            StreamParser parser = new StreamParser();
            parser.OnStreamStart +=
               element =>
                   output.Append("1");

            parser.OnStreamElement +=
                element =>
                    output.Append("2");

            parser.OnStreamEnd += () =>
                output.Append("3");

            parser.OnStreamError +=
                 err => output.Append("4");

            var xml1 = Resource.Get("Xml.stream_partial1.xml");
            var xml2 = Resource.Get("Xml.stream_partial2.xml");

            parser.Write(Encoding.UTF8.GetBytes(xml1));
            parser.Reset();
            parser.Write(Encoding.UTF8.GetBytes(xml2));

            output.ToString().ShouldBe("1221223");
        }
    }
}
