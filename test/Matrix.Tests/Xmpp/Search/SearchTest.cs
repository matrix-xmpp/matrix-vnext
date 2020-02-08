/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Search;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Search
{
    public class SearchTest
    {
        [Fact]
        public void XmlShouldBeOfTypeSearch()
        {
            string xml = Resource.Get("Xmpp.Search.search_query1.xml");
            XmppXElement.LoadXml(xml).ShouldBeOfType<Matrix.Xmpp.Search.Search>();
        }

        [Fact]
        public void ReadInstructions()
        {
            string xml = Resource.Get("Xmpp.Search.search_query1.xml");
            var search = XmppXElement.LoadXml(xml).Cast<Matrix.Xmpp.Search.Search>();
            
            Assert.Equal(search.Email, "gnauck@ag-software.de");
        }

        [Fact]
        public void ReadEmail()
        {
            string xml = Resource.Get("Xmpp.Search.search_query1.xml");
            var search = XmppXElement.LoadXml(xml).Cast<Matrix.Xmpp.Search.Search>();
            
            Assert.Equal(search.Email, "gnauck@ag-software.de");
        }

        [Fact]
        public void XmlShouldBeOfTypeSearchItem()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            XmppXElement.LoadXml(xml).ShouldBeOfType<SearchItem>();
        }

        [Fact]
        public void CountItems()
        {
            string xml = Resource.Get("Xmpp.Search.search_query2.xml");
            var search = XmppXElement.LoadXml(xml).Cast<Matrix.Xmpp.Search.Search>();

            var items = search.GetItems();
            Assert.Equal(items != null, true);
            Assert.Equal(items.Count(), 2);
        }

        [Fact]
        public void TestSeachItemJid()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = XmppXElement.LoadXml(xml).Cast<SearchItem>();

            Assert.Equal(item.Jid.ToString(), "tybalt@shakespeare.lit");
        }

        [Fact]
        public void TestSeachItemFirst()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = XmppXElement.LoadXml(xml).Cast<SearchItem>();
            
            Assert.Equal(item.First, "Tybalt");}

        [Fact]
        public void TestSeachItemLast()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = XmppXElement.LoadXml(xml).Cast<SearchItem>();
            
            Assert.Equal(item.Nick, "ty");
            
        }

        [Fact]
        public void TestSeachItemNick()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = XmppXElement.LoadXml(xml).Cast<SearchItem>();
            
            Assert.Equal(item.Nick, "ty");
        }

        [Fact]
        public void TestSeachItemEmail()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = XmppXElement.LoadXml(xml).Cast<SearchItem>();

            Assert.Equal(item.Email, "tybalt@shakespeare.lit");
        }

        [Fact]
        public void TestCreateSearchItem()
        {
            string xml = Resource.Get("Xmpp.Search.search_query3.xml");
            var item = new SearchItem
                           {
                               Jid = "tybalt@shakespeare.lit",
                               First = "Tybalt",
                               Last = "Capulet",
                               Nick = "ty",
                               Email = "tybalt@shakespeare.lit"
                           };

            item.ShouldBe(xml);
        }

        [Fact]
        public void TestAllItemsWithproperties()
        {
            string xml = Resource.Get("Xmpp.Search.search_query4.xml");
            var iq = XmppXElement.LoadXml(xml).Cast<Iq>();

            var searchQuery = iq.Query.Cast<Matrix.Xmpp.Search.Search>();

            var first   = new[] {"Juliet", "Tybalt"};
            var jid     = new[] {"juliet@capulet.com", "tybalt@shakespeare.lit"};
            var last    = new[] {"Capulet", "Capulet"};
            var nick    = new[] {"JuliC", "ty"};
            var email   = new[] {"juliet@shakespeare.lit", "tybalt@shakespeare.lit"};

            int i = 0;
            foreach (var sItem in searchQuery.GetItems())
            {
                Assert.Equal(sItem.Jid.Bare, jid[i]);
                Assert.Equal(sItem.First, first[i]);
                Assert.Equal(sItem.Last, last[i]);
                Assert.Equal(sItem.First, first[i]);
                Assert.Equal(sItem.Nick, nick[i]);
                Assert.Equal(sItem.Email, email[i]);
                i++;
            }
        }

        [Fact]
        public void TestBuildSearchQuery()
        {
            string expectedXml = Resource.Get("Xmpp.Search.search_query4.xml");

            var searchQuery = new IqQuery<Matrix.Xmpp.Search.Search>()
            {
                Type    = Matrix.Xmpp.IqType.Result,
                From    = "search.shakespeare.lit",
                Id      = "search2"
            };
            
            var first   = new[] { "Juliet", "Tybalt" };
            var jid     = new[] { "juliet@capulet.com", "tybalt@shakespeare.lit" };
            var last    = new[] { "Capulet", "Capulet" };
            var nick    = new[] { "JuliC", "ty" };
            var email   = new[] { "juliet@shakespeare.lit", "tybalt@shakespeare.lit" };
            
            for (int i = 0; i < 2; i++)
            {
                searchQuery.Query.AddItem(
                    new SearchItem
                    {
                        Jid = jid[i],
                        First = first[i],
                        Last = last[i],
                        Nick = nick[i],
                        Email = email[i]
                    }
                );
            }

            searchQuery.ShouldBe(expectedXml);
        }
    }
}
