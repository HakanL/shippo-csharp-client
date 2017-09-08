using System;
using System.Collections.Generic;
using NUnit.Framework;

using Shippo;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class BatchTest : ShippoTest
    {
        void HandleFunc() { }

        [Test]
        public void TestValidCreate()
        {
            Batch testBatch = getDefaultObject();
            Assert.AreEqual(ShippoEnums.Statuses.VALIDATING, testBatch.Status);
        }

        [Test]
        public void TestInvalidCreate()
        {
            Assert.That(() => GetShippoClient().CreateBatch("invalid_carrier_account", "invalid_servicelevel_token", ShippoEnums.LabelFiletypes.NONE, "", new List<BatchShipment>()),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidRetrieve()
        {
            Batch batch = getDefaultObject();
            Batch retrieve = GetShippoClient().RetrieveBatch(batch.ObjectId, 0, ShippoEnums.ObjectResults.none).Result;
            Assert.AreEqual(batch.ObjectId, retrieve.ObjectId);
            Assert.AreEqual(batch.ObjectCreated, retrieve.ObjectCreated);
        }

        [Test]
        public void TestInvalidRetrieve()
        {
            Assert.That(() => GetShippoClient().RetrieveBatch("INVALID_ID", 0, ShippoEnums.ObjectResults.none),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidAddShipmentToBatch()
        {
            Batch batch = getDefaultObject();
            Assert.AreEqual(batch.Status, ShippoEnums.Statuses.VALIDATING);

            List<String> shipmentIds = new List<String>();
            Shipment shipment = ShipmentTest.GetDefaultObject();
            shipmentIds.Add(shipment.ObjectId);

            Batch retrieve = getValidBatch(batch.ObjectId);
            Batch newBatch = GetShippoClient().AddShipmentsToBatch(retrieve.ObjectId, shipmentIds).Result;

            Assert.AreEqual(retrieve.BatchShipments.Count + shipmentIds.Count, newBatch.BatchShipments.Count);
        }

        [Test]
        public void TestInvalidAddShipmentToBatch()
        {
            List<String> shipmentIds = new List<String>();
            shipmentIds.Add("123");
            Assert.That(() => GetShippoClient().AddShipmentsToBatch("INVALID_ID", shipmentIds),
                         Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidRemoveShipmentsFromBatch()
        {
            Batch batch = getDefaultObject();
            Assert.AreEqual(batch.Status, ShippoEnums.Statuses.VALIDATING);

            List<String> shipmentIds = new List<String>();
            Shipment shipment = ShipmentTest.GetDefaultObject();
            shipmentIds.Add(shipment.ObjectId);

            Batch retrieve = getValidBatch(batch.ObjectId);
            Batch addBatch = GetShippoClient().AddShipmentsToBatch(retrieve.ObjectId, shipmentIds).Result;

            string removeId = addBatch.BatchShipments.Results[0].ObjectId;
            List<String> shipmentsToRemove = new List<String>();
            shipmentsToRemove.Add(removeId);

            Batch removeBatch = GetShippoClient().RemoveShipmentsFromBatch(batch.ObjectId, shipmentsToRemove).Result;
            Assert.AreEqual(retrieve.BatchShipments.Count, removeBatch.BatchShipments.Count);
        }

        [Test]
        public void TestInvalidRemoveShipmentsFromBatch()
        {
            List<String> shipments = new List<String>();
            shipments.Add("123");
            Assert.That(() => GetShippoClient().RemoveShipmentsFromBatch("INVALID_ID", shipments),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidPurchase()
        {
            Batch batch = getDefaultObject();
            Batch retrieve = getValidBatch(batch.ObjectId);
            Batch purchase = GetShippoClient().PurchaseBatch(retrieve.ObjectId).Result;
            Assert.AreEqual(ShippoEnums.Statuses.PURCHASING, purchase.Status);
        }

        [Test]
        public void TestInvalidPurchase()
        {
            Assert.That(() => GetShippoClient().PurchaseBatch("INVALID_ID"),
                        Throws.TypeOf<ShippoException>());
        }

        /**
         * Retries up to 10 times to retrieve a batch that has been recently
         * created until the newly created batch is 'VALID' and not 'VALIDATING'.
         */
        public Batch getValidBatch(String id)
        {
            Batch batch;
            int retries = 10;
            for (; retries > 0; retries--)
            {
                batch = GetShippoClient().RetrieveBatch(id, 0, ShippoEnums.ObjectResults.none).Result;
                if (batch.Status == ShippoEnums.Statuses.VALID)
                    return batch;
                System.Threading.Thread.Sleep(1000);
            }
            throw new ShippoException("Could not retrieve valid Batch", new TimeoutException());
        }

        public static Batch getDefaultObject()
        {
            // Grab USPS carrier account to get the correct object ID for further testing.
            // This should be changed to be more generic in future versions of this test. In
            // other words, remove the depedence on a USPS carrier account to exist.
            ShippoCollection<CarrierAccount> carrierAccounts = GetShippoClient().AllCarrierAccounts().Result;
            string defaultCarrierAccount = "";
            foreach (CarrierAccount account in carrierAccounts)
            {
                if (account.Carrier.ToString() == "usps")
                    defaultCarrierAccount = account.ObjectId;
            }

            var addressFrom = CreateAddress.CreateForPurchase("Mr. Hippo", "965 Mission St.", "Ste 201", "SF",
                                                            "CA", "94103", "US", "4151234567", "ship@gmail.com");
            var addressTo = CreateAddress.CreateForPurchase("Mrs. Hippo", "965 Missions St.", "Ste 202", "SF",
                                                          "CA", "94103", "US", "4151234568", "msship@gmail.com");
            CreateParcel[] parcels = { CreateParcel.CreateForShipment(5, 5, 5, DistanceUnits.@in, 2, MassUnits.oz) };
            var shipment = CreateShipment.CreateForBatch(addressFrom, addressTo, parcels);
            var batchShipment = BatchShipment.CreateForBatchShipments(defaultCarrierAccount, "usps_priority", shipment);

            var batchShipments = new List<BatchShipment>();
            batchShipments.Add(batchShipment);

            Batch batch = GetShippoClient().CreateBatch(
                defaultCarrierAccount,
                "usps_priority",
                ShippoEnums.LabelFiletypes.PDF_4x6,
                "BATCH #170",
                batchShipments).Result;

            Assert.AreEqual(ShippoEnums.Statuses.VALIDATING, batch.Status);
            return batch;
        }
    }
}
