using System;
using System.Linq;
using System.Xml.Linq;
using Shouldly;
using Matrix.Xml;
using Matrix.Xmpp.XData;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.XData
{
    public class XDataTest
    {
        string XML1 = @"<x xmlns='jabber:x:data'
               type='result'>
              <reported>
                <field var='field-name' label='description' type='{field-type}'/>
              </reported>
              <item>
                <field var='field-name'>
                  <value>field-value</value>
                </field>
              </item>
              <item>
                <field var='field-name'>
                  <value>field-value</value>
                </field>
              </item>
            </x>";

        private const string XML2 = @"<x xmlns='jabber:x:data' type='result'>
                  <title>Joogle Search: verona</title>
                  <reported>
                    <field var='name'/>
                    <field var='url'/>
                  </reported>
                  <item>
                    <field var='name'>
                      <value>Comune di Verona - Benvenuti nel sito ufficiale</value>
                    </field>
                    <field var='url'>
                      <value>http://www.comune.verona.it/</value>
                    </field>
                  </item>
                  <item>
                    <field var='name'>
                      <value>benvenuto!</value>
                    </field>
                    <field var='url'>
                      <value>http://www.hellasverona.it/</value>
                    </field>
                  </item>
                  <item>
                    <field var='name'>
                      <value>Universita degli Studi di Verona - Home Page</value>
                    </field>
                    <field var='url'>
                      <value>http://www.univr.it/</value>
                    </field>
                  </item>
                  <item>
                    <field var='name'>
                      <value>Aeroporti del Garda</value>
                    </field>
                    <field var='url'>
                      <value>http://www.aeroportoverona.it/</value>
                    </field>
                  </item>
                  <item>
                    <field var='name'>
                      <value>Veronafiere - fiera di Verona</value>
                    </field>
                    <field var='url'>
                      <value>http://www.veronafiere.it/</value>
                    </field>
                  </item>
                </x>";

        [Fact]
        public void TestReported()
        {
            XmppXElement xmpp = XmppXElement.LoadXml(XML1);

            Assert.True(xmpp is Data);
            var data  = xmpp as Data;

            var reported = data.Element<Reported>();

            Assert.True(reported != null);

        }

        [Fact]
        public void TestGetItems()
        {
            XmppXElement xmpp = XmppXElement.LoadXml(XML2);

            Assert.True(xmpp is Data);
            
            var data = xmpp as Data;
            Assert.Equal(data.Title, "Joogle Search: verona");
            
            var items = data.GetItems();
            Assert.True(items != null);
            Assert.Equal(items.Count(), 5);

        }

        [Fact]
        public void BuildFromWithFields()
        {
            string XML1 = @"<x xmlns='jabber:x:data'
               type='result'>                            
                <field var='var1'>
                  <value>value1</value>
                </field>                            
                <field var='var2'>
                  <value>value2</value>
                </field>              
            </x>";

            var data = new Data
            {
                Type = FormType.Result,
                Fields = new [] { new Field("var1", "value1"), new Field("var2", "value2") }
            };
            
            data.ShouldBe(XML1);
        }

        [Fact]
        public void BuildFieldWithValues()
        {
            string XML1 = @"<field xmlns='jabber:x:data' var='pubsub#children'>
	     		<value>queue1</value>
	     		<value>queue2</value>
	     		<value>queue3</value>
	     	</field>";

            var field = new Field
            {
                Var = "pubsub#children",
                Values = new[] { "queue1", "queue2", "queue3" }
            };

            field.ShouldBe(XML1);

            field.Nodes().Remove();
            field.Values.Length.ShouldBe(0);
        }


        [Fact]
        public void TestFieldTypeTextSingle()
        {
            string xml = @"<field var='muc#roomconfig_roomname' type='text-single' label='Natural-Language Room Name' xmlns='jabber:x:data'/>";

            var el = XmppXElement.LoadXml(xml);

            el.ShouldBeOfType<Field>();

            var field = el as Field;
            field.Type.ShouldBe(FieldType.TextSingle);
            
            var f = new Field {Type = FieldType.TextSingle, Var = "muc#roomconfig_roomname", Label = "Natural-Language Room Name" };
            f.ShouldBe(xml);
        }
    }
}
