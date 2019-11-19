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

using DotNetty.Transport.Channels;

using Matrix.Attributes;

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix.Network.Handlers
{
    [Name("Reconnect-Handler")]
    public class ReconnectHandler : ChannelHandlerAdapter
    {
        XmppConnection xmppCon;
        bool reconnecting = false;
        bool shouldReconnect = true;

        CancellationTokenSource cts = new CancellationTokenSource();

        public ReconnectHandler(XmppConnection xmppCon)
        {
            this.xmppCon = xmppCon;

            xmppCon
                .XmppSessionState
                .Subject
                .DistinctUntilChanged()
                .Skip(1)
                .Subscribe(st =>
                {
                    if (st == SessionState.Disconnected 
                        && shouldReconnect
                        && !reconnecting)
                    {
                        // got disconnected
                        Task.Run(async () => await Reconnect());
                    }
                });

            xmppCon
               .XmppSessionEvent
               .Subject
               .DistinctUntilChanged()
               .Subscribe(se =>
               {
                   if (se == SessionEvent.CallDisconnect)
                   {
                       // intended disconnect by user, no reconnect required
                       this.shouldReconnect = false;
                   }
               });
        }

        public override bool IsSharable => true;

        private async Task Reconnect()
        {
            reconnecting = true;
            ExponentialBackoff backoff = new ExponentialBackoff();

            while (reconnecting)
            {
                try
                {
                    await backoff.Delay();                    
                    await xmppCon.ConnectAsync(cts.Token);
                    
                    reconnecting = false;
                }
                catch (Exception)
                {
                }
            }
        }

        public Task Stop()
        {
            return Task.Run(() =>
            {
                cts.Cancel();
                reconnecting = false;
            });
        }
    }
}
