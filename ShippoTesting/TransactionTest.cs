using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;

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
        public void testListAll()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = shippoClient.AllTransactions(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static Transaction getDefaultObject()
        {
            var parameters = new Dictionary<string, object>();

            ShippoCollection<Rate> rateCollection = RateTest.GetDefaultObject();
            List<Rate> rateList = rateCollection.Data;
            Rate[] rateArray = rateList.ToArray();

            parameters.Add("rate", rateArray [0].ObjectId);
            parameters.Add("metadata", "Customer ID 123456");

            return GetShippoClient().CreateTransactionSync(parameters).Result;
        }
    }
}

