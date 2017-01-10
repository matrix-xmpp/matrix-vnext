using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// Contains a display string for use by implementations which are unable to utilize the applicable security policy 
    /// to generate display markings.
    /// </summary>
    [XmppTag(Name = "displaymarking", Namespace = Namespaces.SecurityLabel)]
    public class DisplayMarking : XmppXElement
    {
        /*
         * The fgcolor= default is black. The bgcolor= default is white.
         */

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMarking"/> class.
        /// </summary>
        public DisplayMarking() : base(Namespaces.SecurityLabel, "displaymarking")
        {
        }

        /// <summary>
        /// Gets or sets the color of the foreground. Default is Black
        /// </summary>
        /// <value>
        /// The color of the foreground.
        /// </value>
        public Color ForegroundColor
        {
            //<xs:attribute name="fgcolor" type="color" use="optional" default="black"/>
            get
            {
                if (!HasAttribute("fgcolor"))
                    return Color.Black;
                
                return GetAttributeEnum<Color>("fgcolor");
            }
            set { SetAttribute("fgcolor", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the color of the background. Default is White.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor
        {
            //<xs:attribute name="bgcolor" type="color" use="optional" default="white"/>
            get
            {
                if (!HasAttribute("bgcolor"))
                    return Color.White;

                return GetAttributeEnum<Color>("bgcolor");
            }
            set { SetAttribute("bgcolor", value.ToString().ToLower()); }
        }

        public string ForegroundHexColor
        {
            //<xs:attribute name="fgcolor" type="color" use="optional" default="black"/>
            get { return GetAttribute("fgcolor"); }
            set { SetAttribute("fgcolor", value); }
        }

        public string BackgroundHexColor
        {
            //<xs:attribute name="bgcolor" type="color" use="optional" default="white"/>
            get
            {
                return GetAttribute("bgcolor");
            }
            set { SetAttribute("bgcolor", value); }
        }
    }
}