using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting {
    [TestFixture]
    public class TransactionTest : ShippoTest {

        /* Intentionally commented; cannot be tested without proper billing credentials
        [Test]
        public void TestValidCreate()
        {
            Transaction testObject = TransactionTest.getDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public void testValidRetrieve()
        {
            Transaction testObject = TransactionTest.getDefaultObject();
            Transaction retrievedObject;

            retrievedObject = apiResource.RetrieveTransaction((string) testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        } */

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = await shippoClient.AllTransactions(parameters);
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static async Task<Transaction> GetDefaultObject()
        {
            var parameters = new Dictionary<string, object>();

            ShippoCollection<Rate> rateCollection = await RateTest.GetDefaultObject();
            List<Rate> rateList = rateCollection.Data;
            Rate[] rateArray = rateList.ToArray();

            parameters.Add("rate", rateArray [0].ObjectId);
            parameters.Add("metadata", "Customer ID 123456");

            return await GetShippoClient().CreateTransactionSync(parameters);
        }
    }
}

