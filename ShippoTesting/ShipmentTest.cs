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
    public class ShipmentTest : ShippoTest
    {

        [Test]
        public async Task TestValidCreate()
        {
            Shipment testObject = await ShipmentTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ShippingStatuses.SUCCESS, testObject.Status);
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            Shipment testObject = await ShipmentTest.GetDefaultObject();
            Shipment retrievedObject;

            retrievedObject = await shippoClient.RetrieveShipment((string)testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = await shippoClient.AllShipments(parameters);
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static async Task<Shipment> GetDefaultObject()
        {
            Address addressFrom = await AddressTest.GetDefaultObject();
            Address addressTo = await AddressTest.GetDefaultObject2();
            Parcel parcel = await ParcelTest.GetDefaultObject();

            var parameters = new CreateShipment
            {
                AddressFrom = addressFrom.ObjectId,
                AddressTo = addressTo.ObjectId,
                Async = false,
                Metadata = "Customer ID 123456",
                ShipmentDate = DateTime.Now
            };
            parameters.AddParcel(parcel.ObjectId);
            parameters.CustomsDeclaration = "";
            parameters.Extra = new ShipmentExtra
            {
                Insurance = new ShipmentExtraInsurance
                {
                    Amount = 30,
                    Currency = "USD"
                },
                SignatureConfirmation = ShippoEnums.SignatureConfirmations.STANDARD
            };

            return await GetShippoClient().CreateShipment(parameters);
        }
    }
}
