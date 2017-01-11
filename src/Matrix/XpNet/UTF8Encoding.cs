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
	/// UTF-8 specific tokenizer.
	/// </summary>
    public class UTF8Encoding : Encoding
    {
        private static readonly int[] Utf8HiTypeTable = new int[]
        {
            /* 0x80 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x84 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x88 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x8C */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x90 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x94 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x98 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0x9C */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xA0 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xA4 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xA8 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xAC */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xB0 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xB4 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xB8 */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xBC */ BtMalform, BtMalform, BtMalform, BtMalform,
            /* 0xC0 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xC4 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xC8 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xCC */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xD0 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xD4 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xD8 */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xDC */ BtLead2, BtLead2, BtLead2, BtLead2,
            /* 0xE0 */ BtLead3, BtLead3, BtLead3, BtLead3,
            /* 0xE4 */ BtLead3, BtLead3, BtLead3, BtLead3,
            /* 0xE8 */ BtLead3, BtLead3, BtLead3, BtLead3,
            /* 0xEC */ BtLead3, BtLead3, BtLead3, BtLead3,
            /* 0xF0 */ BtLead4, BtLead4, BtLead4, BtLead4,
            /* 0xF4 */ BtLead4, BtLead4, BtLead4, BtLead4,
            /* 0xF8 */ BtNoXml, BtNoXml, BtNoXml, BtNoXml,
            /* 0xFC */ BtNoXml, BtNoXml, BtMalform, BtMalform
        };

        private static int[] utf8TypeTable = new int[256];

        static UTF8Encoding() {
            System.Array.Copy(AsciiTypeTable,  0, utf8TypeTable,   0, 128);
            System.Array.Copy(Utf8HiTypeTable, 0, utf8TypeTable, 128, 128);
        }

        /// <summary>
        /// New tokenizer
        /// </summary>
        public UTF8Encoding() : base(1)
        {
        }

        /// <summary>
        /// What is the type of the current byte?
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected override int ByteType(byte[] buf, int off) {
            return utf8TypeTable[buf[off] & 0xFF];
        }

        /// <summary>
        /// Current byte to ASCII char
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected override char ByteToAscii(byte[] buf, int off) {
            return (char)buf[off];
        }

        /// <summary>
        /// c is a significant ASCII character
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        protected override bool CharMatches(byte[] buf, int off, char c) {
            return ((char)buf[off]) == c;
        }

        /// <summary>
        /// A 2 byte UTF-8 representation splits the characters 11 bits
        /// between the bottom 5 and 6 bits of the bytes.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <returns></returns>
        protected override int ByteType2(byte[] buf, int off)
        {
            int[] page = CharTypeTable[(buf[off] >> 2) & 0x7];
            return page[((buf[off] & 3) << 6) | (buf[off + 1] & 0x3F)];
        }

        /* A 3 byte UTF-8 representation splits the characters 16 bits
           between the bottom 4, 6 and 6 bits of the bytes. */

        /* This will (incorrectly) return BT_LEAD4 for surrogates, but that
           doesn't matter. */

        void Check3(byte[] buf, int off)
        {
            switch (buf[off])
            {
            case 0xEF:
                /* 0xFFFF 0xFFFE */
                if ((buf[off + 1] == 0xBF) &&
                    ((buf[off + 2] == 0xBF) ||
                     (buf[off + 2] == 0xBE)))
                    throw new InvalidTokenException(off);
                return;
            case 0xED:
                /* 0xD800..0xDFFF <=> top 5 bits are 11011 */
                if ((buf[off + 1] & 0x20) != 0)
                    throw new InvalidTokenException(off);
                return;
            default:
                return;
            }
        }

        void Check4(byte[] buf, int off) {
            switch (buf[off] & 0x7) {
            default:
                return;
            case 5: case 6: case 7:
                break;
            case 4:
                if ((buf[off + 1] & 0x30) == 0)
                    return;
                break;
            }
            throw new InvalidTokenException(off);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceBuf"></param>
        /// <param name="sourceStart"></param>
        /// <param name="sourceEnd"></param>
        /// <param name="targetBuf"></param>
        /// <param name="targetStart"></param>
        /// <returns></returns>
        protected override int Convert(byte[] sourceBuf,
					                   int sourceStart, int sourceEnd,
						               char[] targetBuf, int targetStart)
        {
            int initTargetStart = targetStart;
            int c;
            while (sourceStart != sourceEnd) {
                byte b = sourceBuf[sourceStart++];
                if (b >= 0)
                    targetBuf[targetStart++] = (char)b;
                else {
                    switch (utf8TypeTable[b & 0xFF]) {
                    case BtLead2:
                        /* 5, 6 */
                        targetBuf[targetStart++]
                            = (char)(((b & 0x1F) << 6) | (sourceBuf[sourceStart++] & 0x3F));
                        break;
                    case BtLead3:
                        /* 4, 6, 6 */
                        c = (b & 0xF) << 12;
                        c |= (sourceBuf[sourceStart++] & 0x3F) << 6;
                        c |= (sourceBuf[sourceStart++] & 0x3F);
                        targetBuf[targetStart++] = (char)c;
                        break;
                    case BtLead4:
                        /* 3, 6, 6, 6 */
                        c = (b & 0x7) << 18;
                        c |= (sourceBuf[sourceStart++] & 0x3F) << 12;
                        c |= (sourceBuf[sourceStart++] & 0x3F) << 6;
                        c |= (sourceBuf[sourceStart++] & 0x3F);
                        c -= 0x10000;
                        targetBuf[targetStart++] = (char)((c >> 10) | 0xD800);
                        targetBuf[targetStart++] = (char)((c & ((1 << 10) - 1)) | 0xDC00);
                        break;
                    }
                }
            }
            return targetStart - initTargetStart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="off"></param>
        /// <param name="end"></param>
        /// <param name="pos"></param>
        protected override void MovePosition(byte[] buf, int off, int end, Position pos) {
            /* Maintain the invariant: off - colDiff == colNumber. */
            int colDiff = off - pos.ColumnNumber;
            int lineNumber = pos.LineNumber;
            while (off != end) {
                byte b = buf[off];
                if (b >= 0) {
                    ++off;
                    switch (b) {
                    case (byte)'\n':
                        lineNumber += 1;
                        colDiff = off;
                        break;
                    case (byte)'\r':
                        lineNumber += 1;
                        if (off != end && buf[off] == '\n')
                            off++;
                        colDiff = off;
                        break;
                    }
                }
                else {
                    switch (utf8TypeTable[b & 0xFF]) {
                    default:
                        off += 1;
                        break;
                    case BtLead2:
                        off += 2;
                        colDiff++;
                        break;
                    case BtLead3:
                        off += 3;
                        colDiff += 2;
                        break;
                    case BtLead4:
                        off += 4;
                        colDiff += 3;
                        break;
                    }
                }
            }
            pos.ColumnNumber = off - colDiff;
            pos.LineNumber = lineNumber;
        }

        int ExtendData(byte[] buf, int off, int end)
        {
            while (off != end) {
                int type = utf8TypeTable[buf[off] & 0xFF];
                if (type >= 0)
                    off++;
                else if (type < BtLead4)
                    break;
                else {
                    if (end - off + type < 0)
                        break;
                    switch (type) {
                    case BtLead3:
                        Check3(buf, off);
                        break;
                    case BtLead4:
                        Check4(buf, off);
                        break;
                    }

                    off -= (int)type; // this is an ugly hack, James
                }
            }
            return off;
        }
    }
}