/*
 * Copyright (c) 2003-2017 by Alexander Gnauck, AG-Software
 * All Rights Reserved.
 * Contact information for AG-Software is available at http://www.ag-software.de
 * 
 * xpnet is a deriviative of James Clark's XP parser.
 * See copying.txt for more info.
 */
namespace Matrix.XpNet
{

    /// <summary>
    /// Base tokenizer class
    /// </summary>
    public abstract class Encoding
    {
        // Bytes with type < 0 may not be data in content.
        // The negation of the lead byte type gives the total number of bytes.

        /// <summary>
        /// Need more bytes
        /// </summary>
        protected const int BtLead2 = -2;

        /// <summary>
        /// Need more bytes
        /// </summary>
        protected const int BtLead3 = -3;

        /// <summary>
        /// Need more bytes
        /// </summary>
        protected const int BtLead4 = -4;

        /// <summary>
        /// Not XML
        /// </summary>
        protected const int BtNoXml = BtLead4 - 1;

        /// <summary>
        /// Malformed XML
        /// </summary>
        protected const int BtMalform = BtNoXml - 1;

        /// <summary>
        /// Less than
        /// </summary>
        protected const int BtLt = BtMalform - 1;

        /// <summary>
        /// Ampersand
        /// </summary>
        protected const int BtAmp = BtLt - 1;

        /// <summary>
        /// right square bracket
        /// </summary>
        protected const int BtRsqb = BtAmp - 1;

        /// <summary>
        /// carriage return
        /// </summary>
        protected const int BtCr = BtRsqb - 1;

        /// <summary>
        /// line feed
        /// </summary>
        protected const int BtLf = BtCr - 1;

        // Bytes with type >= 0 are treated as data in content.

        /// <summary>
        /// greater than
        /// </summary>
        protected const int BtGt = 0;

        /// <summary>
        /// Quote
        /// </summary>
        protected const int BtQuot = BtGt + 1;

        /// <summary>
        /// Apostrophe
        /// </summary>
        protected const int BtApos = BtQuot + 1;

        /// <summary>
        /// Equal sign
        /// </summary>
        protected const int BtEquals = BtApos + 1;

        /// <summary>
        /// Question mark
        /// </summary>
        protected const int BtQuest = BtEquals + 1;

        /// <summary>
        /// Exclamation point
        /// </summary>
        protected const int BtExcl = BtQuest + 1;

        /// <summary>
        /// Solidus (/)
        /// </summary>
        protected const int BtSol = BtExcl + 1;

        /// <summary>
        /// Semicolon
        /// </summary>
        protected const int BtSemi = BtSol + 1;

        /// <summary>
        /// Hash
        /// </summary>
        protected const int BtNum = BtSemi + 1;

        /// <summary>
        /// Left square bracket
        /// </summary>
        protected const int BtLsqb = BtNum + 1;

        /// <summary>
        /// space
        /// </summary>
        protected const int BtS = BtLsqb + 1;

        /// <summary>
        /// 
        /// </summary>
        protected const int BtNmstrt = BtS + 1;

        /// <summary>
        /// 
        /// </summary>
        protected const int BtName = BtNmstrt + 1;

        /// <summary>
        /// Minus
        /// </summary>
        protected const int BtMinus = BtName + 1;

        /// <summary>
        /// Other
        /// </summary>
        protected const int BtOther = BtMinus + 1;

        /// <summary>
        /// Percent
        /// </summary>
        protected const int BtPercnt = BtOther + 1;

        /// <summary>
        /// Left paren
        /// </summary>
        protected const int BtLpar = BtPercnt + 1;

        /// <summary>
        /// Right paren
        /// </summary>
        protected const int BtRpar = BtLpar + 1;

        /// <summary>
        /// 
        /// </summary>
        protected const int BtAst = BtRpar + 1;

        /// <summary>
        /// +
        /// </summary>
        protected const int BtPlus = BtAst + 1;

        /// <summary>
        /// ,
        /// </summary>
        protected const int BtComma = BtPlus + 1;

        /// <summary>
        /// Pipe
        /// </summary>
        protected const int BtVerbar = BtComma + 1;

        /// <summary>
        /// What syntax do each of the ASCII7 characters have?
        /// </summary>
        protected static readonly int[] AsciiTypeTable = new int[]
            {
                /* 0x00 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x04 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x08 */ BtNoXml, BtS, BtLf, BtNoXml,
                /* 0x0C */ BtNoXml, BtCr, BtNoXml, BtNoXml,
                /* 0x10 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x14 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x18 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x1C */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
                /* 0x20 */ BtS, BtExcl, BtQuot, BtNum,
                /* 0x24 */ BtOther, BtPercnt, BtAmp, BtApos,
                /* 0x28 */ BtLpar, BtRpar, BtAst, BtPlus,
                /* 0x2C */ BtComma, BtMinus, BtName, BtSol,
                /* 0x30 */ BtName, BtName, BtName, BtName,
                /* 0x34 */ BtName, BtName, BtName, BtName,
                /* 0x38 */ BtName, BtName, BtNmstrt, BtSemi,
                /* 0x3C */ BtLt, BtEquals, BtGt, BtQuest,
                /* 0x40 */ BtOther, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x44 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x48 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x4C */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x50 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x54 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x58 */ BtNmstrt, BtNmstrt, BtNmstrt, BtLsqb,
                /* 0x5C */ BtOther, BtRsqb, BtOther, BtNmstrt,
                /* 0x60 */ BtOther, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x64 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x68 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x6C */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x70 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x74 */ BtNmstrt, BtNmstrt, BtNmstrt, BtNmstrt,
                /* 0x78 */ BtNmstrt, BtNmstrt, BtNmstrt, BtOther,
                /* 0x7C */ BtVerbar, BtOther, BtOther, BtOther,
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceBuf"></param>
        /// <param name="sourceStart"></param>
        /// <param name="sourceEnd"></param>
        /// <param name="targetBuf"></param>
        /// <param name="targetStart"></param>
        /// <returns></returns>
        protected abstract int Convert(byte[] sourceBuf,
                                       int sourceStart, int sourceEnd,
                                       char[] targetBuf, int targetStart);


        private static Encoding utf8Encoding;

        private const byte UTF8_ENCODING = 0;
        private const byte UTF16_LITTLE_ENDIAN_ENCODING = 1;
        private const byte UTF16_BIG_ENDIAN_ENCODING = 2;
        private const byte INTERNAL_ENCODING = 3;
        private const byte ISO8859_1_ENCODING = 4;
        private const byte ASCII_ENCODING = 5;

        private static Encoding GetEncoding(byte enc)
        {
            switch (enc)
            {
                case UTF8_ENCODING:
                    if (utf8Encoding == null)
                        utf8Encoding = new UTF8Encoding();
                    return utf8Encoding;
                    /*
            case UTF16_LITTLE_ENDIAN_ENCODING:
                if (utf16LittleEndianEncoding == null)
                    utf16LittleEndianEncoding = new UTF16LittleEndianEncoding();
                return utf16LittleEndianEncoding;
            case UTF16_BIG_ENDIAN_ENCODING:
                if (utf16BigEndianEncoding == null)
                    utf16BigEndianEncoding = new UTF16BigEndianEncoding();
                return utf16BigEndianEncoding;
            case INTERNAL_ENCODING:
                if (internalEncoding == null)
                    internalEncoding = new InternalEncoding();
                return internalEncoding;
            case ISO8859_1_ENCODING:
                if (iso8859_1Encoding == null)
                    iso8859_1Encoding = new ISO8859_1Encoding();
                return iso8859_1Encoding;
            case ASCII_ENCODING:
                if (asciiEncoding == null)
                    asciiEncoding = new ASCIIEncoding();
                return asciiEncoding;
                */
            }
            return null;
        }

        private int minBPC;

        /// <summary>
        /// Constructor called by subclasses to set the minimum bytes per character
        /// </summary>
        /// <param name="minBPC"></param>
        protected Encoding(int minBPC)
        {
            this.minBPC = minBPC;
        }

        /// <summary>
        /// Get the byte type of the next byte. There are guaranteed to be minBPC available bytes starting at off.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected abstract int ByteType(byte[] buf, int off);

        /// <summary>
        /// Really only works for ASCII7.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected abstract char ByteToAscii(byte[] buf, int off);

        /// <summary>
        /// This must only be called when c is an (XML significant)
        /// ASCII character.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        protected abstract bool CharMatches(byte[] buf, int off, char c);

        /// <summary>
        /// Called only when byteType(buf, off) == BT_LEAD2
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected virtual int ByteType2(byte[] buf, int off)
        {
            return BtOther;
        }

        /// <summary>
        /// Called only when byteType(buf, off) == BT_LEAD3
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        private int ByteType3(byte[] buf, int off)
        {
            return BtOther;
        }

        /// <summary>
        /// Called only when byteType(buf, off) == BT_LEAD4
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        private static int ByteType4(byte[] buf, int off)
        {
            return BtOther;
        }

        private void Check2(byte[] buf, int off)
        {
        }

        private void Check3(byte[] buf, int off)
        {
        }

        private void Check4(byte[] buf, int off)
        {
        }

        /// <summary>
        /// Moves a position forward.  On entry, <code>pos</code> gives
        /// the position of the byte at index<code> off</code> in
        /// <code>buf</code>.On exit, it<code> pos</code> will give
        /// the position of the byte at index<code> end</code>, which
        /// must be greater than or equal to <code>off</code>.The
        /// bytes between<code> off</code> and<code> end</code> must
        /// encode one or more complete characters.A carriage return
        /// followed by a line feed will be treated as a single line
        /// delimiter provided that they are given to
        /// <code>movePosition</code> together.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="end"></param>
        /// <param name="pos"></param>
        protected abstract void MovePosition(byte[] buf, int off, int end, Position pos);

        private void CheckCharMatches(byte[] buf, int off, char c)
        {
            if (!CharMatches(buf, off, c))
                throw new InvalidTokenException(off);
        }

        /* off points to character following "<!-" */
        private Tokens ScanComment(byte[] buf, int off, int end, Token token)
        {
            if (off != end)
            {
                CheckCharMatches(buf, off, '-');
                off += minBPC;
                while (off != end)
                {
                    switch (ByteType(buf, off))
                    {
                        case BtLead2:
                            if (end - off < 2)
                                return Tokens.PartialChar; //throw new PartialCharException(off);
                            Check2(buf, off);
                            off += 2;
                            break;
                        case BtLead3:
                            if (end - off < 3)
                                return Tokens.PartialChar; //throw new PartialCharException(off);
                            Check3(buf, off);
                            off += 3;
                            break;
                        case BtLead4:
                            if (end - off < 4)
                                return Tokens.PartialChar; //throw new PartialCharException(off);
                            Check4(buf, off);
                            off += 4;
                            break;
                        case BtNoXml:
                        case BtMalform:
                            throw new InvalidTokenException(off);
                        case BtMinus:
                            if ((off += minBPC) == end)
                                return Tokens.PartialToken; //throw new PartialTokenException();
                            if (CharMatches(buf, off, '-'))
                            {
                                if ((off += minBPC) == end)
                                    return Tokens.PartialToken; //throw new PartialTokenException();
                                CheckCharMatches(buf, off, '>');
                                token.TokenEnd = off + minBPC;
                                return Tokens.Comment;
                            }
                            break;
                        default:
                            off += minBPC;
                            break;
                    }
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* off points to character following "<!" */

        private Tokens ScanDecl(byte[] buf, int off, int end, Token token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtMinus:
                    return ScanComment(buf, off + minBPC, end, token);
                case BtLsqb:
                    token.TokenEnd = off + minBPC;
                    return Tokens.CondSectOpen;
                case BtNmstrt:
                    off += minBPC;
                    break;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtPercnt:
                        if (off + minBPC == end)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        /* don't allow <!ENTITY% foo "whatever"> */
                        switch (ByteType(buf, off + minBPC))
                        {
                            case BtS:
                            case BtCr:
                            case BtLf:
                            case BtPercnt:
                                throw new InvalidTokenException(off);
                        }
                        /* fall through */
                        goto case BtS;
                    case BtS:
                    case BtCr:
                    case BtLf:
                        token.TokenEnd = off;
                        return Tokens.DeclOpen;
                    case BtNmstrt:
                        off += minBPC;
                        break;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        private bool TargetIsXml(byte[] buf, int off, int end)
        {
            bool upper = false;
            if (end - off != minBPC*3)
                return false;
            switch (ByteToAscii(buf, off))
            {
                case 'x':
                    break;
                case 'X':
                    upper = true;
                    break;
                default:
                    return false;
            }
            off += minBPC;
            switch (ByteToAscii(buf, off))
            {
                case 'm':
                    break;
                case 'M':
                    upper = true;
                    break;
                default:
                    return false;
            }
            off += minBPC;
            switch (ByteToAscii(buf, off))
            {
                case 'l':
                    break;
                case 'L':
                    upper = true;
                    break;
                default:
                    return false;
            }
            if (upper)
                throw new InvalidTokenException(off, InvalidTokenException.XmlTarget);
            return true;
        }

        /* off points to character following "<?" */

        private Tokens ScanPi(byte[] buf, int off, int end, Token token)
        {
            int target = off;
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; // throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtS:
                    case BtCr:
                    case BtLf:
                        bool isXml = TargetIsXml(buf, target, off);
                        token.NameEnd = off;
                        off += minBPC;
                        while (off != end)
                        {
                            switch (ByteType(buf, off))
                            {
                                case BtLead2:
                                    if (end - off < 2)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    Check2(buf, off);
                                    off += 2;
                                    break;
                                case BtLead3:
                                    if (end - off < 3)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    Check3(buf, off);
                                    off += 3;
                                    break;
                                case BtLead4:
                                    if (end - off < 4)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    Check4(buf, off);
                                    off += 4;
                                    break;
                                case BtNoXml:
                                case BtMalform:
                                    throw new InvalidTokenException(off);
                                case BtQuest:
                                    off += minBPC;
                                    if (off == end)
                                        return Tokens.PartialToken; //throw new PartialTokenException();
                                    if (CharMatches(buf, off, '>'))
                                    {
                                        token.TokenEnd = off + minBPC;
                                        if (isXml)
                                            return Tokens.XmlDeclaration;
                                        else
                                            return Tokens.ProcessingInstruction;
                                    }
                                    break;
                                default:
                                    off += minBPC;
                                    break;
                            }
                        }
                        return Tokens.PartialToken; //throw new PartialTokenException();
                    case BtQuest:
                        token.NameEnd = off;
                        off += minBPC;
                        if (off == end)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        CheckCharMatches(buf, off, '>');
                        token.TokenEnd = off + minBPC;
                        return (TargetIsXml(buf, target, token.NameEnd)
                                    ? Tokens.XmlDeclaration
                                    : Tokens.ProcessingInstruction);
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /// <summary>
        /// off points to character following "&lt;![" 
        /// </summary>
        private const string Cdata = "CDATA[";

        private Tokens ScanCdataSection(byte[] buf, int off, int end, Token token)
        {
            /* "CDATA[".length() == 6 */
            if (end - off < 6*minBPC)
                return Tokens.PartialToken; //throw new PartialTokenException();
            for (int i = 0; i < Cdata.Length; i++, off += minBPC)
                CheckCharMatches(buf, off, Cdata[i]);
            token.TokenEnd = off;
            return Tokens.CdataSectOpen;
        }

        /// <summary>
        /// Scans the first token of a byte subarrary that starts with the
        /// content of a CDATA section.
        /// Returns one of the following integers according to the type of token
        /// that the subarray starts with:
        /// <ul>
        /// <li><code>TOK.DATA_CHARS</code></li>
        /// <li><code>TOK.DATA_NEWLINE</code></li>
        /// <li><code>TOK.CDATA_SECT_CLOSE</code></li>
        /// </ul>
        /// Information about the token is stored in <code>token</code>.
        /// After<code> TOK.CDATA_SECT_CLOSE</code> is returned, the application
        /// should use <code>tokenizeContent</code>.
        /// @see #TOK.DATA_CHARS
        /// @see #TOK.DATA_NEWLINE
        /// @see #TOK.CDATA_SECT_CLOSE
        /// @see Token
        /// @see EmptyTokenException
        /// @see PartialTokenException
        /// @see InvalidTokenException
        /// @see ExtensibleTokenException
        /// @see #tokenizeContent
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="EmptyTokenException">EmptyTokenException if the subarray is empty</exception>
        /// <exception cref="InvalidTokenException">if the subarrary does not start with a legal token or part of one</exception>
        public Tokens TokenizeCdataSection(byte[] buf, int off, int end,
                                        Token token)
        {
            /* BPC with UTF-8 in XMPP is always 1
            if (minBPC > 1)
                end = adjustEnd(off, end);
            */
            if (off == end)
                throw new EmptyTokenException();
            switch (ByteType(buf, off))
            {
                case BtRsqb:
                    off += minBPC;
                    if (off == end)
                        return Tokens.PartialToken; //throw new PartialTokenException();
                    if (!CharMatches(buf, off, ']'))
                        break;
                    off += minBPC;
                    if (off == end)
                        return Tokens.PartialToken; //throw new PartialTokenException();
                    if (!CharMatches(buf, off, '>'))
                    {
                        off -= minBPC;
                        break;
                    }
                    token.TokenEnd = off + minBPC;
                    return Tokens.CdataSectClose;
                case BtCr:
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_NEWLINE);
                    if (ByteType(buf, off) == BtLf)
                        off += minBPC;
                    token.TokenEnd = off;
                    return Tokens.DataNewline;
                case BtLf:
                    token.TokenEnd = off + minBPC;
                    return Tokens.DataNewline;
                case BtNoXml:
                case BtMalform:
                    throw new InvalidTokenException(off);
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check2(buf, off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check3(buf, off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check4(buf, off);
                    off += 4;
                    break;
                default:
                    off += minBPC;
                    break;
            }
            token.TokenEnd = ExtendCdata(buf, off, end);
            return Tokens.DataChars;
        }

        private int ExtendCdata(byte[] buf, int off, int end)
        {
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtLead2:
                        if (end - off < 2)
                            return off;
                        Check2(buf, off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return off;
                        Check3(buf, off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return off;
                        Check4(buf, off);
                        off += 4;
                        break;
                    case BtRsqb:
                    case BtNoXml:
                    case BtMalform:
                    case BtCr:
                    case BtLf:
                        return off;
                    default:
                        off += minBPC;
                        break;
                }
            }
            return off;
        }


        /* off points to character following "</" */
        private Tokens ScanEndTag(byte[] buf, int off, int end, Token token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtS:
                    case BtCr:
                    case BtLf:
                        token.NameEnd = off;
                        for (off += minBPC; off != end; off += minBPC)
                        {
                            switch (ByteType(buf, off))
                            {
                                case BtS:
                                case BtCr:
                                case BtLf:
                                    break;
                                case BtGt:
                                    token.TokenEnd = off + minBPC;
                                    return Tokens.EndTag;
                                default:
                                    throw new InvalidTokenException(off);
                            }
                        }
                        return Tokens.PartialToken; //throw new PartialTokenException();
                    case BtGt:
                        token.NameEnd = off;
                        token.TokenEnd = off + minBPC;
                        return Tokens.EndTag;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* off points to character following "&#X" */

        private Tokens ScanHexCharRef(byte[] buf, int off, int end, Token token)
        {
            if (off != end)
            {
                int c = ByteToAscii(buf, off);
                int num;
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        num = c - '0';
                        break;
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        num = c - ('A' - 10);
                        break;
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        num = c - ('a' - 10);
                        break;
                    default:
                        throw new InvalidTokenException(off);
                }
                for (off += minBPC; off != end; off += minBPC)
                {
                    c = ByteToAscii(buf, off);
                    switch (c)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            num = (num << 4) + c - '0';
                            break;
                        case 'A':
                        case 'B':
                        case 'C':
                        case 'D':
                        case 'E':
                        case 'F':
                            num = (num << 4) + c - ('A' - 10);
                            break;
                        case 'a':
                        case 'b':
                        case 'c':
                        case 'd':
                        case 'e':
                        case 'f':
                            num = (num << 4) + c - ('a' - 10);
                            break;
                        case ';':
                            token.TokenEnd = off + minBPC;
                            return SetRefChar(num, token);
                        default:
                            throw new InvalidTokenException(off);
                    }
                    if (num >= 0x110000)
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* off points to character following "&#" */

        private Tokens ScanCharRef(byte[] buf, int off, int end, Token token)
        {
            if (off != end)
            {
                int c = ByteToAscii(buf, off);
                switch (c)
                {
                    case 'x':
                        return ScanHexCharRef(buf, off + minBPC, end, token);
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;
                    default:
                        throw new InvalidTokenException(off);
                }

                int num = c - '0';
                for (off += minBPC; off != end; off += minBPC)
                {
                    c = ByteToAscii(buf, off);
                    switch (c)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            num = num*10 + (c - '0');
                            if (num < 0x110000)
                                break;
                            /* fall through */
                            goto default;
                        default:
                            throw new InvalidTokenException(off);
                        case ';':
                            token.TokenEnd = off + minBPC;
                            return SetRefChar(num, token);
                    }
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* num is known to be < 0x110000; return the token code */

        private Tokens SetRefChar(int num, Token token)
        {
            if (num < 0x10000)
            {
                switch (CharTypeTable[num >> 8][num & 0xFF])
                {
                    case BtNoXml:
                    case BtLead4:
                    case BtMalform:
                        throw new InvalidTokenException(token.TokenEnd - minBPC);
                }
                token.RefChar1 = (char) num;
                return Tokens.CharReference;
            }
            else
            {
                num -= 0x10000;
                token.RefChar1 = (char) ((num >> 10) + 0xD800);
                token.RefChar2 = (char) ((num & ((1 << 10) - 1)) + 0xDC00);
                return Tokens.CharPairReference;
            }
        }

        private bool IsMagicEntityRef(byte[] buf, int off, int end, Token token)
        {
            switch (ByteToAscii(buf, off))
            {
                case 'a':
                    if (end - off < minBPC*4)
                        break;
                    switch (ByteToAscii(buf, off + minBPC))
                    {
                        case 'm':
                            if (CharMatches(buf, off + minBPC*2, 'p')
                                && CharMatches(buf, off + minBPC*3, ';'))
                            {
                                token.TokenEnd = off + minBPC*4;
                                token.RefChar1 = '&';
                                return true;
                            }
                            break;
                        case 'p':
                            if (end - off >= minBPC*5
                                && CharMatches(buf, off + minBPC*2, 'o')
                                && CharMatches(buf, off + minBPC*3, 's')
                                && CharMatches(buf, off + minBPC*4, ';'))
                            {
                                token.TokenEnd = off + minBPC*5;
                                token.RefChar1 = '\'';
                                return true;
                            }
                            break;
                    }
                    break;
                case 'l':
                    if (end - off >= minBPC*3
                        && CharMatches(buf, off + minBPC, 't')
                        && CharMatches(buf, off + minBPC*2, ';'))
                    {
                        token.TokenEnd = off + minBPC*3;
                        token.RefChar1 = '<';
                        return true;
                    }
                    break;
                case 'g':
                    if (end - off >= minBPC*3
                        && CharMatches(buf, off + minBPC, 't')
                        && CharMatches(buf, off + minBPC*2, ';'))
                    {
                        token.TokenEnd = off + minBPC*3;
                        token.RefChar1 = '>';
                        return true;
                    }
                    break;
                case 'q':
                    if (end - off >= minBPC*5
                        && CharMatches(buf, off + minBPC, 'u')
                        && CharMatches(buf, off + minBPC*2, 'o')
                        && CharMatches(buf, off + minBPC*3, 't')
                        && CharMatches(buf, off + minBPC*4, ';'))
                    {
                        token.TokenEnd = off + minBPC*5;
                        token.RefChar1 = '"';
                        return true;
                    }
                    break;
            }
            return false;
        }

        /* off points to character following "&" */

        private Tokens ScanRef(byte[] buf, int off, int end, Token token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            if (IsMagicEntityRef(buf, off, end, token))
                return Tokens.MagicEntityReference;
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                case BtNum:
                    return ScanCharRef(buf, off + minBPC, end, token);
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtSemi:
                        token.NameEnd = off;
                        token.TokenEnd = off + minBPC;
                        return Tokens.EntityReference;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* off points to character following first character of
           attribute name */

        private Tokens ScanAtts(int nameStart, byte[] buf, int off, int end,
                             ContentToken token)
        {
            int NameEnd = -1;
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtS:
                    case BtCr:
                    case BtLf:
                        NameEnd = off;
                        for (;;)
                        {
                            off += minBPC;
                            if (off == end)
                                return Tokens.PartialToken; //throw new PartialTokenException();
                            switch (ByteType(buf, off))
                            {
                                case BtEquals:
                                    goto loop;
                                case BtS:
                                case BtLf:
                                case BtCr:
                                    break;
                                default:
                                    throw new InvalidTokenException(off);
                            }
                        }
                        loop:
                        ;
                        /* fall through */
                        goto case BtEquals;
                    case BtEquals:
                        {
                            if (NameEnd < 0)
                                NameEnd = off;
                            int open;
                            for (;;)
                            {

                                off += minBPC;
                                if (off == end)
                                    return Tokens.PartialToken; //throw new PartialTokenException();
                                open = ByteType(buf, off);
                                if (open == BtQuot || open == BtApos)
                                    break;
                                switch (open)
                                {
                                    case BtS:
                                    case BtLf:
                                    case BtCr:
                                        break;
                                    default:
                                        throw new InvalidTokenException(off);
                                }
                            }
                            off += minBPC;
                            int valueStart = off;
                            bool normalized = true;
                            int t;
                            /* in attribute value */
                            for (;;)
                            {
                                if (off == end)
                                    return Tokens.PartialToken; //throw new PartialTokenException();
                                t = ByteType(buf, off);
                                if (t == open)
                                    break;
                                switch (t)
                                {
                                    case BtNoXml:
                                    case BtMalform:
                                        throw new InvalidTokenException(off);
                                    case BtLead2:
                                        if (end - off < 2)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        Check2(buf, off);
                                        off += 2;
                                        break;
                                    case BtLead3:
                                        if (end - off < 3)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        Check3(buf, off);
                                        off += 3;
                                        break;
                                    case BtLead4:
                                        if (end - off < 4)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        Check4(buf, off);
                                        off += 4;
                                        break;
                                    case BtAmp:
                                        {
                                            normalized = false;
                                            int saveNameEnd = token.NameEnd;

                                            var retTok = ScanRef(buf, off + minBPC, end, token);
                                            if (retTok == Tokens.PartialToken
                                                || retTok == Tokens.PartialChar)
                                                return retTok;

                                            token.NameEnd = saveNameEnd;
                                            off = token.TokenEnd;
                                            break;
                                        }
                                    case BtS:
                                        if (normalized
                                            && (off == valueStart
                                                || ByteToAscii(buf, off) != ' '
                                                || (off + minBPC != end
                                                    && (ByteToAscii(buf, off + minBPC) == ' '
                                                        || ByteType(buf, off + minBPC) == open))))
                                            normalized = false;
                                        off += minBPC;
                                        break;
                                    case BtLt:
                                        throw new InvalidTokenException(off);
                                    case BtLf:
                                    case BtCr:
                                        normalized = false;
                                        /* fall through */
                                        goto default;
                                    default:
                                        off += minBPC;
                                        break;
                                }
                            }
                            token.AppendAttribute(nameStart, NameEnd, valueStart,
                                                  off,
                                                  normalized);
                            off += minBPC;
                            if (off == end)
                                return Tokens.PartialToken; //throw new PartialTokenException();
                            t = ByteType(buf, off);
                            switch (t)
                            {
                                case BtS:
                                case BtCr:
                                case BtLf:
                                    off += minBPC;
                                    if (off == end)
                                        return Tokens.PartialToken; //throw new PartialTokenException();
                                    t = ByteType(buf, off);
                                    break;
                                case BtGt:
                                case BtSol:
                                    break;
                                default:
                                    throw new InvalidTokenException(off);
                            }
                            /* off points to closing quote */
                            for (;;)
                            {
                                switch (t)
                                {
                                    case BtNmstrt:
                                        nameStart = off;
                                        off += minBPC;
                                        goto skipToName;
                                    case BtLead2:
                                        if (end - off < 2)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        if (ByteType2(buf, off) != BtNmstrt)
                                            throw new InvalidTokenException(off);
                                        nameStart = off;
                                        off += 2;
                                        goto skipToName;
                                    case BtLead3:
                                        if (end - off < 3)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        if (ByteType3(buf, off) != BtNmstrt)
                                            throw new InvalidTokenException(off);
                                        nameStart = off;
                                        off += 3;
                                        goto skipToName;
                                    case BtLead4:
                                        if (end - off < 4)
                                            return Tokens.PartialChar; //throw new PartialCharException(off);
                                        if (ByteType4(buf, off) != BtNmstrt)
                                            throw new InvalidTokenException(off);
                                        nameStart = off;
                                        off += 4;
                                        goto skipToName;
                                    case BtS:
                                    case BtCr:
                                    case BtLf:
                                        break;
                                    case BtGt:
                                        token.CheckAttributeUniqueness(buf);
                                        token.TokenEnd = off + minBPC;
                                        return Tokens.StartTagWithAtts;
                                    case BtSol:
                                        off += minBPC;
                                        if (off == end)
                                            return Tokens.PartialToken; //throw new PartialTokenException();
                                        CheckCharMatches(buf, off, '>');
                                        token.CheckAttributeUniqueness(buf);
                                        token.TokenEnd = off + minBPC;
                                        return Tokens.EmptyElementWithAtts;
                                    default:
                                        throw new InvalidTokenException(off);
                                }
                                off += minBPC;
                                if (off == end)
                                    return Tokens.PartialToken; //throw new PartialTokenException();
                                t = ByteType(buf, off);
                            }

                            skipToName:
                            NameEnd = -1;
                            break;
                        }
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* off points to character following "<" */

        private Tokens ScanLt(byte[] buf, int off, int end, ContentToken token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                case BtExcl:
                    if ((off += minBPC) == end)
                        return Tokens.PartialToken; //throw new PartialTokenException();
                    switch (ByteType(buf, off))
                    {
                        case BtMinus:
                            return ScanComment(buf, off + minBPC, end, token);
                        case BtLsqb:
                            return ScanCdataSection(buf, off + minBPC, end, token);
                    }
                    throw new InvalidTokenException(off);
                case BtQuest:
                    return ScanPi(buf, off + minBPC, end, token);
                case BtSol:
                    return ScanEndTag(buf, off + minBPC, end, token);
                default:
                    throw new InvalidTokenException(off);
            }
            /* we have a start-tag */
            token.NameEnd = -1;
            token.ClearAttributes();
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtS:
                    case BtCr:
                    case BtLf:
                        token.NameEnd = off;
                        off += minBPC;
                        for (;;)
                        {
                            if (off == end)
                                return Tokens.PartialToken; //throw new PartialTokenException();
                            switch (ByteType(buf, off))
                            {
                                case BtNmstrt:
                                    return ScanAtts(off, buf, off + minBPC, end, token);
                                case BtLead2:
                                    if (end - off < 2)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    if (ByteType2(buf, off) != BtNmstrt)
                                        throw new InvalidTokenException(off);
                                    return ScanAtts(off, buf, off + 2, end, token);
                                case BtLead3:
                                    if (end - off < 3)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    if (ByteType3(buf, off) != BtNmstrt)
                                        throw new InvalidTokenException(off);
                                    return ScanAtts(off, buf, off + 3, end, token);
                                case BtLead4:
                                    if (end - off < 4)
                                        return Tokens.PartialChar; //throw new PartialCharException(off);
                                    if (ByteType4(buf, off) != BtNmstrt)
                                        throw new InvalidTokenException(off);
                                    return ScanAtts(off, buf, off + 4, end, token);
                                case BtGt:
                                case BtSol:
                                    goto loop;
                                case BtS:
                                case BtCr:
                                case BtLf:
                                    off += minBPC;
                                    break;
                                default:
                                    throw new InvalidTokenException(off);
                            }
                        }
                        loop:
                        break;
                    case BtGt:
                        if (token.NameEnd < 0)
                            token.NameEnd = off;
                        token.TokenEnd = off + minBPC;
                        return Tokens.StartTagNoAtts;
                    case BtSol:
                        if (token.NameEnd < 0)
                            token.NameEnd = off;
                        off += minBPC;
                        if (off == end)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        CheckCharMatches(buf, off, '>');
                        token.TokenEnd = off + minBPC;
                        return Tokens.EmptyElementNoAtts;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /* BPC with UTF-8 in XMPP is always 1, so we don't need thif function
        // Ensure that we always scan a multiple of minBPC bytes.
        private int adjustEnd(int off, int end)
        {        
            int n = end - off;
            if ((n & (minBPC - 1)) != 0)
            {
                n &= ~(minBPC - 1);
                if (n == 0)
                    throw new PartialCharException(off);
                return off + n;
            }
            else
                return end;
        }
        */

        /**
         * Scans the first token of a byte subarrary that contains content.
         * Returns one of the following integers according to the type of token
         * that the subarray starts with:
         * <ul>
         * <li><code>TOK.START_TAG_NO_ATTS</code></li>
         * <li><code>TOK.START_TAG_WITH_ATTS</code></li>
         * <li><code>TOK.EMPTY_ELEMENT_NO_ATTS</code></li>
         * <li><code>TOK.EMPTY_ELEMENT_WITH_ATTS</code></li>
         * <li><code>TOK.END_TAG</code></li>
         * <li><code>TOK.DATA_CHARS</code></li>
         * <li><code>TOK.DATA_NEWLINE</code></li>
         * <li><code>TOK.CDATA_SECT_OPEN</code></li>
         * <li><code>TOK.ENTITY_REF</code></li>
         * <li><code>TOK.MAGIC_ENTITY_REF</code></li>
         * <li><code>TOK.CHAR_REF</code></li>
         * <li><code>TOK.CHAR_PAIR_REF</code></li>
         * <li><code>TOK.ProcessingInstruction</code></li>
         * <li><code>TOK.XML_DECL</code></li>
         * <li><code>TOK.COMMENT</code></li>
         * </ul>
         * <p>
         * Information about the token is stored in <code>token</code>.
         * </p>
         * When <code>TOK.CDATA_SECT_OPEN</code> is returned,
         * <code>tokenizeCdataSection</code> should be called until
         * it returns <code>TOK.CDATA_SECT</code>.
         *
         * @exception EmptyTokenException if the subarray is empty
         * @exception PartialTokenException if the subarray contains only part of
         * a legal token
         * @exception InvalidTokenException if the subarrary does not start
         * with a legal token or part of one
         * @exception ExtensibleTokenException if the subarray encodes just a carriage
         * return ('\r')
         *
         * @see #TOK.START_TAG_NO_ATTS
         * @see #TOK.START_TAG_WITH_ATTS
         * @see #TOK.EMPTY_ELEMENT_NO_ATTS
         * @see #TOK.EMPTY_ELEMENT_WITH_ATTS
         * @see #TOK.END_TAG
         * @see #TOK.DATA_CHARS
         * @see #TOK.DATA_NEWLINE
         * @see #TOK.CDATA_SECT_OPEN
         * @see #TOK.ENTITY_REF
         * @see #TOK.MAGIC_ENTITY_REF
         * @see #TOK.CHAR_REF
         * @see #TOK.CHAR_PAIR_REF
         * @see #TOK.ProcessingInstruction
         * @see #TOK.XML_DECL
         * @see #TOK.COMMENT
         * @see ContentToken
         * @see EmptyTokenException
         * @see PartialTokenException
         * @see InvalidTokenException
         * @see ExtensibleTokenException
         * @see #tokenizeCdataSection
         */

        public Tokens TokenizeContent(byte[] buf, int off, int end,
                                   ContentToken token)
        {
            /* BPC with UTF-8 in XMPP is always 1
            if (minBPC > 1)
                end = adjustEnd(off, end);
            */
            if (off == end)
                throw new EmptyTokenException();
            switch (ByteType(buf, off))
            {
                case BtLt:
                    return ScanLt(buf, off + minBPC, end, token);
                case BtAmp:
                    return ScanRef(buf, off + minBPC, end, token);
                case BtCr:
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_NEWLINE);
                    if (ByteType(buf, off) == BtLf)
                        off += minBPC;
                    token.TokenEnd = off;
                    return Tokens.DataNewline;
                case BtLf:
                    token.TokenEnd = off + minBPC;
                    return Tokens.DataNewline;
                case BtRsqb:
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_CHARS);
                    if (!CharMatches(buf, off, ']'))
                        break;
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_CHARS);
                    if (!CharMatches(buf, off, '>'))
                    {
                        off -= minBPC;
                        break;
                    }
                    throw new InvalidTokenException(off);
                case BtNoXml:
                case BtMalform:
                    throw new InvalidTokenException(off);
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check2(buf, off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check3(buf, off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    Check4(buf, off);
                    off += 4;
                    break;
                default:
                    off += minBPC;
                    break;
            }
            token.TokenEnd = ExtendData(buf, off, end);
            return Tokens.DataChars;
        }

        private int ExtendData(byte[] buf, int off, int end)
        {
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtLead2:
                        if (end - off < 2)
                            return off;
                        Check2(buf, off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return off;
                        Check3(buf, off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return off;
                        Check4(buf, off);
                        off += 4;
                        break;
                    case BtRsqb:
                    case BtAmp:
                    case BtLt:
                    case BtNoXml:
                    case BtMalform:
                    case BtCr:
                    case BtLf:
                        return off;
                    default:
                        off += minBPC;
                        break;
                }
            }
            return off;
        }

        /* off points to character following "%" */

        private Tokens ScanPercent(byte[] buf, int off, int end, Token token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                case BtS:
                case BtLf:
                case BtCr:
                case BtPercnt:
                    token.TokenEnd = off;
                    return Tokens.Percent;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtSemi:
                        token.NameEnd = off;
                        token.TokenEnd = off + minBPC;
                        return Tokens.ParamEntityReference;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }


        private Tokens ScanPoundName(byte[] buf, int off, int end, Token token)
        {
            if (off == end)
                return Tokens.PartialToken; //throw new PartialTokenException();
            switch (ByteType(buf, off))
            {
                case BtNmstrt:
                    off += minBPC;
                    break;
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType2(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 2;
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType3(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 3;
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    if (ByteType4(buf, off) != BtNmstrt)
                        throw new InvalidTokenException(off);
                    off += 4;
                    break;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtCr:
                    case BtLf:
                    case BtS:
                    case BtRpar:
                    case BtGt:
                    case BtPercnt:
                    case BtVerbar:
                        token.TokenEnd = off;
                        return Tokens.PoundName;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.POUND_NAME);
        }

        private Tokens ScanLit(int open, byte[] buf, int off, int end, Token token)
        {
            while (off != end)
            {
                int t = ByteType(buf, off);
                switch (t)
                {
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        Check2(buf, off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        Check3(buf, off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        Check4(buf, off);
                        off += 4;
                        break;
                    case BtNoXml:
                    case BtMalform:
                        throw new InvalidTokenException(off);
                    case BtQuot:
                    case BtApos:
                        off += minBPC;
                        if (t != open)
                            break;
                        if (off == end)
                            return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.LITERAL);
                        switch (ByteType(buf, off))
                        {
                            case BtS:
                            case BtCr:
                            case BtLf:
                            case BtGt:
                            case BtPercnt:
                            case BtLsqb:
                                token.TokenEnd = off;
                                return Tokens.Literal;
                            default:
                                throw new InvalidTokenException(off);
                        }
                    default:
                        off += minBPC;
                        break;
                }
            }
            return Tokens.PartialToken; //throw new PartialTokenException();
        }

        /**
         * Returns an encoding object to be used to start parsing an
         * external entity.  The encoding is chosen based on the
         * initial 4 bytes of the entity.
         * 
         * @param buf the byte array containing the initial bytes of
         * the entity @param off the index in <code>buf</code> of the
         * first byte of the entity @param end the index in
         * <code>buf</code> following the last available byte of the
         * entity; <code>end - off</code> must be greater than or
         * equal to 4 unless the entity has fewer that 4 bytes, in
         * which case it must be equal to the length of the entity
         * @param token receives information about the presence of a
         * byte order mark; if the entity starts with a byte order
         * mark then <code>token.getTokenEnd()</code> will return
         * <code>off + 2</code>, otherwise it will return
         * <code>off</code>
         *
         * @see TextDecl
         * @see XmlDeclaration
         * @see #TOK.XML_DECL
         * @see #getEncoding
         * @see #getInternalEncoding
         */

        public static Encoding GetInitialEncoding(byte[] buf, int off, int end,
                                                  Token token)
        {
            token.TokenEnd = off;
            switch (end - off)
            {
                case 0:
                    break;
                case 1:
                    if (buf[off] > 127)
                        return null;
                    break;
                default:
                    int b0 = buf[off] & 0xFF;
                    int b1 = buf[off + 1] & 0xFF;
                    switch ((b0 << 8) | b1)
                    {
                        case 0xFEFF:
                            token.TokenEnd = off + 2;
                            /* fall through */
                            goto case '<';
                        case '<': /* not legal; but not a fatal error */
                            return GetEncoding(UTF16_BIG_ENDIAN_ENCODING);
                        case 0xFFFE:
                            token.TokenEnd = off + 2;
                            /* fall through */
                            goto case '<' << 8;
                        case '<' << 8: /* not legal; but not a fatal error */
                            return GetEncoding(UTF16_LITTLE_ENDIAN_ENCODING);
                    }
                    break;
            }
            return GetEncoding(UTF8_ENCODING);
        }

        /**
         * Returns an <code>Encoding</code> corresponding to the
         * specified IANA character set name.  Returns this
         * <code>Encoding</code> if the name is null.  Returns null if
         * the specified encoding is not supported.  Note that there
         * are two distinct <code>Encoding</code> objects associated
         * with the name <code>UTF-16</code>, one for each possible
         * byte order; if this <code>Encoding</code> is UTF-16 with
         * little-endian byte ordering, then
         * <code>getEncoding("UTF-16")</code> will return this,
         * otherwise it will return an <code>Encoding</code> for
         * UTF-16 with big-endian byte ordering.  @param name a string
         * specifying the IANA name of the encoding; this is case
         * insensitive
         */

        public Encoding GetEncoding(string name)
        {
            if (name == null)
                return this;

            switch (name.ToUpper())
            {
                case "UTF-8":
                    return GetEncoding(UTF8_ENCODING);
                    /*
            case "UTF-16":
                return getUTF16Encoding();
            case "ISO-8859-1":
                return getEncoding(ISO8859_1_ENCODING);
            case "US-ASCII":
                return getEncoding(ASCII_ENCODING);
                */
            }
            return null;
        }

        /**
         * Returns an <code>Encoding</code> for entities encoded with
         * a single-byte encoding (an encoding in which each byte
         * represents exactly one character).  @param map a string
         * specifying the character represented by each byte; the
         * string must have a length of 256;
         * <code>map.charAt(b)</code> specifies the character encoded
         * by byte <code>b</code>; bytes that do not represent any
         * character should be mapped to <code>\uFFFD</code>
         */

        public Encoding GetSingleByteEncoding(string map)
        {
            //return new SingleByteEncoding(map);

            throw new System.NotImplementedException();
        }

        /**
         * Returns an <code>Encoding</code> object for use with
         * internal entities.  This is a UTF-16 big endian encoding,
         * except that newlines are assumed to have been normalized
         * into line feed, so carriage return is treated like a space.
         */

        public static Encoding GetInternalEncoding()
        {
            return GetEncoding(INTERNAL_ENCODING);
        }

        /**
         * Scans the first token of a byte subarray that contains part of a
         * prolog.
         * Returns one of the following integers according to the type of token
         * that the subarray starts with:
         * <ul>
         * <li><code>TOK.ProcessingInstruction</code></li>
         * <li><code>TOK.XML_DECL</code></li>
         * <li><code>TOK.COMMENT</code></li>
         * <li><code>TOK.PARAM_ENTITY_REF</code></li>
         * <li><code>TOK.PROLOG_S</code></li>
         * <li><code>TOK.DECL_OPEN</code></li>
         * <li><code>TOK.DECL_CLOSE</code></li>
         * <li><code>TOK.NAME</code></li>
         * <li><code>TOK.NMTOKEN</code></li>
         * <li><code>TOK.POUND_NAME</code></li>
         * <li><code>TOK.OR</code></li>
         * <li><code>TOK.PERCENT</code></li>
         * <li><code>TOK.OPEN_PAREN</code></li>
         * <li><code>TOK.CLOSE_PAREN</code></li>
         * <li><code>TOK.OPEN_BRACKET</code></li>
         * <li><code>TOK.CLOSE_BRACKET</code></li>
         * <li><code>TOK.LITERAL</code></li>
         * <li><code>TOK.NAME_QUESTION</code></li>
         * <li><code>TOK.NAME_ASTERISK</code></li>
         * <li><code>TOK.NAME_PLUS</code></li>
         * <li><code>TOK.COND_SECT_OPEN</code></li>
         * <li><code>TOK.COND_SECT_CLOSE</code></li>
         * <li><code>TOK.CLOSE_PAREN_QUESTION</code></li>
         * <li><code>TOK.CLOSE_PAREN_ASTERISK</code></li>
         * <li><code>TOK.CLOSE_PAREN_PLUS</code></li>
         * <li><code>TOK.COMMA</code></li>
         * </ul>
         * @exception EmptyTokenException if the subarray is empty
         * @exception PartialTokenException if the subarray contains only part of
         * a legal token
         * @exception InvalidTokenException if the subarrary does not start
         * with a legal token or part of one
         * @exception EndOfPrologException if the subarray starts with the document
         * element; <code>tokenizeContent</code> should be used on the remainder
         * of the entity
         * @exception ExtensibleTokenException if the subarray is a legal token
         * but subsequent bytes in the same entity could be part of the token
         * @see #TOK.ProcessingInstruction
         * @see #TOK.XML_DECL
         * @see #TOK.COMMENT
         * @see #TOK.PARAM_ENTITY_REF
         * @see #TOK.PROLOG_S
         * @see #TOK.DECL_OPEN
         * @see #TOK.DECL_CLOSE
         * @see #TOK.NAME
         * @see #TOK.NMTOKEN
         * @see #TOK.POUND_NAME
         * @see #TOK.OR
         * @see #TOK.PERCENT
         * @see #TOK.OPEN_PAREN
         * @see #TOK.CLOSE_PAREN
         * @see #TOK.OPEN_BRACKET
         * @see #TOK.CLOSE_BRACKET
         * @see #TOK.LITERAL
         * @see #TOK.NAME_QUESTION
         * @see #TOK.NAME_ASTERISK
         * @see #TOK.NAME_PLUS
         * @see #TOK.COND_SECT_OPEN
         * @see #TOK.COND_SECT_CLOSE
         * @see #TOK.CLOSE_PAREN_QUESTION
         * @see #TOK.CLOSE_PAREN_ASTERISK
         * @see #TOK.CLOSE_PAREN_PLUS
         * @see #TOK.COMMA
         * @see ContentToken
         * @see EmptyTokenException
         * @see PartialTokenException
         * @see InvalidTokenException
         * @see ExtensibleTokenException
         * @see EndOfPrologException
         */

        public Tokens TokenizeProlog(byte[] buf, int off, int end, Token token)
        {
            Tokens tok;
            /* BPC with UTF-8 in XMPP is always 1
            if (minBPC > 1)
                end = adjustEnd(off, end);
            */
            if (off == end)
                throw new EmptyTokenException();
            switch (ByteType(buf, off))
            {
                case BtQuot:
                    return ScanLit(BtQuot, buf, off + minBPC, end, token);
                case BtApos:
                    return ScanLit(BtApos, buf, off + minBPC, end, token);
                case BtLt:
                    {
                        off += minBPC;
                        if (off == end)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        switch (ByteType(buf, off))
                        {
                            case BtExcl:
                                return ScanDecl(buf, off + minBPC, end, token);
                            case BtQuest:
                                return ScanPi(buf, off + minBPC, end, token);
                            case BtNmstrt:
                            case BtLead2:
                            case BtLead3:
                            case BtLead4:
                                token.TokenEnd = off - minBPC;
                                throw new EndOfPrologException();
                        }
                        throw new InvalidTokenException(off);
                    }
                case BtCr:
                    if (off + minBPC == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.PROLOG_S);
                    /* fall through */
                    goto case BtS;
                case BtS:
                case BtLf:
                    for (;;)
                    {
                        off += minBPC;
                        if (off == end)
                            break;
                        switch (ByteType(buf, off))
                        {
                            case BtS:
                            case BtLf:
                                break;
                            case BtCr:
                                /* don't split CR/LF pair */
                                if (off + minBPC != end)
                                    break;
                                /* fall through */
                                goto default;
                            default:
                                token.TokenEnd = off;
                                return Tokens.PrologS;
                        }
                    }
                    token.TokenEnd = off;
                    return Tokens.PrologS;
                case BtPercnt:
                    return ScanPercent(buf, off + minBPC, end, token);
                case BtComma:
                    token.TokenEnd = off + minBPC;
                    return Tokens.Comma;
                case BtLsqb:
                    token.TokenEnd = off + minBPC;
                    return Tokens.OpenBracket;
                case BtRsqb:
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.CLOSE_BRACKET);
                    if (CharMatches(buf, off, ']'))
                    {
                        if (off + minBPC == end)
                            return Tokens.PartialToken; //throw new PartialTokenException();
                        if (CharMatches(buf, off + minBPC, '>'))
                        {
                            token.TokenEnd = off + 2*minBPC;
                            return Tokens.CondSectClose;
                        }
                    }
                    token.TokenEnd = off;
                    return Tokens.CloseBracket;
                case BtLpar:
                    token.TokenEnd = off + minBPC;
                    return Tokens.OpenParen;
                case BtRpar:
                    off += minBPC;
                    if (off == end)
                        return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.CLOSE_PAREN);
                    switch (ByteType(buf, off))
                    {
                        case BtAst:
                            token.TokenEnd = off + minBPC;
                            return Tokens.CloseParenAsterisk;
                        case BtQuest:
                            token.TokenEnd = off + minBPC;
                            return Tokens.CloseParenQuestion;
                        case BtPlus:
                            token.TokenEnd = off + minBPC;
                            return Tokens.CloseParenPlus;
                        case BtCr:
                        case BtLf:
                        case BtS:
                        case BtGt:
                        case BtComma:
                        case BtVerbar:
                        case BtRpar:
                            token.TokenEnd = off;
                            return Tokens.CloseParen;
                    }
                    throw new InvalidTokenException(off);
                case BtVerbar:
                    token.TokenEnd = off + minBPC;
                    return Tokens.Or;
                case BtGt:
                    token.TokenEnd = off + minBPC;
                    return Tokens.DeclClose;
                case BtNum:
                    return ScanPoundName(buf, off + minBPC, end, token);
                case BtLead2:
                    if (end - off < 2)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    switch (ByteType2(buf, off))
                    {
                        case BtNmstrt:
                            off += 2;
                            tok = Tokens.Name;
                            break;
                        case BtName:
                            off += 2;
                            tok = Tokens.Nmtoken;
                            break;
                        default:
                            throw new InvalidTokenException(off);
                    }
                    break;
                case BtLead3:
                    if (end - off < 3)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    switch (ByteType3(buf, off))
                    {
                        case BtNmstrt:
                            off += 3;
                            tok = Tokens.Name;
                            break;
                        case BtName:
                            off += 3;
                            tok = Tokens.Nmtoken;
                            break;
                        default:
                            throw new InvalidTokenException(off);
                    }
                    break;
                case BtLead4:
                    if (end - off < 4)
                        return Tokens.PartialChar; //throw new PartialCharException(off);
                    switch (ByteType4(buf, off))
                    {
                        case BtNmstrt:
                            off += 4;
                            tok = Tokens.Name;
                            break;
                        case BtName:
                            off += 4;
                            tok = Tokens.Nmtoken;
                            break;
                        default:
                            throw new InvalidTokenException(off);
                    }
                    break;
                case BtNmstrt:
                    tok = Tokens.Name;
                    off += minBPC;
                    break;
                case BtName:
                case BtMinus:
                    tok = Tokens.Nmtoken;
                    off += minBPC;
                    break;
                default:
                    throw new InvalidTokenException(off);
            }
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtNmstrt:
                    case BtName:
                    case BtMinus:
                        off += minBPC;
                        break;
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar2(buf, off))
                            throw new InvalidTokenException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar3(buf, off))
                            throw new InvalidTokenException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        if (!IsNameChar4(buf, off))
                            throw new InvalidTokenException(off);
                        off += 4;
                        break;
                    case BtGt:
                    case BtRpar:
                    case BtComma:
                    case BtVerbar:
                    case BtLsqb:
                    case BtPercnt:
                    case BtS:
                    case BtCr:
                    case BtLf:
                        token.TokenEnd = off;
                        return tok;
                    case BtPlus:
                        if (tok != Tokens.Name)
                            throw new InvalidTokenException(off);
                        token.TokenEnd = off + minBPC;
                        return Tokens.NamePlus;
                    case BtAst:
                        if (tok != Tokens.Name)
                            throw new InvalidTokenException(off);
                        token.TokenEnd = off + minBPC;
                        return Tokens.NameAsterisk;
                    case BtQuest:
                        if (tok != Tokens.Name)
                            throw new InvalidTokenException(off);
                        token.TokenEnd = off + minBPC;
                        return Tokens.NameQuestion;
                    default:
                        throw new InvalidTokenException(off);
                }
            }
            return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(tok);
        }

        /**
         * Scans the first token of a byte subarrary that contains part of
         * literal attribute value.  The opening and closing delimiters
         * are not included in the subarrary.
         * Returns one of the following integers according to the type of
         * token that the subarray starts with:
         * <ul>
         * <li><code>TOK.DATA_CHARS</code></li>
         * <li><code>TOK.DATA_NEWLINE</code></li>
         * <li><code>TOK.ATTRIBUTE_VALUE_S</code></li>
         * <li><code>TOK.MAGIC_ENTITY_REF</code></li>
         * <li><code>TOK.ENTITY_REF</code></li>
         * <li><code>TOK.CHAR_REF</code></li>
         * <li><code>TOK.CHAR_PAIR_REF</code></li>
         * </ul>
         * @exception EmptyTokenException if the subarray is empty
         * @exception PartialTokenException if the subarray contains only part of
         * a legal token
         * @exception InvalidTokenException if the subarrary does not start
         * with a legal token or part of one
         * @exception ExtensibleTokenException if the subarray encodes just a carriage
         * return ('\r')
         * @see #TOK.DATA_CHARS
         * @see #TOK.DATA_NEWLINE
         * @see #TOK.ATTRIBUTE_VALUE_S
         * @see #TOK.MAGIC_ENTITY_REF
         * @see #TOK.ENTITY_REF
         * @see #TOK.CHAR_REF
         * @see #TOK.CHAR_PAIR_REF
         * @see Token
         * @see EmptyTokenException
         * @see PartialTokenException
         * @see InvalidTokenException
         * @see ExtensibleTokenException
         */

        public Tokens TokenizeAttributeValue(byte[] buf, int off, int end, Token token)
        {
            /* BPC with UTF-8 in XMPP is always 1
            if (minBPC > 1)
                end = adjustEnd(off, end);
            */
            if (off == end)
                throw new EmptyTokenException();
            int start = off;
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 4;
                        break;
                    case BtAmp:
                        if (off == start)
                            return ScanRef(buf, off + minBPC, end, token);
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtLt:
                        /* this is for inside entity references */
                        throw new InvalidTokenException(off);
                    case BtS:
                        if (off == start)
                        {
                            token.TokenEnd = off + minBPC;
                            return Tokens.AttributeValueS;
                        }
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtLf:
                        if (off == start)
                        {
                            token.TokenEnd = off + minBPC;
                            return Tokens.DataNewline;
                        }
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtCr:
                        if (off == start)
                        {
                            off += minBPC;
                            if (off == end)
                                return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_NEWLINE);
                            if (ByteType(buf, off) == BtLf)
                                off += minBPC;
                            token.TokenEnd = off;
                            return Tokens.DataNewline;
                        }
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    default:
                        off += minBPC;
                        break;
                }
            }
            token.TokenEnd = off;
            return Tokens.DataChars;
        }

        /**
         * Scans the first token of a byte subarrary that contains part of
         * literal entity value.  The opening and closing delimiters
         * are not included in the subarrary.
         * Returns one of the following integers according to the type of
         * token that the subarray starts with:
         * <ul>
         * <li><code>TOK.DATA_CHARS</code></li>
         * <li><code>TOK.DATA_NEWLINE</code></li>
         * <li><code>TOK.PARAM_ENTITY_REF</code></li>
         * <li><code>TOK.MAGIC_ENTITY_REF</code></li>
         * <li><code>TOK.ENTITY_REF</code></li>
         * <li><code>TOK.CHAR_REF</code></li>
         * <li><code>TOK.CHAR_PAIR_REF</code></li>
         * </ul>
         * @exception EmptyTokenException if the subarray is empty
         * @exception PartialTokenException if the subarray contains only part of
         * a legal token
         * @exception InvalidTokenException if the subarrary does not start
         * with a legal token or part of one
         * @exception ExtensibleTokenException if the subarray encodes just a carriage
         * return ('\r')
         * @see #TOK.DATA_CHARS
         * @see #TOK.DATA_NEWLINE
         * @see #TOK.MAGIC_ENTITY_REF
         * @see #TOK.ENTITY_REF
         * @see #TOK.PARAM_ENTITY_REF
         * @see #TOK.CHAR_REF
         * @see #TOK.CHAR_PAIR_REF
         * @see Token
         * @see EmptyTokenException
         * @see PartialTokenException
         * @see InvalidTokenException
         * @see ExtensibleTokenException
         */

        public Tokens TokenizeEntityValue(byte[] buf, int off, int end,
                                       Token token)
        {
            /* BPC with UTF-8 in XMPP is always 1
            if (minBPC > 1)
                end = adjustEnd(off, end);
            */
            if (off == end)
                throw new EmptyTokenException();
            int start = off;
            while (off != end)
            {
                switch (ByteType(buf, off))
                {
                    case BtLead2:
                        if (end - off < 2)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 2;
                        break;
                    case BtLead3:
                        if (end - off < 3)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 3;
                        break;
                    case BtLead4:
                        if (end - off < 4)
                            return Tokens.PartialChar; //throw new PartialCharException(off);
                        off += 4;
                        break;
                    case BtAmp:
                        if (off == start)
                            return ScanRef(buf, off + minBPC, end, token);
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtPercnt:
                        if (off == start)
                            return ScanPercent(buf, off + minBPC, end, token);
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtLf:
                        if (off == start)
                        {
                            token.TokenEnd = off + minBPC;
                            return Tokens.DataNewline;
                        }
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    case BtCr:
                        if (off == start)
                        {
                            off += minBPC;
                            if (off == end)
                                return Tokens.ExtensibleToken; //throw new ExtensibleTokenException(TOK.DATA_NEWLINE);
                            if (ByteType(buf, off) == BtLf)
                                off += minBPC;
                            token.TokenEnd = off;
                            return Tokens.DataNewline;
                        }
                        token.TokenEnd = off;
                        return Tokens.DataChars;
                    default:
                        off += minBPC;
                        break;
                }
            }
            token.TokenEnd = off;
            return Tokens.DataChars;
        }

        /**
         * Skips over an ignored conditional section.
         * The subarray starts following the <code>&lt;![ IGNORE [</code>.
         *
         * @return the index of the character following the closing
         * <code>]]&gt;</code>
         *
         * @exception PartialTokenException if the subarray does not contain the
         * complete ignored conditional section
         * @exception InvalidTokenException if the ignored conditional section
         * contains illegal characters
         */
        /*
        public int skipIgnoreSect(byte[] buf, int off, int end)
        {
            if (minBPC > 1)
                end = adjustEnd(off, end);
            int level = 0;
            while (off != end)
            {
                switch (byteType(buf, off))
                {
                case BT_LEAD2:
                    if (end - off < 2)
                        throw new PartialCharException(off);
                    check2(buf, off);
                    off += 2;
                    break;
                case BT_LEAD3:
                    if (end - off < 3)
                        throw new PartialCharException(off);
                    check3(buf, off);
                    off += 3;
                    break;
                case BT_LEAD4:
                    if (end - off < 4)
                        throw new PartialCharException(off);
                    check4(buf, off);
                    off += 4;
                    break;
                case BtNoXml:
                case BT_MALFORM:
                    throw new InvalidTokenException(off);
                case BT_LT:
                    off += minBPC;
                    if (off == end)
                        goto loop;
                    if (!charMatches(buf, off, '!'))
                        break;
                    off += minBPC;
                    if (off == end)
                        goto loop;
                    if (!charMatches(buf, off, '['))
                        break;
                    level++;
                    off += minBPC;
                    break;
                case BT_RSQB:
                    off += minBPC;
                    if (off == end)
                        goto loop;
                    if (!charMatches(buf, off, ']'))
                        break;
                    off += minBPC;
                    if (off == end)
                        goto loop;
                    if (charMatches(buf, off, '>')) {
                        if (level == 0)
                            return off + minBPC;
                        level--;
                    }
                    else if (charMatches(buf, off, ']'))
                        break;
                    off += minBPC;
                    break;
                default:
                    off += minBPC;
                    break;
                }
            }
        loop:
            throw new PartialTokenException();
        }
        */

        /**
         * Checks that a literal contained in the specified byte subarray
         * is a legal public identifier and returns a string with
         * the normalized content of the public id.
         * The subarray includes the opening and closing quotes.
         * @exception InvalidTokenException if it is not a legal public identifier
         */

        public string GetPublicId(byte[] buf, int off, int end)
        {
            System.Text.StringBuilder sbuf = new System.Text.StringBuilder();
            off += minBPC;
            end -= minBPC;
            for (; off != end; off += minBPC)
            {
                char c = (char) ByteToAscii(buf, off);
                switch (ByteType(buf, off))
                {
                    case BtMinus:
                    case BtApos:
                    case BtLpar:
                    case BtRpar:
                    case BtPlus:
                    case BtComma:
                    case BtSol:
                    case BtEquals:
                    case BtQuest:
                    case BtSemi:
                    case BtExcl:
                    case BtAst:
                    case BtPercnt:
                    case BtNum:
                        sbuf.Append(c);
                        break;
                    case BtS:
                        if (CharMatches(buf, off, '\t'))
                            throw new InvalidTokenException(off);
                        /* fall through */
                        goto case BtCr;
                    case BtCr:
                    case BtLf:
                        if ((sbuf.Length > 0) && (sbuf[sbuf.Length - 1] != ' '))
                            sbuf.Append(' ');
                        break;
                    case BtName:
                    case BtNmstrt:
                        if ((c & ~0x7f) == 0)
                        {
                            sbuf.Append(c);
                            break;
                        }
                        // fall through
                        goto default;
                    default:
                        switch (c)
                        {
                            case '$':
                            case '@':
                                break;
                            default:
                                throw new InvalidTokenException(off);
                        }
                        break;
                }
            }
            if (sbuf.Length > 0 && sbuf[sbuf.Length - 1] == ' ')
                sbuf.Length = sbuf.Length - 1;
            return sbuf.ToString();
        }

        /**
         * Returns true if the specified byte subarray is equal to the string.
         * The string must contain only XML significant characters.
         */

        public bool MatchesXmlString(byte[] buf, int off, int end, string str)
        {
            int len = str.Length;
            if (len*minBPC != end - off)
                return false;
            for (int i = 0; i < len; off += minBPC, i++)
            {
                if (!CharMatches(buf, off, str[i]))
                    return false;
            }
            return true;
        }

        /**
         * Skips over XML whitespace characters at the start of the specified
         * subarray.
         *
         * @return the index of the first non-whitespace character,
         * <code>end</code> if there is the subarray is all whitespace
         */

        public int SkipS(byte[] buf, int off, int end)
        {
            while (off < end)
            {
                switch (ByteType(buf, off))
                {
                    case BtS:
                    case BtCr:
                    case BtLf:
                        off += minBPC;
                        break;
                    default:
                        goto loop;
                }
            }
            loop:
            return off;
        }

        private bool IsNameChar2(byte[] buf, int off)
        {
            int bt = ByteType2(buf, off);
            return bt == BtName || bt == BtNmstrt;
        }

        private bool IsNameChar3(byte[] buf, int off)
        {
            int bt = ByteType3(buf, off);
            return bt == BtName || bt == BtNmstrt;
        }

        private static bool IsNameChar4(byte[] buf, int off)
        {
            int bt = ByteType4(buf, off);
            return bt == BtName || bt == BtNmstrt;
        }

        private const string NameStartSingles =
            "\u003a\u005f\u0386\u038c\u03da\u03dc\u03de\u03e0\u0559\u06d5\u093d\u09b2" +
            "\u0a5e\u0a8d\u0abd\u0ae0\u0b3d\u0b9c\u0cde\u0e30\u0e84\u0e8a\u0e8d\u0ea5" +
            "\u0ea7\u0eb0\u0ebd\u1100\u1109\u113c\u113e\u1140\u114c\u114e\u1150\u1159" +
            "\u1163\u1165\u1167\u1169\u1175\u119e\u11a8\u11ab\u11ba\u11eb\u11f0\u11f9" +
            "\u1f59\u1f5b\u1f5d\u1fbe\u2126\u212e\u3007";

        private const string NameStartRanges =
            "\u0041\u005a\u0061\u007a\u00c0\u00d6\u00d8\u00f6\u00f8\u00ff\u0100\u0131" +
            "\u0134\u013e\u0141\u0148\u014a\u017e\u0180\u01c3\u01cd\u01f0\u01f4\u01f5" +
            "\u01fa\u0217\u0250\u02a8\u02bb\u02c1\u0388\u038a\u038e\u03a1\u03a3\u03ce" +
            "\u03d0\u03d6\u03e2\u03f3\u0401\u040c\u040e\u044f\u0451\u045c\u045e\u0481" +
            "\u0490\u04c4\u04c7\u04c8\u04cb\u04cc\u04d0\u04eb\u04ee\u04f5\u04f8\u04f9" +
            "\u0531\u0556\u0561\u0586\u05d0\u05ea\u05f0\u05f2\u0621\u063a\u0641\u064a" +
            "\u0671\u06b7\u06ba\u06be\u06c0\u06ce\u06d0\u06d3\u06e5\u06e6\u0905\u0939" +
            "\u0958\u0961\u0985\u098c\u098f\u0990\u0993\u09a8\u09aa\u09b0\u09b6\u09b9" +
            "\u09dc\u09dd\u09df\u09e1\u09f0\u09f1\u0a05\u0a0a\u0a0f\u0a10\u0a13\u0a28" +
            "\u0a2a\u0a30\u0a32\u0a33\u0a35\u0a36\u0a38\u0a39\u0a59\u0a5c\u0a72\u0a74" +
            "\u0a85\u0a8b\u0a8f\u0a91\u0a93\u0aa8\u0aaa\u0ab0\u0ab2\u0ab3\u0ab5\u0ab9" +
            "\u0b05\u0b0c\u0b0f\u0b10\u0b13\u0b28\u0b2a\u0b30\u0b32\u0b33\u0b36\u0b39" +
            "\u0b5c\u0b5d\u0b5f\u0b61\u0b85\u0b8a\u0b8e\u0b90\u0b92\u0b95\u0b99\u0b9a" +
            "\u0b9e\u0b9f\u0ba3\u0ba4\u0ba8\u0baa\u0bae\u0bb5\u0bb7\u0bb9\u0c05\u0c0c" +
            "\u0c0e\u0c10\u0c12\u0c28\u0c2a\u0c33\u0c35\u0c39\u0c60\u0c61\u0c85\u0c8c" +
            "\u0c8e\u0c90\u0c92\u0ca8\u0caa\u0cb3\u0cb5\u0cb9\u0ce0\u0ce1\u0d05\u0d0c" +
            "\u0d0e\u0d10\u0d12\u0d28\u0d2a\u0d39\u0d60\u0d61\u0e01\u0e2e\u0e32\u0e33" +
            "\u0e40\u0e45\u0e81\u0e82\u0e87\u0e88\u0e94\u0e97\u0e99\u0e9f\u0ea1\u0ea3" +
            "\u0eaa\u0eab\u0ead\u0eae\u0eb2\u0eb3\u0ec0\u0ec4\u0f40\u0f47\u0f49\u0f69" +
            "\u10a0\u10c5\u10d0\u10f6\u1102\u1103\u1105\u1107\u110b\u110c\u110e\u1112" +
            "\u1154\u1155\u115f\u1161\u116d\u116e\u1172\u1173\u11ae\u11af\u11b7\u11b8" +
            "\u11bc\u11c2\u1e00\u1e9b\u1ea0\u1ef9\u1f00\u1f15\u1f18\u1f1d\u1f20\u1f45" +
            "\u1f48\u1f4d\u1f50\u1f57\u1f5f\u1f7d\u1f80\u1fb4\u1fb6\u1fbc\u1fc2\u1fc4" +
            "\u1fc6\u1fcc\u1fd0\u1fd3\u1fd6\u1fdb\u1fe0\u1fec\u1ff2\u1ff4\u1ff6\u1ffc" +
            "\u212a\u212b\u2180\u2182\u3041\u3094\u30a1\u30fa\u3105\u312c\uac00\ud7a3" +
            "\u4e00\u9fa5\u3021\u3029";

        private const string NameSingles =
            "\u002d\u002e\u05bf\u05c4\u0670\u093c\u094d\u09bc\u09be\u09bf\u09d7\u0a02" +
            "\u0a3c\u0a3e\u0a3f\u0abc\u0b3c\u0bd7\u0d57\u0e31\u0eb1\u0f35\u0f37\u0f39" +
            "\u0f3e\u0f3f\u0f97\u0fb9\u20e1\u3099\u309a\u00b7\u02d0\u02d1\u0387\u0640" +
            "\u0e46\u0ec6\u3005";

        private const string NameRanges =
            "\u0300\u0345\u0360\u0361\u0483\u0486\u0591\u05a1\u05a3\u05b9\u05bb\u05bd" +
            "\u05c1\u05c2\u064b\u0652\u06d6\u06dc\u06dd\u06df\u06e0\u06e4\u06e7\u06e8" +
            "\u06ea\u06ed\u0901\u0903\u093e\u094c\u0951\u0954\u0962\u0963\u0981\u0983" +
            "\u09c0\u09c4\u09c7\u09c8\u09cb\u09cd\u09e2\u09e3\u0a40\u0a42\u0a47\u0a48" +
            "\u0a4b\u0a4d\u0a70\u0a71\u0a81\u0a83\u0abe\u0ac5\u0ac7\u0ac9\u0acb\u0acd" +
            "\u0b01\u0b03\u0b3e\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57\u0b82\u0b83" +
            "\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0c01\u0c03\u0c3e\u0c44\u0c46\u0c48" +
            "\u0c4a\u0c4d\u0c55\u0c56\u0c82\u0c83\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd" +
            "\u0cd5\u0cd6\u0d02\u0d03\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0e34\u0e3a" +
            "\u0e47\u0e4e\u0eb4\u0eb9\u0ebb\u0ebc\u0ec8\u0ecd\u0f18\u0f19\u0f71\u0f84" +
            "\u0f86\u0f8b\u0f90\u0f95\u0f99\u0fad\u0fb1\u0fb7\u20d0\u20dc\u302a\u302f" +
            "\u0030\u0039\u0660\u0669\u06f0\u06f9\u0966\u096f\u09e6\u09ef\u0a66\u0a6f" +
            "\u0ae6\u0aef\u0b66\u0b6f\u0be7\u0bef\u0c66\u0c6f\u0ce6\u0cef\u0d66\u0d6f" +
            "\u0e50\u0e59\u0ed0\u0ed9\u0f20\u0f29\u3031\u3035\u309d\u309e\u30fc\u30fe";

        /// <summary>
        /// 
        /// </summary>
        protected static int[][] CharTypeTable;

        private static void SetCharType(char c, int type)
        {
            if (c < 0x80)
                return;
            int hi = c >> 8;
            if (CharTypeTable[hi] == null)
            {
                CharTypeTable[hi] = new int[256];
                for (int i = 0; i < 256; i++)
                    CharTypeTable[hi][i] = BtOther;
            }
            CharTypeTable[hi][c & 0xFF] = type;
        }

        private static void SetCharType(char min, char max, int type)
        {
            int[] shared = null;
            do
            {
                if ((min & 0xFF) == 0)
                {
                    for (; min + (char) 0xFF <= max; min += (char) 0x100)
                    {
                        if (shared == null)
                        {
                            shared = new int[256];
                            for (int i = 0; i < 256; i++)
                                shared[i] = type;
                        }
                        CharTypeTable[min >> 8] = shared;
                        if (min + 0xFF == max)
                            return;
                    }
                }
                SetCharType(min, type);
            } while (min++ != max);
        }

        static Encoding()
        {
            CharTypeTable = new int[256][];
            foreach (char c in NameSingles)
                SetCharType(c, BtName);
            for (int i = 0; i < NameRanges.Length; i += 2)
                SetCharType(NameRanges[i], NameRanges[i + 1], BtName);
            for (int i = 0; i < NameStartSingles.Length; i++)
                SetCharType(NameStartSingles[i], BtNmstrt);
            for (int i = 0; i < NameStartRanges.Length; i += 2)
                SetCharType(NameStartRanges[i], NameStartRanges[i + 1],
                            BtNmstrt);
            SetCharType('\uD800', '\uDBFF', BtLead4);
            SetCharType('\uDC00', '\uDFFF', BtMalform);
            SetCharType('\uFFFE', '\uFFFF', BtNoXml);
            int[] other = new int[256];
            for (int i = 0; i < 256; i++)
                other[i] = BtOther;
            for (int i = 0; i < 256; i++)
                if (CharTypeTable[i] == null)
                    CharTypeTable[i] = other;
            System.Array.Copy(AsciiTypeTable, 0, CharTypeTable[0], 0, 128);
        }

        /**
         * Returns the minimum number of bytes required to represent a single
         * character in this encoding.  The value will be 1, 2 or 4.
         */

        public int MinBytesPerChar => minBPC;
    }
}