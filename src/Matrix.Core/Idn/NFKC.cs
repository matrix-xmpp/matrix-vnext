// Copyright (C) 2004  Free Software Foundation, Inc.
// *
// Author: Alexander Gnauck AG-Software, mailto:gnauck@ag-software.de
// *
// This file is part of GNU Libidn.
// *
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 2.1 of
// the License, or (at your option) any later version.
// *
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// *
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301
// USA
#if STRINGPREP
using System.Text;

namespace Matrix.Idn
{
    internal class NFKC
    {
        /// <summary>
        /// Applies NFKC normalization to a string.
        /// </summary>
        /// <param name="sbIn">The string to normalize.</param>
        /// <returns> An NFKC normalized string.</returns>
        public static string NormalizeNFKC(string sbIn)
        {
            StringBuilder sbOut = new StringBuilder();
			
            for (int i = 0; i < sbIn.Length; i++)
            {
                char code = sbIn[i];
				
                // In Unicode 3.0, Hangul was defined as the block from U+AC00
                // to U+D7A3, however, since Unicode 3.2 the block extends until
                // U+D7AF. The decomposeHangul function only decomposes until
                // U+D7A3. Should this be changed?
                if (code >= 0xAC00 && code <= 0xD7AF)
                {
                    sbOut.Append(DecomposeHangul(code));
                }
                else
                {
                    int index = DecomposeIndex(code);
                    if (index == - 1)
                    {
                        sbOut.Append(code);
                    }
                    else
                    {
                        sbOut.Append(DecompositionMappings.m[index]);
                    }
                }
            }
			
            // Bring the stringbuffer into canonical order.
            CanonicalOrdering(sbOut);
			
            // Do the canonical composition.
            int last_cc = 0;
            int last_start = 0;
			
            for (int i = 0; i < sbOut.Length; i++)
            {
                int cc = CombiningClass(sbOut[i]);
				
                if (i > 0 && (last_cc == 0 || last_cc != cc))
                {
                    // Try to combine characters
                    char a = sbOut[last_start];
                    char b = sbOut[i];
					
                    int c = Compose(a, b);
					
                    if (c != - 1)
                    {
                        sbOut[last_start] = (char) c;
                        //sbOut.deleteCharAt(i);
                        sbOut.Remove(i, 1);
                        i--;
						
                        if (i == last_start)
                        {
                            last_cc = 0;
                        }
                        else
                        {
                            last_cc = CombiningClass(sbOut[i - 1]);
                        }
                        continue;
                    }
                }
				
                if (cc == 0)
                {
                    last_start = i;
                }
				
                last_cc = cc;
            }
			
            return sbOut.ToString();
        }

        /// <summary>
        /// Returns the index inside the decomposition table, implemented
        /// using a binary search.
        /// </summary>
        /// <param name="c">Character to look up.</param>
        /// <returns> Index if found, -1 otherwise.</returns>
        private static int DecomposeIndex(char c)
        {
            int start = 0;
            int end = DecompositionKeys.k.Length / 2;
			
            while (true)
            {
                int half = (start + end) / 2;
                int code = DecompositionKeys.k[half * 2];
				
                if (c == code)
                {
                    return DecompositionKeys.k[half * 2 + 1];
                }
                if (half == start)
                {
                    // Character not found
                    return - 1;
                }
                else if (c > code)
                {
                    start = half;
                }
                else
                {
                    end = half;
                }
            }
        }

        /// <summary>
        /// Returns the combining class of a given character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns> The combining class.</returns>
        private static int CombiningClass(char c)
        {
            int h = c >> 8;
            int l = c & 0xff;
			
            int i = Idn.CombiningClass.i[h];
            if (i > - 1)
            {
                return Idn.CombiningClass.c[i, l];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Rearranges characters in a stringbuffer in order to respect the
        /// canonical ordering properties.
        /// </summary>
        /// <param name="sbIn">StringBuffer to rearrange.</param>
        private static void CanonicalOrdering(StringBuilder sbIn)
        {
            bool isOrdered = false;
			
            while (!isOrdered)
            {
                isOrdered = true;
				

                // 24.10.2005
                int lastCC = 0;
                if (sbIn.Length > 0)
                    lastCC = CombiningClass(sbIn[0]);
				
                for (int i = 0; i < sbIn.Length - 1; i++)
                {
                    int nextCC = CombiningClass(sbIn[i + 1]);
                    if (nextCC != 0 && lastCC > nextCC)
                    {
                        for (int j = i + 1; j > 0; j--)
                        {
                            if (CombiningClass(sbIn[j - 1]) <= nextCC)
                            {
                                break;
                            }
                            char t = sbIn[j];
                            sbIn[j] = sbIn[j - 1];
                            sbIn[j - 1] = t;
                            isOrdered = false;
                        }
                        nextCC = lastCC;
                    }
                    lastCC = nextCC;
                }
            }
        }

        /// <summary>
        /// Returns the index inside the composition table.		
        /// </summary>
        /// <param name="a">Character to look up.</param>
        /// <returns> Index if found, -1 otherwise.</returns>
        private static int ComposeIndex(char a)
        {
            if (a >> 8 >= Composition.composePage.Length)
            {
                return - 1;
            }
            int ap = Composition.composePage[a >> 8];
            if (ap == - 1)
            {
                return - 1;
            }
            return Composition.composeData[ap, a & 0xff];
        }

        /// <summary>
        /// Tries to compose two characters canonically.
        /// </summary>
        /// <param name="a">First character.</param>
        /// <param name="b">Second character.</param>
        /// <returns> The composed character or -1 if no composition could be found.</returns>
        private static int Compose(char a, char b)
        {
            int h = ComposeHangul(a, b);
            if (h != - 1)
            {
                return h;
            }
			
            int ai = ComposeIndex(a);
			
            if (ai >= Composition.singleFirstStart && ai < Composition.singleSecondStart)
            {
                if (b == Composition.singleFirst[ai - Composition.singleFirstStart, 0])                
                {
                    return Composition.singleFirst[ai - Composition.singleFirstStart, 1];                    
                }
                else
                {
                    return - 1;
                }
            }

			
            int bi = ComposeIndex(b);
			
            if (bi >= Composition.singleSecondStart)
            {
                if (a == Composition.singleSecond[bi - Composition.singleSecondStart,0])
                {
                    return Composition.singleSecond[bi - Composition.singleSecondStart,1];
                }
                else
                {
                    return - 1;
                }
            }
			
            if (ai >= 0 && ai < Composition.multiSecondStart && bi >= Composition.multiSecondStart && bi < Composition.singleFirstStart)
            {
                char[] f = Composition.multiFirst[ai];
				
                if (bi - Composition.multiSecondStart < f.Length)
                {
                    char r = f[bi - Composition.multiSecondStart];
                    if (r == 0)
                    {
                        return - 1;
                    }
                    else
                    {
                        return r;
                    }
                }
            }			
			
            return - 1;
        }
		
        /// <summary>
        /// Entire hangul code copied from:
        /// http://www.unicode.org/unicode/reports/tr15/
        /// Several hangul specific constants
        /// </summary>
        private const int SBase = 0xAC00;
        private const int LBase = 0x1100;
        private const int VBase = 0x1161;
        private const int TBase = 0x11A7;
        private const int LCount = 19;
        private const int VCount = 21;
        private const int TCount = 28;

        private static readonly int NCount = VCount * TCount;

        private static readonly int SCount = LCount * NCount;

        /// <summary>
        /// Decomposes a hangul character.
        /// </summary>
        /// <param name="s">A character to decompose.</param>
        /// <returns> A string containing the hangul decomposition of the input
        /// character. If no hangul decomposition can be found, a string
        /// containing the character itself is returned.</returns>
        private static string DecomposeHangul(char s)
        {
            int SIndex = s - SBase;
            if (SIndex < 0 || SIndex >= SCount)
            {
                return s.ToString();
            }
            StringBuilder result = new StringBuilder();
            int L = LBase + SIndex / NCount;
            int V = VBase + (SIndex % NCount) / TCount;
            int T = TBase + SIndex % TCount;
            result.Append((char) L);
            result.Append((char) V);
            if (T != TBase)
                result.Append((char) T);
            return result.ToString();
        }

        /// <summary>
        /// Composes two hangul characters.
        /// </summary>
        /// <param name="a">First character.</param>
        /// <param name="b">Second character.</param>
        /// <returns> Returns the composed character or -1 if the two characters cannot be composed.</returns>
        private static int ComposeHangul(char a, char b)
        {
            // 1. check to see if two current characters are L and V
            int LIndex = a - LBase;
            if (0 <= LIndex && LIndex < LCount)
            {
                int VIndex = b - VBase;
                if (0 <= VIndex && VIndex < VCount)
                {
                    // make syllable of form LV
                    return SBase + (LIndex * VCount + VIndex) * TCount;
                }
            }
			
            // 2. check to see if two current characters are LV and T
            int SIndex = a - SBase;
            if (0 <= SIndex && SIndex < SCount && (SIndex % TCount) == 0)
            {
                int TIndex = b - TBase;
                if (0 <= TIndex && TIndex <= TCount)
                {
                    // make syllable of form LVT
                    return a + TIndex;
                }
            }
            return - 1;
        }
    }
}
#endif