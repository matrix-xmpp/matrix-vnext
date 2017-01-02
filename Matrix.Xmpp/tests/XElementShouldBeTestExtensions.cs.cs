using System.Linq;
using System.Xml.Linq;

using Shouldly;

namespace Matrix.Xmpp.Tests
{
    public static class XElementShouldBeTestExtensions
    {
        /*
         * see:
         * http://stackoverflow.com/questions/7318157/best-way-to-compare-xelement-objects
         * https://weblogs.asp.net/marianor/easy-way-to-compare-two-xmls-for-equality
         * https://blogs.msdn.microsoft.com/ericwhite/2009/01/27/equality-semantics-of-linq-to-xml-trees/
         * https://msdn.microsoft.com/de-de/library/system.xml.linq.xnode.deepequals(v=vs.110).aspx
         */
        public static void ShouldBe(this XElement actual, XElement expected)
        {
            string sActual  = Normalize(actual).ToString(SaveOptions.DisableFormatting);
            string sExpected = Normalize(expected).ToString(SaveOptions.DisableFormatting);

            sActual.ShouldBe(sExpected);
        }

        public static void ShouldBe(this XElement actual, string expected)
        {
            ShouldBe(actual, XElement.Parse(expected));
        }
        
        /// <summary>
        /// Normalized Xml elements. For comparing Xml the order of attributes does not matter.
        /// It also does not matter how empty tags are serialized.
        /// Once 2 XElements are normalied we should be able to compare them as strings.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static XElement Normalize(XElement element)
        {
            if (element.HasElements)
            {
                return new XElement(
                    element.Name,
                    element.Attributes()
                           .OrderBy(a => a.Name.ToString()),
                    element.Elements()
                           .OrderBy(a => a.Name.ToString())
                           .Select(Normalize));
            }

            if (element.IsEmpty || string.IsNullOrEmpty(element.Value))
            {
                return new XElement(
                    element.Name,
                    element.Attributes()
                           .OrderBy(a => a.Name.ToString()));
            }

            return new XElement(element.Name, element.Attributes()
                .OrderBy(a => a.Name.ToString()), element.Value);
        }
    }
}
