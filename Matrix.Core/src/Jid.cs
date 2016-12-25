using System;
using System.Collections.Generic;
using System.Text;

#if STRINGPREP
using Matrix.Idn;
#endif

namespace Matrix.Core
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
        m_User      ==> nodeprep
        m_Server    ==> nameprep
        m_Resource  ==> resourceprep
        */
        
        // !!! 
        // use this internal variables only if you know what you are doing
        // !!!
        internal string m_Jid;
		internal string m_User;
		internal string m_Server;
		internal string m_Resource;

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
		/// <param name="jid">XMPP ID, in string form examples: user@server/Resource, user@server</param>
		public Jid(string jid)
		{			
			m_Jid = jid;
			Parse(jid);
		}

        public Jid(string jid, bool stringPrep)
        {
            m_Jid = jid;
            Parse(m_Jid);

            if (!stringPrep)
                return;

            SetUser(User);
            SetServer(Server);
            SetResource(Resource);
        }

        /// <summary>
        /// Builds a new Jid object.
        /// StringPrep is applied to the input string.
        /// </summary>
        /// <param name="user">XMPP User part</param>
        /// <param name="server">XMPP Domain part</param>
        /// <param name="resource">XMPP Resource part</param>        
        public Jid(string user, string server, string resource)
        {
#if !STRINGPREP
            if (user != null)
            {
                user = EscapeNode(user);
                
                m_User = user.ToLower();
            }

            if (server != null)
                m_Server = server.ToLower();

            if (resource != null)
                m_Resource = resource;
#else
            if (user != null)
            {             
                user = EscapeNode(user);

                m_User = StringPrep.NodePrep(user);
            }

            if (server != null)
                m_Server = StringPrep.NamePrep(server);

            if (resource != null)
                m_Resource = StringPrep.ResourcePrep(resource);
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
        /// <returns>true if the jid could be parsed, false if an error occured</returns>
		public bool Parse(string fullJid)
		{
			string user		= null;
			string server	= null;
			string resource = null;

            try
            {
                if (string.IsNullOrEmpty(fullJid))
                    return false;

                m_Jid = fullJid;

                int atPos = m_Jid.IndexOf('@');
                int slashPos = m_Jid.IndexOf('/');

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
                    //user = null;
                    if (slashPos == -1)
                    {
                        // JID Contains only the Server
                        server = m_Jid;
                    }
                    else
                    {
                        // JID Contains only the Server and Resource
                        server = m_Jid.Substring(0, slashPos);
                        resource = m_Jid.Substring(slashPos + 1);
                    }
                }
                else
                {
                    if (slashPos == -1)
                    {
                        // We have no resource
                        // Devide User and Server (user@server)
                        server = m_Jid.Substring(atPos + 1);
                        user = m_Jid.Substring(0, atPos);
                    }
                    else
                    {
                        // We have all
                        user = m_Jid.Substring(0, atPos);
                        server = m_Jid.Substring(atPos + 1, slashPos - atPos - 1);
                        resource = m_Jid.Substring(slashPos + 1);
                    }
                }

                if (user != null)
                    m_User = user;
                if (server != null)
                    m_Server = server;
                if (resource != null)
                    m_Resource = resource;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
		}               
        
        internal void BuildJid()
        {
            m_Jid = BuildJid(m_User, m_Server, m_Resource);
        }

        private string BuildJid(string user, string server, string resource)
		{			
			var sb = new StringBuilder();
			if (user != null)
			{
				sb.Append(user);
				sb.Append("@");
			}
			sb.Append(server);
			if (resource != null)
			{
				sb.Append("/");
				sb.Append(resource);
			}
			return sb.ToString();
		}
        
		public override string ToString()
		{
			return m_Jid;
		}

	    #region << Properties >>

	    /// <summary>
		/// the user part of the JabberId.
		/// </summary>
        public string User
		{
			get
			{				
				return m_User;
			}
			set
			{
                m_User = value;
                BuildJid();				
			}
		}

		/// <summary>
		/// Only Server
		/// </summary>
        public string Server
		{
			get
			{
				return m_Server;
			}
			set
			{
			    m_Server = value;
                BuildJid();
			}
		}

		/// <summary>
		/// Only the Resource field, null for none
		/// </summary>        
        public string Resource
		{
			get
			{				
				return m_Resource;
			}
			set
			{
			    m_Resource = value;
                BuildJid();
			}
		}

        /// <summary>
        /// Sets the User part of the jid. Nodeprep and jid escaping is applied to the input string.
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(string user)
        {
            User = PrepareUser(user);
        }

        public static string PrepareUser(string user)
        {
            if (String.IsNullOrEmpty(user))
                return null;

            // first Encode the user/node
            string tmpUser = EscapeNode(user);
#if !STRINGPREP
			return tmpUser.ToLower();
#else
            return StringPrep.NodePrep(tmpUser);
#endif
        }

	    /// <summary>
        /// Sets the Server part of the jid. Nameprep is applied to the input string.
        /// </summary>
        /// <param name="server"></param>
        public void SetServer(string server)
        {
            Server = PrepareServer(server);
        }

        public static string PrepareServer(string server)
        {
            if (String.IsNullOrEmpty(server))
                return null;

#if !STRINGPREP
            return server.ToLower();
#else
            return StringPrep.NamePrep(server);
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
		/// The Bare Jid only (user@server).
		/// </summary>		
        public string Bare
		{
			get
			{				
				return BuildJid(m_User, m_Server, null);
			}
        }

	    #endregion

	    #region << Overrides >>        

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hcode = 0;
            if (m_User  !=null)
                hcode ^= m_User.GetHashCode();

            if (m_Server != null)
                hcode ^= m_Server.GetHashCode();

            if (m_Resource != null)
                hcode ^= m_Resource.GetHashCode();
            
            return hcode;
        }
        #endregion

        #region IEquatable<Jid> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool IEquatable<Jid>.Equals(Jid other)
        {
            return new FullJidComparer().Compare(other, this) == 0;
        }

        #endregion
        /// <summary>
        /// Compares Full jid (user@server/resource)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Jid other)
        {            
            return new FullJidComparer().Compare(other, this) == 0; 
        }       

        /// <summary>
        /// Compares his with the given IComparer
        /// </summary>
        /// <param name="other"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public bool Equals(Jid other, IComparer<Jid> comparer)
		{            
            return comparer.Compare(other, this) == 0;			
		}

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
        public int CompareTo(Jid other)
        {            
            return new FullJidComparer().Compare(other, this);
        }
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
        public Jid Clone()
        {
            return MemberwiseClone() as Jid;
        }
        
    }
}