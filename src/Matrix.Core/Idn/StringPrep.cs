#if STRINGPREP
using System;
using System.Text;

namespace Matrix.Idn
{
    /// <summary> This class offers static methods for preparing internationalized
    /// strings. It supports the following stringprep profiles:
    /// * RFC3491 nameprep
    /// * RFC3920 XMPP nodeprep and resourceprep 
    /// Note that this implementation only supports 16-bit Unicode code
    /// points.
    /// </summary>
    internal class StringPrep
    {
        /// <summary> Preps a name according to the Stringprep profile defined in
        /// RFC3491. Unassigned code points are not allowed.
        /// *
        /// </summary>
        /// <param name="input">the name to prep.
        /// </param>
        /// <returns> the prepped name.
        /// @throws StringprepException If the name cannot be prepped with
        /// this profile.
        /// @throws NullPointerException If the name is null.
        /// 
        /// </returns>
        public static string NamePrep(string input)
        {
            return NamePrep(input, false);
        }
		
        /// <summary> Preps a name according to the Stringprep profile defined in
        /// RFC3491.
        /// *
        /// </summary>
        /// <param name="input">the name to prep.
        /// </param>
        /// <param name="allowUnassigned">true if the name may contain unassigned
        /// code points.
        /// </param>
        /// <returns> the prepped name.
        /// @throws StringprepException If the name cannot be prepped with
        /// this profile.
        /// @throws NullPointerException If the name is null.
        /// 
        /// </returns>
        public static string NamePrep(string input, bool allowUnassigned)
        {
            if (input == null)
            {
                throw new System.NullReferenceException();
            }
			
            StringBuilder s = new StringBuilder(input);
			
            if (!allowUnassigned && Contains(s, RFC3454.A1))
            {
                throw new StringPrepException(StringPrepException.CONTAINS_UNASSIGNED);
            }
			
            Filter(s, RFC3454.B1);
            Map(s, RFC3454.B2search, RFC3454.B2replace);
			
            s = new StringBuilder(NFKC.NormalizeNFKC(s.ToString()));
            // B.3 is only needed if NFKC is not used, right?
            // map(s, RFC3454.B3search, RFC3454.B3replace);
			
            if (Contains(s, RFC3454.C12) || Contains(s, RFC3454.C22) || Contains(s, RFC3454.C3) || Contains(s, RFC3454.C4) || Contains(s, RFC3454.C5) || Contains(s, RFC3454.C6) || Contains(s, RFC3454.C7) || Contains(s, RFC3454.C8))
            {
                // Table C.9 only contains code points > 0xFFFF which Java
                // doesn't handle
                throw new StringPrepException(StringPrepException.CONTAINS_PROHIBITED);
            }
			
            // Bidi handling
            bool r = Contains(s, RFC3454.D1);
            bool l = Contains(s, RFC3454.D2);
			
            // RFC 3454, section 6, requirement 1: already handled above (table C.8)
			
            // RFC 3454, section 6, requirement 2
            if (r && l)
            {
                throw new StringPrepException(StringPrepException.BIDI_BOTHRAL);
            }
			
            // RFC 3454, section 6, requirement 3
            if (r)
            {
                if (!Contains(s[0], RFC3454.D1) || !Contains(s[s.Length - 1], RFC3454.D1))
                {
                    throw new StringPrepException(StringPrepException.BIDI_LTRAL);
                }
            }
			
            return s.ToString();
        }
		
        /**
		* Characters prohibited by RFC3920 nodeprep that aren't defined as
		* part of the RFC3454 tables.
		*/
        private static char [] RFC3920_NODEPREP_PROHIBIT = new char [] {
                                                                           '\u0022', '\u0026', '\'',     '\u002F',
                                                                           '\u003A', '\u003C', '\u003E', '\u0040'
                                                                       };
		
        /// <summary> Preps a node name according to the Stringprep profile defined in
        /// RFC3920. Unassigned code points are not allowed.
        /// *
        /// </summary>
        /// <param name="input">the node name to prep.
        /// </param>
        /// <returns> the prepped node name.
        /// @throws StringprepException If the node name cannot be prepped
        /// with this profile.
        /// @throws NullPointerException If the node name is null.
        /// 
        /// </returns>
        public static string NodePrep(string input)
        {
            return NodePrep(input, false);
        }
		
        /// <summary>
        /// Preps a node name according to the Stringprep profile defined in RFC3920.
        /// </summary>
        /// <param name="input">the node name to prep.
        /// </param>
        /// <param name="allowUnassigned">true if the node name may contain
        /// unassigned code points.
        /// </param>
        /// <returns> the prepped node name.
        /// @throws StringprepException If the node name cannot be prepped
        /// with this profile.
        /// @throws NullPointerException If the node name is null.
        /// 
        /// </returns>
        public static string NodePrep(string input, bool allowUnassigned)
        {
            if (input == null)
            {
                throw new System.NullReferenceException();
            }
			
            StringBuilder s = new StringBuilder(input);
			
            if (!allowUnassigned && Contains(s, RFC3454.A1))
            {
                throw new StringPrepException(StringPrepException.CONTAINS_UNASSIGNED);
            }
			
            Filter(s, RFC3454.B1);
            Map(s, RFC3454.B2search, RFC3454.B2replace);
			
            s = new StringBuilder(NFKC.NormalizeNFKC(s.ToString()));
			
            if (Contains(s, RFC3454.C11) || Contains(s, RFC3454.C12) || Contains(s, RFC3454.C21) || Contains(s, RFC3454.C22) || Contains(s, RFC3454.C3) || Contains(s, RFC3454.C4) || Contains(s, RFC3454.C5) || Contains(s, RFC3454.C6) || Contains(s, RFC3454.C7) || Contains(s, RFC3454.C8) || Contains(s, RFC3920_NODEPREP_PROHIBIT))
            {
                // Table C.9 only contains code points > 0xFFFF which Java
                // doesn't handle
                throw new StringPrepException(StringPrepException.CONTAINS_PROHIBITED);
            }
			
            // Bidi handling
            bool r = Contains(s, RFC3454.D1);
            bool l = Contains(s, RFC3454.D2);
			
            // RFC 3454, section 6, requirement 1: already handled above (table C.8)
			
            // RFC 3454, section 6, requirement 2
            if (r && l)
            {
                throw new StringPrepException(StringPrepException.BIDI_BOTHRAL);
            }
			
            // RFC 3454, section 6, requirement 3
            if (r)
            {
                if (!Contains(s[0], RFC3454.D1) || !Contains(s[s.Length - 1], RFC3454.D1))
                {
                    throw new StringPrepException(StringPrepException.BIDI_LTRAL);
                }
            }
			
            return s.ToString();
        }
		
        /// <summary>
        /// Preps a resource name according to the Stringprep profile defined
        /// in RFC3920. Unassigned code points are not allowed.
        /// </summary>
        /// <param name="input">the resource name to prep.
        /// </param>
        /// <returns> the prepped node name.
        /// @throws StringprepException If the resource name cannot be prepped
        /// with this profile.
        /// @throws NullPointerException If the resource name is null.
        /// 
        /// </returns>
        public static string ResourcePrep(string input)
        {
            return ResourcePrep(input, false);
        }
		
        /// <summary>
        /// Preps a resource name according to the Stringprep profile defined
        /// in RFC3920.
        /// </summary>
        /// <param name="input">the resource name to prep.
        /// </param>
        /// <param name="allowUnassigned">true if the resource name may contain
        /// unassigned code points.
        /// </param>
        /// <returns>
        /// the prepped node name.
        /// @throws StringprepException If the resource name cannot be prepped
        /// with this profile.
        /// @throws NullPointerException If the resource name is null.
        /// 
        /// </returns>        
        public static string ResourcePrep(string input, bool allowUnassigned)
        {
            if (input == null)
            {
                throw new System.NullReferenceException();
            }
			
            StringBuilder s = new StringBuilder(input);
			
            if (!allowUnassigned && Contains(s, RFC3454.A1))
            {
                throw new StringPrepException(StringPrepException.CONTAINS_UNASSIGNED);
            }
			
            Filter(s, RFC3454.B1);
			
            s = new StringBuilder(NFKC.NormalizeNFKC(s.ToString()));
			
            if (Contains(s, RFC3454.C12) || Contains(s, RFC3454.C21) || Contains(s, RFC3454.C22) || Contains(s, RFC3454.C3) || Contains(s, RFC3454.C4) || Contains(s, RFC3454.C5) || Contains(s, RFC3454.C6) || Contains(s, RFC3454.C7) || Contains(s, RFC3454.C8))
            {
                // Table C.9 only contains code points > 0xFFFF which Java
                // doesn't handle
                throw new StringPrepException(StringPrepException.CONTAINS_PROHIBITED);
            }
			
            // Bidi handling
            bool r = Contains(s, RFC3454.D1);
            bool l = Contains(s, RFC3454.D2);
			
            // RFC 3454, section 6, requirement 1: already handled above (table C.8)
			
            // RFC 3454, section 6, requirement 2
            if (r && l)
            {
                throw new StringPrepException(StringPrepException.BIDI_BOTHRAL);
            }
			
            // RFC 3454, section 6, requirement 3
            if (r)
            {
                if (!Contains(s[0], RFC3454.D1) || !Contains(s[s.Length - 1], RFC3454.D1))
                {
                    throw new StringPrepException(StringPrepException.BIDI_LTRAL);
                }
            }
			
            return s.ToString();
        }
		
        public static string SaslPrep(string input)
        {
            var s = new StringBuilder(input);

            /*
              This profile specifies:

              -  non-ASCII space characters [StringPrep, C.1.2] that can be
                 mapped to SPACE (U+0020), and

              -  the "commonly mapped to nothing" characters [StringPrep, B.1]
                 that can be mapped to nothing.
            */

            Map(s, RFC3454.C12, RFC3454.C12replace);
            Filter(s, RFC3454.B1);

            // Bidi handling
            //bool r = Contains(s, RFC3454.D1);
            //bool l = Contains(s, RFC3454.D2);

            /*
              - Non-ASCII space characters [StringPrep, C.1.2]
              - ASCII control characters [StringPrep, C.2.1]
              - Non-ASCII control characters [StringPrep, C.2.2]
              - Private Use characters [StringPrep, C.3]
              - Non-character code points [StringPrep, C.4]
              - Surrogate code points [StringPrep, C.5]
              - Inappropriate for plain text characters [StringPrep, C.6]
              - Inappropriate for canonical representation characters
                [StringPrep, C.7]
              - Change display properties or deprecated characters
                [StringPrep, C.8]
              - Tagging characters [StringPrep, C.9]
            */
            
            if (Contains(s, RFC3454.C12) || Contains(s, RFC3454.C21) || Contains(s, RFC3454.C22) || Contains(s, RFC3454.C3) || Contains(s, RFC3454.C4) || Contains(s, RFC3454.C5) || Contains(s, RFC3454.C6) || Contains(s, RFC3454.C7) || Contains(s, RFC3454.C8))
            {
                // TODO add C9 table
                throw new StringPrepException(StringPrepException.CONTAINS_PROHIBITED);
            }

#if WINDOWS_PHONE || CF || SILVERLIGHT || WINRT
            return NFKC.NormalizeNFKC(s.ToString());
#else
            var result =  s.ToString();
            return result.Normalize(NormalizationForm.FormKC);
#endif

        }

        internal static bool Contains(StringBuilder s, char[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                char c = p[i];
                for (int j = 0; j < s.Length; j++)
                {
                    if (c == s[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
		
        internal static bool Contains(StringBuilder s, char[][] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                char[] r = p[i];
                if (1 == r.Length)
                {
                    char c = r[0];
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (c == s[j])
                        {
                            return true;
                        }
                    }
                }
                else if (2 == r.Length)
                {
                    char f = r[0];
                    char t = r[1];
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (f <= s[j] && t >= s[j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
		
        internal static bool Contains(char c, char[][] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                char[] r = p[i];
                if (1 == r.Length)
                {
                    if (c == r[0])
                    {
                        return true;
                    }
                }
                else if (2 == r.Length)
                {
                    char f = r[0];
                    char t = r[1];
                    if (f <= c && t >= c)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
		
        internal static void Filter(StringBuilder s, char[] f)
        {
            for (int i = 0; i < f.Length; i++)
            {
                char c = f[i];
				
                int j = 0;
                while (j < s.Length)
                {
                    if (c == s[j])
                    {
                        //s.deleteCharAt(j);
                        s.Remove(j, 1);
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }
		
        internal static void Filter(StringBuilder s, char[][] f)
        {
            for (int i = 0; i < f.Length; i++)
            {
                char[] r = f[i];
				
                if (1 == r.Length)
                {
                    char c = r[0];
					
                    int j = 0;
                    while (j < s.Length)
                    {
                        if (c == s[j])
                        {
                            //s.deleteCharAt(j);
                            s.Remove(j, 1);
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                else if (2 == r.Length)
                {
                    char from = r[0];
                    char to = r[1];
					
                    int j = 0;
                    while (j < s.Length)
                    {
                        if (from <= s[j] && to >= s[j])
                        {
                            //s.deleteCharAt(j);
                            s.Remove(j, 1);
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
        }
		
        internal static void Map(StringBuilder s, char[] search, string[] replace)
        {
            for (int i = 0; i < search.Length; i++)
            {
                char c = search[i];
				
                int j = 0;
                while (j < s.Length)
                {
                    if (c == s[j])
                    {
                        //s.deleteCharAt(j);
                        s.Remove(j, 1);
                        if (null != replace[i])
                        {
                            s.Insert(j, replace[i]);
                            j += replace[i].Length - 1;
                        }
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }
    }
}
#endif