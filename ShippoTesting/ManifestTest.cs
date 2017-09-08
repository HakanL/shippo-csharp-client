using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class ManifestTest : ShippoTest
    {

        [Test]
        public void TestInvalidCreate()
        {
            Assert.That(() => ManifestTest.GetDefaultObject(), Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidRetrieve()
        {
            Manifest testObject = ManifestTest.GetDefaultObject();
            Manifest retrievedObject;

            retrievedObject = shippoClient.RetrieveManifest((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var Manifests = shippoClient.AllManifests(parameters).Result;
            Assert.AreEqual(0, Manifests.Data.Count);
        }

        public static Manifest GetDefaultObject()
        {
            var parameters0 = new CreateShipment();
            Address addressFrom = AddressTest.GetDefaultObject();
            Address addressTo = AddressTest.GetDefaultObject_2();
            Parcel parcel = ParcelTest.GetDefaultObject();
            parameters0.AddressFrom = addressFrom.ObjectId;
            parameters0.AddressTo = addressTo.ObjectId;
            //FIXME            parameters0.Add("parcels", new string[] { parcel.ObjectId });
            parameters0.ShipmentDate = DateTime.Now;
            //FIXME            parameters0.Add("insurance_amount", "30");
            //FIXME             parameters0.Add("insurance_currency", "USD");
            //FIXME             parameters0.Add("extra", "{signature_confirmation: true}");
            //FIXME             parameters0.Add("customs_declaration", "");
            parameters0.Metadata = "Customer ID 123456";
            parameters0.Async = false;

            Shipment shipment = GetShippoClient().CreateShipment(parameters0).Result;
            var parameters1 = new Dictionary<string, object>();
            parameters1.Add("id", shipment.ObjectId);
            parameters1.Add("currency_code", "USD");

            ShippoCollection<Rate> rateCollection = GetShippoClient().GetShippingRatesSync(parameters1).Result;
            List<Rate> rateList = rateCollection.Data;
            Rate[] rateArray = rateList.ToArray();

            parameters1.Add("rate", rateArray[0].ObjectId);
            parameters1.Add("metadata", "Customer ID 123456");

            Transaction transaction = GetShippoClient().CreateTransactionSync(parameters1).Result;

            var parameters2 = new Dictionary<string, object>();
            parameters2.Add("provider", "USPS");
            parameters2.Add("shipment_date", DateTime.Now);
            parameters2.Add("address_from", addressFrom.ObjectId);
            List<String> transactions = new List<String>();
            transactions.Add(transaction.ObjectId);
            parameters2.Add("transactions", transactions);

            return GetShippoClient().CreateManifest(parameters2).Result;
        }
    }
}

