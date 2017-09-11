using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class ShipmentTest : ShippoTest
    {

        [Test]
        public void TestValidCreate()
        {
            Shipment testObject = ShipmentTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ShippingStatuses.SUCCESS, testObject.Status);
        }

        [Test]
        public void TestValidRetrieve()
        {
            Shipment testObject = ShipmentTest.GetDefaultObject();
            Shipment retrievedObject;

            retrievedObject = shippoClient.RetrieveShipment((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = shippoClient.AllShipments(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static Shipment GetDefaultObject()
        {
            Address addressFrom = AddressTest.GetDefaultObject();
            Address addressTo = AddressTest.GetDefaultObject2();
            Parcel parcel = ParcelTest.GetDefaultObject();

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

            return GetShippoClient().CreateShipment(parameters).Result;
        }
    }
}
