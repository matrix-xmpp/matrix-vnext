namespace Matrix.XpNet
{
    /// <summary>
    /// Tokens that might have been found
    /// </summary>
    public enum Tokens
    {
        /// <summary>
        /// Represents one or more characters of data.
        /// </summary>
        DataChars,

        /// <summary>
        /// Represents a newline (CR, LF or CR followed by LF) in data.
        /// </summary>
        DataNewline,

        /// <summary>
        /// Represents a complete start-tag <code>&lt;name&gt;</code>,
        /// that doesn't have any attribute specifications.
        /// </summary>
        StartTagNoAtts,

        /// <summary>
        /// Represents a complete start-tag <code>&lt;name
        /// att = "val" & gt;</code>, that contains one or more
        /// attribute specifications.
        /// </summary>
        StartTagWithAtts,

        /// <summary>
        /// Represents an empty element tag <code>&lt;name/&gt;</code>,
        /// that doesn't have any attribute specifications.
        /// </summary>
        EmptyElementNoAtts,

        /// <summary>
        /// Represents an empty element tag <code>&lt;name
        /// att = "val" / &gt;</code>, that contains one or more
        /// attribute specifications.
        /// </summary>
        EmptyElementWithAtts,

        /// <summary>
        /// Represents a complete end-tag <code>&lt;/name&gt;</code>.
        /// </summary>
        EndTag,

        /// <summary>
        /// Represents the start of a CDATA section <code>&lt;![CDATA[</code>.
        /// </summary>
        CdataSectOpen,

        /// <summary>
        /// Represents the end of a CDATA section <code>]]&gt;</code>.
        /// </summary>
        CdataSectClose,

        /// <summary>
        /// Represents a general entity reference.
        /// </summary>
        EntityReference,

        /// <summary>
        /// Represents a general entity reference to a one of the 5
        /// predefined entities <code>amp</code>, <code>lt</code>,
        /// <code>gt</code>, <code>quot</code>, <code>apos</code>.
        /// </summary>
        MagicEntityReference,

        /// <summary>
        /// Represents a numeric character reference (decimal or
        /// hexadecimal), when the referenced character is less
        /// than or equal to 0xFFFF and so is represented by a
        /// single char.
        /// </summary>
        CharReference,

        /// <summary>
        /// Represents a numeric character reference (decimal or
        /// hexadecimal), when the referenced character is greater
        /// than 0xFFFF and so is represented by a pair of chars.
        /// </summary>
        CharPairReference,

        /// <summary>
        /// Represents a processing instruction.
        /// </summary>
        ProcessingInstruction,

        /// <summary>
        /// Represents an XML declaration or text declaration (a processing instruction whose target is <code>xml</code>).
        /// </summary>
        XmlDeclaration,

        /// <summary>
        /// Represents a comment <code>&lt;!-- comment --&gt;</code>.
        /// This can occur both in the prolog and in content.
        /// </summary>
        Comment,

        /// <summary>
        /// Represents a white space character in an attribute
        /// value, excluding white space characters that are part
        /// of line boundaries.
        /// </summary>
        AttributeValueS,

        /// <summary>
        /// Represents a parameter entity reference in the prolog.
        /// </summary>
        ParamEntityReference,

        /// <summary>
        /// Represents whitespace in the prolog. The token contains one or more whitespace characters.
        /// </summary>
        PrologS,

        /// <summary>
        /// Represents <code>&lt;!NAME</code> in the prolog.
        /// </summary>
        DeclOpen,

        /// <summary>
        /// Represents <code>&gt;</code> in the prolog.
        /// </summary>
        DeclClose,

        /// <summary>
        /// Represents a name in the prolog.
        /// </summary>
        Name,

        /// <summary>
        /// Represents a name token in the prolog that is not a name.
        /// </summary>
        Nmtoken,

        /// <summary>
        /// Represents <code>#NAME</code> in the prolog.
        /// </summary>
        PoundName,

        /// <summary>
        /// Represents <code>|</code> in the prolog.
        /// </summary>
        Or,

        /// <summary>
        /// Represents a <code>%</code> in the prolog that does not start
        /// a parameter entity reference.
        /// This can occur in an entity declaration.
        /// </summary>
        Percent,

        /// <summary>
        /// Represents a <code>(</code> in the prolog.
        /// </summary>
        OpenParen,

        /// <summary>
        /// Represents a <code>)</code> in the prolog that is not
        /// followed immediately by any of
        /// <code>*</code>, <code>+</code> or <code>?</code>.
        /// </summary>
        CloseParen,

        /// <summary>
        /// Represents <code>[</code> in the prolog.
        /// </summary>
        OpenBracket,

        /// <summary>
        /// Represents <code>]</code> in the prolog.
        /// </summary>
        CloseBracket,

        /// <summary>
        /// Represents a literal (EntityValue, AttValue, SystemLiteral or * PubidLiteral).
        /// </summary>
        Literal,

        /// <summary>
        /// Represents a name followed immediately by <code>?</code>.
        /// </summary>
        NameQuestion,

        /// <summary>
        /// Represents a name followed immediately by <code>*</code>.
        /// </summary>
        NameAsterisk,

        /// <summary>
        /// Represents a name followed immediately by <code>+</code>.
        /// </summary>
        NamePlus,

        /// <summary>
        /// Represents <code>&lt;![</code> in the prolog.
        /// </summary>
        CondSectOpen,

        /// <summary>
        /// Represents <code>]]&gt;</code> in the prolog.
        /// </summary>
        CondSectClose,

        /// <summary>
        ///  Represents &lt;code&gt;)?&lt;/code&gt; in the prolog.
        /// </summary>
        CloseParenQuestion,

        /// <summary>
        /// Represents <code>)*</code> in the prolog.
        /// </summary>
        CloseParenAsterisk,

        /// <summary>
        /// Represents <code>)+</code> in the prolog.
        /// </summary>
        CloseParenPlus,

        /// <summary>
        /// Represents <code>,</code> in the prolog.
        /// </summary>
        Comma,

        /// <summary>
        /// When we received a partial Xmp fragment and expect the rest of the Element with the next read.
        /// </summary>
        PartialToken,

        /// <summary>
        /// When received a partial char and expect the rest of the char in the next receive event.
        /// </summary>
        PartialChar,

        /// <summary>
        /// Indicates that the byte subarray being tokenized is a legal XML
        /// token, but that subsequent bytes in the same entity could be part of
        /// the token. For example, <code>Encoding.tokenizeProlog</code>
        /// would return this if the byte subarray consists of a legal XML name.
        /// </summary>
        ExtensibleToken,
    }
}
