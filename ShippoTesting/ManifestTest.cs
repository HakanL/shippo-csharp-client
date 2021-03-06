﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting
{
    [TestFixture]
    public class ManifestTest : ShippoTest
    {

        [Test]
        public void TestInvalidCreate()
        {
            var invalidAddressTo = new CreateAddress
            {
                Name = "Undefault New Wu",
                Company = "Shippo",
                StreetNo = "215",
                Street1 = "Clayton St.",
                Street2 = null,
                City = "Mickey Town",
                State = "CA",
                PostalCode = "1183",
                Country = "US",
                Phone = "+1 555 9393",
                Email = "laura@goshipppo.com",
                Metadata = "Customer ID 123456",
                Validate = true
            };

            Assert.That(async () => await ManifestTest.GetDefaultObject(invalidAddressTo), Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            Manifest testObject = await ManifestTest.GetDefaultObject(await AddressTest.GetDefaultObject2());
            Manifest retrievedObject;

            retrievedObject = await shippoClient.RetrieveManifest(testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var manifests = await shippoClient.AllManifests(parameters);
            // This depends on the test account having at least one shipping manifest
            Assert.AreEqual(1, manifests.Data.Count);
        }

        public static async Task<Manifest> GetDefaultObject(BaseAddress addressTo)
        {
            var parameters0 = new CreateShipment();
            Address addressFrom = await AddressTest.GetDefaultObject();
            Parcel parcel = await ParcelTest.GetDefaultObject();
            parameters0.AddressFrom = addressFrom.ObjectId;
            parameters0.AddressTo = addressTo;
            parameters0.AddParcel(parcel.ObjectId);
            parameters0.ShipmentDate = DateTime.Now;
            parameters0.CustomsDeclaration = "";
            parameters0.Extra = new ShipmentExtra
            {
                Insurance = new ShipmentExtraInsurance
                {
                    Amount = 30,
                    Currency = "USD"
                },
                SignatureConfirmation = ShippoEnums.SignatureConfirmations.STANDARD
            };
            parameters0.Metadata = "Customer ID 123456";
            parameters0.Async = false;

            Shipment shipment = await GetShippoClient().CreateShipment(parameters0);

            ShippoCollection<Rate> rateCollection = await GetShippoClient().GetShippingRatesSync(shipment.ObjectId, "USD");
            List<Rate> rateList = rateCollection.Data;
            Rate[] rateArray = rateList.ToArray();

            var createTransaction = new CreateTransaction
            {
                Rate = rateArray[0].ObjectId,
                Metadata = "Customer ID 123456"
            };

            Transaction transaction = await GetShippoClient().CreateTransactionSync(createTransaction);

            var parameters2 = new CreateManifest
            {
                ShipmentDate = DateTime.Now,
                AddressFromObjectId = addressFrom.ObjectId,
                Provider = "usps"
            };

            var transactions = new List<string>();
            transactions.Add(transaction.ObjectId);
            parameters2.TransactionsIds = transactions.ToArray();

            return await GetShippoClient().CreateManifest(parameters2);
        }
    }
}
