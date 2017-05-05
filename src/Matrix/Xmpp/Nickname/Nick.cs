/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Nickname
{
    /// <summary>
    /// XEP-0172: User Nickname
    /// </summary>
    [XmppTag(Name = "nick", Namespace = Namespaces.Nick)]
    public class Nick : XmppXElement
    {
        #region Xml smaple
        /*
	        <nick xmlns='http://jabber.org/protocol/nick'>Ishmael</nick>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Nick"/> class.
        /// </summary>
		public Nick() : base(Namespaces.Nick, "nick")
		{		
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Nick"/> class.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
		public Nick(string nickname) : this()
		{            
			Value = nickname;
		}
		#endregion
		
		static public implicit operator Nick(string value)
	    {            
	        return new Nick(value);
	    }
	
	    static public implicit operator string(Nick nick)
	    {
	        return nick.Value;
	    }
	}
}
