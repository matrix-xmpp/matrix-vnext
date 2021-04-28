using System;
using System.Collections.Generic;
using System.Text;

#if STRINGPREP
using Matrix.Idn;
#endif

namespace Matrix
{	
	/// <summary>
	/// Class for building and handling XMPP Id's (JID's)
	/// </summary>  
    public class Jid : IComparable<Jid>, IEquatable<Jid>
	{
        /*		
        14 possible invalid forms of JIDs and some variations on valid JIDs with invalid lengths, viz:

        jidforms = [
            "",
            "@",
            "@/resource",
            "@domain",
            "@domain/",
            "@domain/resource",
            "nodename@",
            "/",
            "nodename@domain/",
            "nodename@/",
            "@/",
            "nodename/",
            "/resource",
            "nodename@/resource",
        ]
        

        TODO
        Each allowable portion of a JID (node identifier, domain identifier, and resource identifier) MUST NOT
        be more than 1023 bytes in length, resulting in a maximum total size
        (including the '@' and '/' separators) of 3071 bytes.
            
        stringprep with libIDN        
        local       ==> nodeprep
        domain      ==> nameprep
        resource    ==> resourceprep
        */
        
        private string _fullJid;
        private string _local;
        private string _domain;
        private string _resource;

        /// <summary>
        /// Create a new Jid object.
        /// </summary>
        public Jid()
        {
        }

		/// <summary>
		/// Create a new Jid object from a string. The input string must be a valid jabberId and already prepared with stringprep.
        /// Otherwise use one of the other constructors with escapes the node and prepares the gives balues with the stringprep
        /// profiles
		/// </summary>
		/// <param name="jid">XMPP ID, in string form examples: local@domain/Resource, local@domain</param>
		public Jid(string jid)
		{			
			_fullJid = jid;
			Parse(jid);
		}

        public Jid(string jid, bool stringPrep)
        {
            _fullJid = jid;
            Parse(_fullJid);

            if (!stringPrep)
                return;

            SetLocal(Local);
            SetDomain(Domain);
            SetResource(Resource);
        }

        /// <summary>
        /// Builds a new Jid object.
        /// StringPrep is applied to the input string.
        /// </summary>
        /// <param name="local">XMPP local part</param>
        /// <param name="domain">XMPP domain part</param>
        /// <param name="resource">XMPP resource part</param>        
        public Jid(string local, string domain, string resource)
        {
#if !STRINGPREP
            if (local != null)
            {
                local = EscapeNode(local);
                
                _local = local.ToLower();
            }

            if (domain != null)
                _domain = domain.ToLower();

            if (resource != null)
                _resource = resource;
#else
            if (local != null)
            {             
                local = EscapeNode(local);

                _local = StringPrep.NodePrep(local);
            }

            if (domain != null)
                _domain = StringPrep.NamePrep(domain);

            if (resource != null)
                _resource = StringPrep.ResourcePrep(resource);
#endif
            BuildJid();            
        }

	    #region << Operators >>

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Jid"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
	    public static implicit operator Jid(string value)
        {            
            return new Jid(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Jid"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Jid jid)
        {
            return jid.ToString();
        }

	    #endregion

        /// <summary>
        /// Parses a JabberId from a string. If we parse a jid we assume it's correct and already prepared via stringprep.
        /// </summary>
        /// <param name="fullJid">jid to parse as string</param>
        /// <returns>true if the jid could be parsed, false if an error occurred</returns>
		public bool Parse(string fullJid)
		{
			string local = null;
			string domain = null;
			string resource = null;

            try
            {
                if (string.IsNullOrEmpty(fullJid))
                    return false;

                int atPos = fullJid.IndexOf('@');
                int slashPos = fullJid.IndexOf('/');

                // some more validations
                // @... or /...
                if (atPos == 0 || slashPos == 0)
                    return false;

                // nodename@
                if (atPos + 1 == fullJid.Length)
                    return false;

                // @/ at followed by resource separator
                if (atPos + 1 == slashPos)
                    return false;

                if (atPos == -1)
                {
                    //local = null;
                    if (slashPos == -1)
                    {
                        // JID Contains only the domain
                        domain = fullJid;
                    }
                    else
                    {
                        // JID Contains only the domain and resource
                        domain = fullJid.Substring(0, slashPos);
                        resource = fullJid.Substring(slashPos + 1);
                    }
                }
                else
                {
                    if (slashPos == -1)
                    {
                        // We have no resource
                        // Devide local and domain (local@domain)
                        domain = fullJid.Substring(atPos + 1);
                        local = fullJid.Substring(0, atPos);
                    }
                    else
                    {
                        // We have all
                        local = fullJid.Substring(0, atPos);
                        domain = fullJid.Substring(atPos + 1, slashPos - atPos - 1);
                        resource = fullJid.Substring(slashPos + 1);
                    }
                }

                if (local != null)
                {
                    _local = local;
                }

                if (domain != null)
                {
                    _domain = domain;
                }

                if (resource != null)
                {
                    _resource = resource;
                }

                _fullJid = fullJid;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
		}

        private void BuildJid()
        {
            _fullJid = BuildJid(_local, _domain, _resource);
        }

        private string BuildJid(string local, string domain, string resource)
		{			
			var sb = new StringBuilder();
			if (local != null)
			{
				sb.Append(local);
				sb.Append("@");
			}
			sb.Append(domain);
			if (resource != null)
			{
				sb.Append("/");
				sb.Append(resource);
			}
			return sb.ToString();
		}
        
		public override string ToString() => _fullJid;

	    #region << Properties >>

	    /// <summary>
		/// The local part
		/// </summary>
        public string Local
		{
			get => _local;
            set
			{
                _local = value;
                BuildJid();				
			}
		}

		/// <summary>
		/// The domain part
		/// </summary>
        public string Domain
		{
			get => _domain;
            set
			{
			    _domain = value;
                BuildJid();
			}
		}

		/// <summary>
		/// The Resource field, null for none
		/// </summary>        
        public string Resource
		{
			get => _resource;
            set
			{
			    _resource = value;
                BuildJid();
			}
		}

        /// <summary>
        /// Sets the User part of the jid. Nodeprep and jid escaping is applied to the input string.
        /// </summary>
        /// <param name="local"></param>
        public void SetLocal(string local)
        {
            Local = PrepareLocal(local);
        }

        public static string PrepareLocal(string local)
        {
            if (String.IsNullOrEmpty(local))
                return null;

            // first Encode the user/node
            string tmpLocal = EscapeNode(local);
#if !STRINGPREP
			return tmpLocal.ToLower();
#else
            return StringPrep.NodePrep(tmpLocal);
#endif
        }

        /// <summary>
        /// Sets the domain part of the jid. Nameprep is applied to the input string.
        /// </summary>
        /// <param name="domain"></param>
        public void SetDomain(string domain)
        {
            Domain = PrepareDomain(domain);
        }

        public static string PrepareDomain(string domain)
        {
            if (String.IsNullOrEmpty(domain))
                return null;

#if !STRINGPREP
            return domain.ToLower();
#else
            return StringPrep.NamePrep(domain);
#endif
        }

	    /// <summary>
        /// Sets the Resource part of the jid.
        /// ResourcePrep is applied to the input string.
        /// </summary>
        /// <param name="resource"></param>
        public void SetResource(string resource)
        {
            Resource = PrepareResource(resource);
        }

        public static string PrepareResource(string resource)
        {
            if (String.IsNullOrEmpty(resource))
                return null;
           
#if !STRINGPREP
			return resource;
#else
            return StringPrep.ResourcePrep(resource);
#endif
        }

		/// <summary>
		/// The Bare Jid only (local@domain).
		/// </summary>		
        public string Bare => BuildJid(_local, _domain, null);

	    #endregion

        #region IEquatable<Jid> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool IEquatable<Jid>.Equals(Jid other) => new FullJidComparer().Compare(other, this) == 0;

	    #endregion
        /// <summary>
        /// Compares Full jid (local@domain/resource)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Jid other) => new FullJidComparer().Compare(other, this) == 0;

	    /// <summary>
        /// Compares his with the given IComparer
        /// </summary>
        /// <param name="other"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public bool Equals(Jid other, IComparer<Jid> comparer) => comparer.Compare(other, this) == 0;

	    #region IComparable<Jid> Members
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Jid other) => new FullJidComparer().Compare(other, this);

	    #endregion

        #region << XEP-0106: JID Escaping >>
        /// <summary>
        /// <para>
        /// Escape a node according to XEP-0106
        /// </para>
        /// <para>
        /// <a href="http://www.xmpp.org/extensions/xep-0106.html">http://www.xmpp.org/extensions/xep-0106.html</a>
        /// </para>        
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string EscapeNode(string node)
        {
            if (node == null)
                return null;

            var sb = new StringBuilder();
            for (int i = 0; i < node.Length; i++)
            {
                /*
                <space> \20
                " 	    \22
                & 	    \26
                ' 	    \27
                / 	    \2f
                : 	    \3a
                < 	    \3c
                > 	    \3e
                @ 	    \40
                \ 	    \5c
                */
                char c = node[i];
                switch (c)
                {
                    case ' ': sb.Append(@"\20"); break;
                    case '"': sb.Append(@"\22"); break;
                    case '&': sb.Append(@"\26"); break;
                    case '\'': sb.Append(@"\27"); break;
                    case '/': sb.Append(@"\2f"); break;
                    case ':': sb.Append(@"\3a"); break;
                    case '<': sb.Append(@"\3c"); break;
                    case '>': sb.Append(@"\3e"); break;
                    case '@': sb.Append(@"\40"); break;
                    case '\\': sb.Append(@"\5c"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// <para>
        /// unescape a node according to XEP-0106
        /// </para>
        /// <para>
        /// <a href="http://www.xmpp.org/extensions/xep-0106.html">http://www.xmpp.org/extensions/xep-0106.html</a>
        /// </para>        
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string UnescapeNode(string node)
        {
            if (node == null)
                return null;

            var sb = new StringBuilder();
            for (int i = 0; i < node.Length; i++)
            {
                char c1 = node[i];
                if (c1 == '\\' && i + 2 < node.Length)
                {
                    i += 1;
                    char c2 = node[i];
                    i += 1;
                    char c3 = node[i];
                    if (c2 == '2')
                    {
                        switch (c3)
                        {
                            case '0':
                                sb.Append(' ');
                                break;
                            case '2':
                                sb.Append('"');
                                break;
                            case '6':
                                sb.Append('&');
                                break;
                            case '7':
                                sb.Append('\'');
                                break;
                            case 'f':
                                sb.Append('/');
                                break;
                        }
                    }
                    else if (c2 == '3')
                    {
                        switch (c3)
                        {
                            case 'a':
                                sb.Append(':');
                                break;
                            case 'c':
                                sb.Append('<');
                                break;
                            case 'e':
                                sb.Append('>');
                                break;
                        }
                    }
                    else if (c2 == '4')
                    {
                        if (c3 == '0')
                            sb.Append("@");
                    }
                    else if (c2 == '5')
                    {
                        if (c3 == 'c')
                            sb.Append("\\");
                    }
                }
                else
                    sb.Append(c1);
            }
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Jid Clone() => MemberwiseClone() as Jid;
	}
}
