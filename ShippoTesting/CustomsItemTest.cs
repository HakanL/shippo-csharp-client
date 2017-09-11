using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting
{
    [TestFixture]
    public class CustomsItemTest : ShippoTest
    {
        [Test]
        public async Task TestValidCreate()
        {
            CustomsItem testObject = await CustomsItemTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            CustomsItem testObject = await CustomsItemTest.GetDefaultObject();
            CustomsItem retrievedObject;

            retrievedObject = await shippoClient.RetrieveCustomsItem(testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = await shippoClient.AllCustomsItems(parameters);
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static async Task<CustomsItem> GetDefaultObject()
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

            return await GetShippoClient().CreateCustomsItem(parameters);
        }
    }
}
