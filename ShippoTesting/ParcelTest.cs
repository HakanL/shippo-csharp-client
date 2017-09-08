using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class ParcelTest : ShippoTest
    {

        [Test]
        public void TestValidCreate()
        {
            Parcel testObject = ParcelTest.GetDefaultObject();
            Assert.AreEqual("VALID", testObject.ObjectState);
        }

        [Test]
        public void TestValidRetrieve()
        {
            Parcel testObject = ParcelTest.GetDefaultObject();
            Parcel retrievedObject;

            retrievedObject = apiResource.RetrieveParcel((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "10");
            parameters.Add("page", "1");

            var parcels = apiResource.AllParcels(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static Parcel GetDefaultObject()
        {
            var parameters = new CreateParcel
            {
                Length = 5,
                Width = 5,
                Height = 5,
                DistanceUnit = DistanceUnits.cm,
                Weight = 2,
                MassUnit = MassUnits.lb,
                Template = null,
                Metadata = "Customer ID 123456"
            };

            return GetAPIResource().CreateParcel(parameters).Result;
        }
    }
}

