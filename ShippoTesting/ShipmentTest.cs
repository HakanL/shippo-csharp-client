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
            Assert.AreEqual("SUCCESS", testObject.Status);
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
            Address addressTo = AddressTest.GetDefaultObject_2();
            Parcel parcel = ParcelTest.GetDefaultObject();

            var parameters = new CreateShipment
            {
                AddressFrom = addressFrom.ObjectId,
                AddressTo = addressTo.ObjectId,
                Async = false,
                Metadata = "Customer ID 123456",
                ShipmentDate = DateTime.Now
            };
            //FIXME     parameters.Add("parcels", new String[]{ parcel.ObjectId});
            //FIXME     parameters.Add("insurance_amount", "30");
            //FIXME     parameters.Add("insurance_currency", "USD");
            //FIXME     parameters.Add("extra", "{signature_confirmation: true}");
            //FIXME     parameters.Add("customs_declaration", "");

            return GetShippoClient().CreateShipment(parameters).Result;
        }
    }
}
