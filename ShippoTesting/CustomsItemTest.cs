using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class CustomsItemTest : ShippoTest
    {
        [Test]
        public void TestValidCreate()
        {
            CustomsItem testObject = CustomsItemTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public void TestValidRetrieve()
        {
            CustomsItem testObject = CustomsItemTest.GetDefaultObject();
            CustomsItem retrievedObject;

            retrievedObject = shippoClient.RetrieveCustomsItem((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = shippoClient.AllCustomsItems(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static CustomsItem GetDefaultObject()
        {
            var parameters = new CreateCustomsItem
            {
                Description = "T-Shirt",
                Quantity = 2,
                NetWeight = 400,
                MassUnit = MassUnits.g,
                ValueAmount = 20,
                ValueCurrency = "USD",
                TariffNumber = "",
                OriginCountry = "US",
                Metadata = "Order ID #123123"
            };

            return GetShippoClient().CreateCustomsItem(parameters).Result;
        }
    }
}
