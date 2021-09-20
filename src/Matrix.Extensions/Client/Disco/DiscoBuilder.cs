namespace Matrix.Extensions.Client.Disco
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using System;

    public class DiscoBuilder
    {
        /// <summary>
        /// Builds a request to discover information of an entity
        /// </summary>
        /// <param name="to">The xmpp entity to discover</param>
        /// <returns></returns>
        /// <param name="node">Optional node information</param>
        /// <returns></returns>
        public static Iq DiscoverInformation(Jid to, string node = null)
        {
            var discoIq = new DiscoInfoIq { To = to, Type = IqType.Get };
                       
            if (!String.IsNullOrEmpty(node))
            { 
                discoIq.Info.Node = node;
            }

            return discoIq;
        }


        /// <summary>
        /// Builds a request to discover the items of an entity
        /// </summary>
        /// <param name="to">The xmpp entity to discover</param>
        /// <returns></returns>
        /// <param name="node">Optional node information</param>
        /// <returns></returns>
        public static Iq DiscoverItems(Jid to, string node = null)
        {
            var discoItemsIq = new DiscoItemsIq { Type = IqType.Get, To = to };

            if (!String.IsNullOrEmpty(node))
            { 
                discoItemsIq.Items.Node = node;
            }

            return discoItemsIq;
        }
    }
}
