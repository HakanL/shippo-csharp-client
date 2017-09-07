using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting {
    [TestFixture]
    public class ShipmentTest : ShippoTest {

        [Test]
        public void TestValidCreate()
        {
            Shipment testObject = ShipmentTest.getDefaultObject();
            Assert.AreEqual("SUCCESS", testObject.Status);
        }

        [Test]
        public void testValidRetrieve()
        {
            Shipment testObject = ShipmentTest.getDefaultObject();
            Shipment retrievedObject;

            retrievedObject = apiResource.RetrieveShipment((string) testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void testListAll()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = apiResource.AllShipments(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static Shipment getDefaultObject()
        {
            var parameters = new Dictionary<string, object>();
            Address addressFrom = AddressTest.GetDefaultObject();
            Address addressTo = AddressTest.GetDefaultObject_2();
            Parcel parcel = ParcelTest.getDefaultObject();
            parameters.Add("address_from", addressFrom.ObjectId);
            parameters.Add("address_to", addressTo.ObjectId);
            parameters.Add("parcels", new String[]{ parcel.ObjectId});
            parameters.Add("shipment_date", now);
            parameters.Add("insurance_amount", "30");
            parameters.Add("insurance_currency", "USD");
            parameters.Add("extra", "{signature_confirmation: true}");
            parameters.Add("customs_declaration", "");
            parameters.Add("metadata", "Customer ID 123456");
            parameters.Add("async", false);

            return GetAPIResource().CreateShipment(parameters).Result;
        }
    }
}

