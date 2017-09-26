using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task TestValidCreate()
        {
            Batch testBatch = await GetDefaultObject();
            Assert.AreEqual(ShippoEnums.Statuses.VALIDATING, testBatch.Status);
        }

        [Test]
        public void TestInvalidCreate()
        {
            var createBatch = new CreateBatch
            {
                DefaultCarrierAccount = "invalid_carrier_account",
                DefaultServicelevelToken = "invalid_servicelevel_token",
                LabelFiletype = null,
                Metadata = ""
            };
            Assert.That(async () => await GetShippoClient().CreateBatch(createBatch), Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            Batch batch = await GetDefaultObject();
            Batch retrieve = await GetShippoClient().RetrieveBatch(batch.ObjectId, 0, ShippoEnums.ObjectResults.none);
            Assert.AreEqual(batch.ObjectId, retrieve.ObjectId);
            Assert.AreEqual(batch.ObjectCreated, retrieve.ObjectCreated);
        }

        [Test]
        public void TestInvalidRetrieve()
        {
            Assert.That(async () => await GetShippoClient().RetrieveBatch("INVALID_ID", 0, ShippoEnums.ObjectResults.none),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidAddShipmentToBatch()
        {
            Batch batch = await GetDefaultObject();
            Assert.AreEqual(batch.Status, ShippoEnums.Statuses.VALIDATING);

            Shipment shipment = await ShipmentTest.GetDefaultObject();

            Batch retrieve = await GetValidBatch(batch.ObjectId);
            Batch newBatch = await GetShippoClient().AddShipmentsToBatch(retrieve.ObjectId, new CreateBatchShipment[] {
                new CreateBatchShipment
                {
                    Shipment = shipment.ObjectId
                }
            });

            Assert.AreEqual(retrieve.BatchShipments.Count + 1, newBatch.BatchShipments.Count);
        }

        [Test]
        public void TestInvalidAddShipmentToBatch()
        {
            Assert.That(async () => await GetShippoClient().AddShipmentsToBatch("INVALID_ID", new CreateBatchShipment[] {
                new CreateBatchShipment
                {
                    Shipment = "123"
                }
            }), Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidRemoveShipmentsFromBatch()
        {
            Batch batch = await GetDefaultObject();
            Assert.AreEqual(batch.Status, ShippoEnums.Statuses.VALIDATING);

            var shipmentIds = new List<string>();
            Shipment shipment = await ShipmentTest.GetDefaultObject();
            shipmentIds.Add(shipment.ObjectId);

            Batch retrieve = await GetValidBatch(batch.ObjectId);
            Batch addBatch = await GetShippoClient().AddShipmentsToBatch(retrieve.ObjectId, shipmentIds);

            string removeId = addBatch.BatchShipments.Results[0].ObjectId;
            var shipmentsToRemove = new List<string>();
            shipmentsToRemove.Add(removeId);

            Batch removeBatch = await GetShippoClient().RemoveShipmentsFromBatch(batch.ObjectId, shipmentsToRemove);
            Assert.AreEqual(retrieve.BatchShipments.Count, removeBatch.BatchShipments.Count);
        }

        [Test]
        public void TestInvalidRemoveShipmentsFromBatch()
        {
            var shipments = new List<string>();
            shipments.Add("123");
            Assert.That(async () => await GetShippoClient().RemoveShipmentsFromBatch("INVALID_ID", shipments),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidPurchase()
        {
            Batch batch = await GetDefaultObject();

            Batch retrieve = await GetValidBatch(batch.ObjectId);
            Assert.AreEqual(ShippoEnums.Statuses.VALID, retrieve.Status);

            Batch purchase = await GetShippoClient().PurchaseBatch(retrieve.ObjectId);
            Assert.AreEqual(ShippoEnums.Statuses.PURCHASING, purchase.Status);
        }

        [Test]
        public void TestInvalidPurchase()
        {
            Assert.That(async () => await GetShippoClient().PurchaseBatch("INVALID_ID"),
                        Throws.TypeOf<ShippoException>());
        }

        /**
         * Retries up to 10 times to retrieve a batch that has been recently
         * created until the newly created batch is 'VALID' and not 'VALIDATING'.
         */
        public async Task<Batch> GetValidBatch(string id)
        {
            Batch batch;
            int retries = 10;
            for (; retries > 0; retries--)
            {
                batch = await GetShippoClient().RetrieveBatch(id, 0, ShippoEnums.ObjectResults.none);
                if (batch.Status != ShippoEnums.Statuses.VALIDATING)
                    return batch;
                System.Threading.Thread.Sleep(1000);
            }
            throw new ShippoException("Could not retrieve valid Batch", new TimeoutException());
        }

        public static async Task<Batch> GetDefaultObject()
        {
            // Grab USPS carrier account to get the correct object ID for further testing.
            // This should be changed to be more generic in future versions of this test. In
            // other words, remove the depedence on a USPS carrier account to exist.
            ShippoCollection<CarrierAccount> carrierAccounts = await GetShippoClient().AllCarrierAccounts();
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
            var batchShipment = CreateBatchShipment.CreateForBatchShipments(defaultCarrierAccount, "usps_priority", shipment);

            var batchShipments = new List<CreateBatchShipment>();
            batchShipments.Add(batchShipment);

            Batch batch = await GetShippoClient().CreateBatch(new CreateBatch
            {
                DefaultCarrierAccount = defaultCarrierAccount,
                DefaultServicelevelToken = "usps_priority",
                LabelFiletype = ShippoEnums.LabelFiletypes.PDF_4x6,
                Metadata = "BATCH #170",
                BatchShipments = batchShipments
            });

            Assert.AreEqual(ShippoEnums.Statuses.VALIDATING, batch.Status);
            return batch;
        }
    }
}
