using System;
using System.ComponentModel;
using Matrix.Attributes;
#if WIN
using System.ComponentModel;
using System.ComponentModel.Design;
#endif
#if NET_4_5
using System.Threading.Tasks;
#endif
using Matrix.Xml;
using Matrix.Xmpp.Client;

namespace Matrix.Xmpp.Base
{
    [ToolboxItem(true)]
    public abstract class Manager
#if WIN
        : System.ComponentModel.Component
#endif
    {
        #region << Properties >>
        private XmppClient m_XmppClient;

        /// <summary>
        /// Gets or sets the XMPP client.
        /// </summary>
        /// <value>The XMPP client.</value>
        public XmppClient XmppClient
        {
            get
            {
#if WIN
                if ((m_XmppClient == null) && DesignMode)
                {
                    // try to get the XmppClient object
                    // Access designer host and obtain reference to root component
                    var designer = GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (designer != null)
                    {
                        var root = designer.RootComponent as System.ComponentModel.Component;
                        if (root != null)
                        {
                            foreach (System.ComponentModel.Component c in root.Container.Components)
                            {
                                if (c is XmppClient)
                                {
                                    m_XmppClient = c as XmppClient;
                                    return m_XmppClient;
                                }
                            }
                        }
                    }
                    return null;
                }

                return m_XmppClient;
#else
                return m_XmppClient;
#endif
            }
            set
            {
                if (XmppClient != null)
                {
                    // remove previous events
                    XmppClient.OnMessage            -= XmppClient_OnMessage;
                    XmppClient.OnIq                 -= XmppClient_OnIq;
                    XmppClient.OnPresence           -= XmppClient_OnPresence;
                    XmppClient.OnClose              -= XmppClient_OnClose;
                    
                    // only used in DiscoManager right now
                    XmppClient.OnBeforeSendPresence -= XmppClient_OnBeforeSendPresence;
                    XmppClient.OnStreamFeatures     -= XmppClient_OnStreamFeatures;
                    XmppClient.OnStreamHeader       -= XmppClient_OnStreamHeader;
                    
                }

                m_XmppClient = value;
                // add new events
                if (XmppClient != null)
                {
                    XmppClient.OnMessage            += XmppClient_OnMessage;
                    XmppClient.OnIq                 += XmppClient_OnIq;
                    XmppClient.OnPresence           += XmppClient_OnPresence;
                    XmppClient.OnClose              += XmppClient_OnClose;

                    // only used in DiscoManager right now
                    XmppClient.OnBeforeSendPresence += XmppClient_OnBeforeSendPresence;
                    XmppClient.OnStreamFeatures     += XmppClient_OnStreamFeatures;
                    XmppClient.OnStreamHeader       += XmppClient_OnStreamHeader;
                }
            }
        }
        #endregion

        #region << Events >>
        internal virtual void XmppClient_OnPresence(object sender, PresenceEventArgs e)
        {
        }

        internal virtual void XmppClient_OnMessage(object sender, MessageEventArgs e)
        {
        }
        
        internal virtual void XmppClient_OnIq(object sender, IqEventArgs e)
        {
        }

        internal virtual void XmppClient_OnClose(object sender, EventArgs e)
        {
        }

        internal virtual void XmppClient_OnStreamHeader(object sender, StanzaEventArgs e)
        {
        }

        internal virtual void XmppClient_OnStreamFeatures(object sender, StanzaEventArgs e)
        {
        }

        internal virtual void XmppClient_OnBeforeSendPresence(object sender, PresenceEventArgs e)
        {
        }
        #endregion

        #region << Private Members & Functions >>
        /// <summary>
        /// Sends the specified iq.
        /// </summary>
        /// <param name="iq">The iq.</param>
        /// <param name="cb">The callback.</param>
        /// <param name="state">The state.</param>
        /// <exception cref="PropertyNotSetException">Throws PropertyNotSetException when the XmppClient property is null.</exception>
        internal void Send(Client.Iq iq, EventHandler<IqEventArgs> cb, object state)
        {
            if (XmppClient != null)
            {
                if (cb != null)
                    XmppClient.IqFilter.SendIq(iq, cb, state);
                else
                    XmppClient.Send(iq);
            }
            else
                throw new PropertyNotSetException("XmppClient");
        }

        /// <summary>
        /// Sends the specified el.
        /// </summary>
        /// <param name="el">The el.</param>
        /// <exception cref="PropertyNotSetException"></exception>
        internal void Send(XmppXElement el)
        {
            if (XmppClient != null)
                XmppClient.Send(el);
            else
                throw new PropertyNotSetException("XmppClient");
        }

#if NET_4_5
        internal async Task<Client.Iq> SendAsync(Client.Iq iq)
        {
            if (XmppClient != null)
                return await XmppClient.IqFilter.SendIqAsync(iq);

            throw new PropertyNotSetException("XmppClient");
        }

        internal async Task<Client.Iq> SendAsync(Client.Iq iq, int timeout)
        {
            if (XmppClient != null)
                return await XmppClient.IqFilter.SendIqAsync(iq, timeout);

            throw new PropertyNotSetException("XmppClient");
        }
#endif
        #endregion
    }
}