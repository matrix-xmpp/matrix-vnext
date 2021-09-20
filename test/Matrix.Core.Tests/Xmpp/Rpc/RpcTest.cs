using System;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Rpc;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Rpc
{
    public class RpcTest
    {
        [Fact]
        public void ShouldBeOfTypeRpc()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Rpc.rpc_query1.xml")).ShouldBeOfType<Matrix.Xmpp.Rpc.Rpc>();
        }

        [Fact]
        public void TestRpcQuery()
        {
            var rpc = XmppXElement.LoadXml(Resource.Get("Xmpp.Rpc.rpc_query1.xml")).Cast<Matrix.Xmpp.Rpc.Rpc>();

            var call = rpc.MethodCall;
            Assert.True(call != null);
            Assert.Equal("bid", call.MethodName);
        }

        [Fact]
        public void TestBuildMethodcallWithIq()
        {
            var iq = new RpcIq
            {
                Id = "0815",
                Type = Matrix.Xmpp.IqType.Set
            };

            var call = new MethodCall {MethodName = "bid"};

            var parameters = new Parameters
            {
                new StructParameter
                {
                    {"symbol", "RHAT"},
                    {"limit", 2.25},
                    {"expires", new DateTime(2002, 07, 09, 20, 0, 0)}
                }
            };
            call.SetParameters(parameters);
            iq.Rpc.MethodCall = call;

            iq.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_iq1.xml"));
        }

        [Fact]
        public void TestBuildRpcQuery()
        {
            var rpc = new Matrix.Xmpp.Rpc.Rpc();
            var methodCall = new MethodCall { MethodName = "bid" };

            var parameters = new Parameters
                {
                    new StructParameter
                        {
                            {"symbol", "RHAT"},
                            {"limit", 2.25},
                            {"expires", new DateTime(2002,07,09,20,0,0)}
                        }
                };
            methodCall.SetParameters(parameters);
            rpc.MethodCall = methodCall;

            rpc.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_query1.xml"));
        }


        [Fact]
        public void TestBuildMethodcallWithIq2()
        {
            var iq = new RpcIq
            {
                Id = "4jLvG-34",
                To = "buyer@collabnexgen.net/Smack",
                From = "seller@collabnexgen.net/Smack",
                Type = Matrix.Xmpp.IqType.Set
            };

            var call = new MethodCall {MethodName = "com.collabng.net.remoteable.ViewpointRemoteable.setViewpoint"};

            var parameters = new Parameters
            {
                "viewpoint_BackView_collabng"
            };
            call.SetParameters(parameters);
            iq.Rpc.MethodCall = call;

            iq.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_iq2.xml"));
        }

        [Fact]
        public void TestMethodResponse1()
        {
            var rpc = XmppXElement.LoadXml(Resource.Get("Xmpp.Rpc.rpc_query_response1.xml")).Cast<Matrix.Xmpp.Rpc.Rpc>();
            var resp = rpc.MethodResponse;
            Assert.True(resp != null);
        }

        [Fact]
        public void TestBuildRpcQuery2()
        {
            var rpc = new Matrix.Xmpp.Rpc.Rpc();
            var call = new MethodCall {MethodName = "examples.getStateName"};

            var pars = new Parameters {6};
            call.SetParameters(pars);

            rpc.MethodCall = call;
            rpc.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_query2.xml"));
        }

        [Fact]
        public void TestBuildMethodResponseQuery()
        {
            var rpc = new Matrix.Xmpp.Rpc.Rpc();
            var resp = new MethodResponse();

            var pars = new Parameters { "Colorado" };
            resp.SetParameters(pars);

            rpc.MethodResponse = resp;
            rpc.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_query_response2.xml"));
        }

        [Fact]
        public void TestBuildFaultResponse()
        {
            var resp = new MethodResponse();
            Assert.False(resp.IsError);
            var pars = new Parameters { new XmlRpcException("Unknown stock symbol ABCD") { Code = 23 } };
            resp.SetParameters(pars);

            Assert.True(resp.IsError);
            resp.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_query_response3.xml"));
        }

        [Fact]
        public void TestBuildIqWithStructThatContainsAnArray()
        {
            var iq = new RpcIq
            {
                Id = "0815",
                Type = Matrix.Xmpp.IqType.Set
            };

            var call = new MethodCall {MethodName = "bid"};

            var parameters = new Parameters
            {
                new StructParameter
                {
                    {"symbol", "RHAT"},
                    {"limit", 2.25},
                    {"some_array", new Parameters {"A", "B", "C", "D"}}
                }
            };
            call.SetParameters(parameters);
            iq.Rpc.MethodCall = call;
            iq.ShouldBe(Resource.Get("Xmpp.Rpc.rpc_iq3.xml"));
        }
    }
}
