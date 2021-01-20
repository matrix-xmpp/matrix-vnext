namespace Matrix.Tests.Xml
{
    using Matrix.Xml;
    using Matrix.Xml.Parser;
    using Shouldly;
    using System.Text;
    using Xunit;

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
        
        [Fact]
        public void XmlWithUnicodeTagsTest()
        {
            StreamParser parser = new StreamParser();
            XmppXElement el = null;

            parser.OnStreamElement += (XmppXElement e) => el = e;

            string xml = @"<foo><फ़क /></foo>";

            var b1 = Encoding.UTF8.GetBytes(xml);
            parser.Write(b1, 0, b1.Length);

            el.Name.LocalName.ShouldBe("फ़क");
        }
        
        [Fact]
        public void XmlWithUnicodeTagsThatContainPayloadTest()
        {
            StreamParser parser = new StreamParser();
            XmppXElement el = null;

            parser.OnStreamElement += (XmppXElement e) => el = e;

            string xml = @"<foo><फ़क>bar</फ़क></foo>";

            var b1 = Encoding.UTF8.GetBytes(xml);
            parser.Write(b1, 0, b1.Length);

            el.Name.LocalName.ShouldBe("फ़क");
            el.Value.ShouldBe("bar");
        }
    }
}
