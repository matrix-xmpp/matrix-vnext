//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Matrix.DotNetty.Dns
{
	/// <summary>
	/// Summary description for Dns.
	/// </summary>
    internal sealed partial class Resolver
	{
		const   int		_dnsPort            = 53;
		const   int		_udpRetryAttempts   = 2;
		static  int		_uniqueId;
        const   int     _udpTimeout         = 1000;


        /// <summary>
        /// Shorthand form to make SRV querying easier, essentially wraps up the retreival
        /// of the SRV records, and sorts them by preference
        /// </summary>
        /// <param name="domain">domain name to retreive SRV RRs for</param>
        /// <param name="dnsServer">the server we're going to ask</param>
        /// <returns>
        /// An array of SRVRecords
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// domain
        /// or
        /// dnsServer
        /// </exception>
        public static async Task<List<SRVRecord>> SRVLookup(string domain, IPAddress dnsServer)
        {
            // check the inputs
            if (domain == null) throw new ArgumentNullException("domain");
            if (dnsServer == null) throw new ArgumentNullException("dnsServer");

            // create a request for this
            Request request = new Request();

            // add one question - the SRV IN lookup for the supplied domain
            request.AddQuestion(new Question(domain, DnsType.SRV, DnsClass.IN));

            Response response = await Lookup(request, dnsServer);

            // if we didn't get a response, then return null
            if (response == null) return null;


            var resourceRecords = new List<RecordBase>();

            // add each of the answers to the array
            foreach (Answer answer in response.Answers)
            {
                // if the answer is an SRV record
                if (answer.Type == DnsType.SRV)
                    resourceRecords.Add(answer.Record);
            }

            // create array of MX records
            var srvRecords = new SRVRecord[resourceRecords.Count];

            // copy from the array list
            resourceRecords.CopyTo(srvRecords);

            // sort into lowest preference order
            Array.Sort(srvRecords);

            // and return
            return srvRecords.ToList();
        }

        public static async Task<Response> Lookup(Request request, IPAddress dnsServer)
        {
            // check the inputs
            if (request == null) throw new ArgumentNullException("request");
            if (dnsServer == null) throw new ArgumentNullException("dnsServer");

            // We will not catch exceptions here, rather just refer them to the caller

            // create an end point to communicate with
            IPEndPoint server = new IPEndPoint(dnsServer, Resolver._dnsPort);

            // get the message
            byte[] requestMessage = request.GetMessage();

            // send the request and get the response
            byte[] responseMessage = await UdpTransfer(server, requestMessage);

            // and populate a response object from that and return it
            return new Response(responseMessage);
        }

        private static async Task<byte[]> UdpTransfer(IPEndPoint server, byte[] requestMessage)
        {
            // UDP can fail - if it does try again keeping track of how many attempts we've made
            int attempts = 0;

            // try repeatedly in case of failure
            while (attempts <= Resolver._udpRetryAttempts)
            {
                // firstly, uniquely mark this request with an id
                unchecked
                {
                    // substitute in an id unique to this lookup, the request has no idea about this
                    requestMessage[0] = (byte)(Resolver._uniqueId >> 8);
                    requestMessage[1] = (byte)Resolver._uniqueId;
                }

                using (var udpClient = new UdpClient())
                {
                    // send it off to the server
                    await udpClient.SendAsync(requestMessage, requestMessage.Length, server);
                    try
                    {
                        // wait for a response upto 1 second
                        var recv = await udpClient.ReceiveAsync();
                        // make sure the message returned is ours
                        if (recv.Buffer[0] == requestMessage[0] && recv.Buffer[1] == requestMessage[1])
                        {
                            // its a valid response - return it, this is our successful exit point
                            return recv.Buffer;
                        }
                    }
                    catch (SocketException)
                    {
                        // failure - we better try again, but remember how many attempts
                        attempts++;
                    }
                    finally
                    {
                        // increase the unique id
                        _uniqueId++;
                    }
                }
            }
            // the operation has failed, this is our unsuccessful exit point
            throw new NoResponseException();
        }
    }
}
