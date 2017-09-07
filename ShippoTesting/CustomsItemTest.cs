using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting {
    [TestFixture]
    public class CustomsItemTest : ShippoTest {

        [Test]
        public void TestValidCreate()
        {
            CustomsItem testObject = CustomsItemTest.getDefaultObject();
            Assert.AreEqual("VALID", testObject.ObjectState);
        }

        [Test]
        public void testValidRetrieve()
        {
            CustomsItem testObject = CustomsItemTest.getDefaultObject();
            CustomsItem retrievedObject;

            retrievedObject = apiResource.RetrieveCustomsItem((string) testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void testListAll()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = apiResource.AllCustomsItems(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static CustomsItem getDefaultObject()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("description", "T-Shirt");
            parameters.Add("quantity", "2");
            parameters.Add("net_weight", "400");
            parameters.Add("mass_unit", "g");
            parameters.Add("value_amount", "20");
            parameters.Add("value_currency", "USD");
            parameters.Add("tariff_number", "");
            parameters.Add("origin_country", "US");
            parameters.Add("metadata", "Order ID #123123");

            return GetAPIResource().CreateCustomsItem(parameters).Result;
        }
    }
}

