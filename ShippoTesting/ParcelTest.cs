using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting
{
    [TestFixture]
    public class ParcelTest : ShippoTest
    {

        [Test]
        public async Task TestValidCreate()
        {
            Parcel testObject = await ParcelTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            Parcel testObject = await ParcelTest.GetDefaultObject();
            Parcel retrievedObject;

            retrievedObject = await shippoClient.RetrieveParcel((string)testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "10");
            parameters.Add("page", "1");

            var parcels = await shippoClient.AllParcels(parameters);
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static async Task<Parcel> GetDefaultObject()
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

            return await GetShippoClient().CreateParcel(parameters);
        }
    }
}

