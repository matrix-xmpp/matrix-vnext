using System;
using System.Diagnostics;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Rpc;
using Xunit;


namespace Matrix.Tests.Xmpp.Rpc
{
    
    public class RpcTest
    {
        private const string METHOD_CALL1 = @"<query xmlns='jabber:iq:rpc'><methodCall>
<methodName>bid</methodName>
  <params>
    <param>
      <value>
        <struct>
          <member>
            <name>symbol</name>
            <value><string>RHAT</string></value>
          </member>
          <member>
            <name>limit</name>
            <value><double>2.25</double></value>
          </member>
          <member>
            <name>expires</name>
            <value><dateTime.iso8601>20020709T20:00:00</dateTime.iso8601></value>
          </member>
        </struct>
      </value>
    </param>
  </params>
</methodCall></query>";

        private const string METHOD_CALL_WITH_IQ = @"<iq xmlns='jabber:client' id='0815' type='set'><query xmlns='jabber:iq:rpc'><methodCall>
<methodName>bid</methodName>
  <params>
    <param>
      <value>
        <struct>
          <member>
            <name>symbol</name>
            <value><string>RHAT</string></value>
          </member>
          <member>
            <name>limit</name>
            <value><double>2.25</double></value>
          </member>
          <member>
            <name>expires</name>
            <value><dateTime.iso8601>20020709T20:00:00</dateTime.iso8601></value>
          </member>
        </struct>
      </value>
    </param>
  </params>
</methodCall></query></iq>";

        private const string METHOD_CALL_WITH_IQ_2 = @"<iq xmlns='jabber:client' id='4jLvG-34' to='buyer@collabnexgen.net/Smack' from='seller@collabnexgen.net/Smack' type='set'>
        <query xmlns='jabber:iq:rpc'>
                <methodCall>
                        <methodName>com.collabng.net.remoteable.ViewpointRemoteable.setViewpoint</methodName>
                        <params>
                                <param>
                                        <value>
                                                <string>viewpoint_BackView_collabng</string>
                                        </value>
                                </param>
                        </params>
                </methodCall>
        </query>
</iq>";

        private const string METHOD_CALL_WITH_STRUCT_AND_NESTED_ARRAY = @"<iq id='0815' type='set' xmlns='jabber:client'>
  <query xmlns='jabber:iq:rpc'>
    <methodCall>
      <methodName>bid</methodName>
      <params>
        <param>
          <value>
            <struct>
              <member>
                <name>symbol</name>
                <value><string>RHAT</string></value>
              </member>
              <member>
                <name>limit</name>
                <value><double>2.25</double></value>
              </member>
              <member>
                <name>some_array</name>
                <value>
                  <array>
                    <data>
                      <value><string>A</string></value>
                      <value><string>B</string></value>
                      <value><string>C</string></value>
                      <value><string>D</string></value>
                    </data>
                  </array>
                </value>
              </member>
            </struct>
          </value>
        </param>
      </params>
    </methodCall>
  </query>
</iq>";

        private const string METHOD_CALL2 = @"<query xmlns='jabber:iq:rpc'>
    <methodCall>
      <methodName>examples.getStateName</methodName>
      <params>
        <param>
          <value><i4>6</i4></value>
        </param>
      </params>
    </methodCall>
  </query>";

        private const string METHOD_RESPONSE1 = @"<query xmlns='jabber:iq:rpc'><methodResponse>
  <fault>
    <value>
      <struct>
        <member>
          <name>faultCode</name>
          <value><int>23</int></value>
        </member>
        <member>
          <name>faultString</name>
          <value><string>Unknown stock symbol ABCD</string></value>
        </member>
      </struct>
    </value>
  </fault>
</methodResponse></query>";

        private const string METHOD_RESPONSE2 = @"<query xmlns='jabber:iq:rpc'>
    <methodResponse>
      <params>
        <param>
          <value><string>Colorado</string></value>
        </param>
      </params>
    </methodResponse>
  </query>";

        private const string METHOD_RESPONSE3 = @"<methodResponse xmlns='jabber:iq:rpc'>
  <fault>
    <value>
      <struct>
        <member>
          <name>faultCode</name>
          <value><int>23</int></value>
        </member>
        <member>
          <name>faultString</name>
          <value><string>Unknown stock symbol ABCD</string></value>
        </member>
      </struct>
    </value>
  </fault>
</methodResponse>";

        [Fact]
        public void Test1()
        {
            var el = XmppXElement.LoadXml(METHOD_CALL1);
            Assert.Equal(el is Matrix.Xmpp.Rpc.Rpc, true);

            var rpc = el as Matrix.Xmpp.Rpc.Rpc;

            var call = rpc.MethodCall;
            Assert.Equal(call != null, true);
            Assert.Equal(call.MethodName, "bid");

            var pars = call.GetParameters();
            

            var rpc2 = new Matrix.Xmpp.Rpc.Rpc();
            var call2 = new MethodCall {MethodName = "bid"};

            var parameters = new Parameters
                {
                    new StructParameter
                        {
                            {"symbol", "RHAT"},
                            {"limit", 2.25},
                            {"expires", new DateTime(2002,07,09,20,0,0)}
                        }
                };
            call2.SetParameters(parameters);
            rpc2.MethodCall = call2;

            rpc2.ShouldBe(METHOD_CALL1);
        }

        [Fact]
        public void MethodcallWithIq()
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

            iq.ShouldBe(METHOD_CALL_WITH_IQ);
        }

        [Fact]
        public void MethodcallWithIq2()
        {
            var iq = new RpcIq
            {
                Id = "4jLvG-34",
                To = "buyer@collabnexgen.net/Smack",
                From = "seller@collabnexgen.net/Smack",
                Type = Matrix.Xmpp.IqType.Set
            };

            var call = new MethodCall { MethodName = "com.collabng.net.remoteable.ViewpointRemoteable.setViewpoint" };

            var parameters = new Parameters
                {
                    "viewpoint_BackView_collabng"
                };
            call.SetParameters(parameters);
            iq.Rpc.MethodCall = call;

            iq.ShouldBe(METHOD_CALL_WITH_IQ_2);
        }

        [Fact]
        public void Test2()
        {
            var el = XmppXElement.LoadXml(METHOD_RESPONSE1);
            Assert.Equal(el is Matrix.Xmpp.Rpc.Rpc, true);

            var rpc = el as Matrix.Xmpp.Rpc.Rpc;

            var resp = rpc.MethodResponse;
            Assert.Equal(resp != null, true);
            
            var pars = resp.GetParameters();
        }

        [Fact]
        public void Test3()
        {
            var rpc = new Matrix.Xmpp.Rpc.Rpc();
            var call = new MethodCall {MethodName = "examples.getStateName"};

            var pars = new Parameters { 6 };
            call.SetParameters(pars);

            rpc.MethodCall = call;
            rpc.ShouldBe(METHOD_CALL2);
        }

        [Fact]
        public void Test4()
        {
            var rpc = new Matrix.Xmpp.Rpc.Rpc();
            var resp = new MethodResponse();

            var pars = new Parameters { "Colorado" };
            resp.SetParameters(pars);

            rpc.MethodResponse = resp;
            rpc.ShouldBe(METHOD_RESPONSE2);
        }

        [Fact]
        public void Test_Write_Fault()
        {
            var resp = new MethodResponse();
            Assert.Equal(resp.IsError, false);
            var pars = new Parameters { new XmlRpcException("Unknown stock symbol ABCD") { Code = 23 } };
            resp.SetParameters(pars);

            Assert.Equal(resp.IsError, true);
            resp.ShouldBe(METHOD_RESPONSE3);
        }

        [Fact]
        public void TestWithStructThatContainsAnArray()
        {
            var iq = new RpcIq
            {
                Id = "0815",
                Type = Matrix.Xmpp.IqType.Set
            };

            var call = new MethodCall { MethodName = "bid" };

            var parameters = new Parameters
                {
                    new StructParameter
                        {
                            {"symbol", "RHAT"},
                            {"limit", 2.25},
                            {"some_array", new Parameters{"A", "B", "C", "D"}}
                        }
                };
            call.SetParameters(parameters);
            iq.Rpc.MethodCall = call;
            iq.ShouldBe(METHOD_CALL_WITH_STRUCT_AND_NESTED_ARRAY);
        }
    }
}
