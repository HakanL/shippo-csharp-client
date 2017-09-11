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
    public class AddressTest : ShippoTest
    {
        [Test]
        public async Task TestValidCreate()
        {
            Address testObject = await AddressTest.GetDefaultObject();
            Assert.AreEqual(true, testObject.IsComplete);
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            Address testObject = await AddressTest.GetDefaultObject();
            Address retrievedObject;

            retrievedObject = await shippoClient.RetrieveAddress(testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        public static async Task<Address> GetDefaultObject()
        {
            var parameters = new CreateAddress
            {
                Name = "Undefault New Wu",
                Company = "Shippo",
                StreetNo = "215",
                Street1 = "Clayton St.",
                Street2 = null,
                City = "San Francisco",
                State = "CA",
                PostalCode = "94117",
                Country = "US",
                Phone = "+1 555 341 9393",
                Email = "laura@goshipppo.com",
                Metadata = "Customer ID 123456",
                Validate = true
            };

            return await GetShippoClient().CreateAddress(parameters);
        }

        public static async Task<Address> GetDefaultObject2()
        {
            var parameters = new CreateAddress
            {
                Name = "Undefault New Wu",
                Company = "Shippo",
                Street1 = "Francis St.",
                StreetNo = "56",
                Street2 = null,
                City = "San Francisco",
                State = "CA",
                PostalCode = "94112",
                Country = "US",
                Phone = "+1 555 341 9393",
                Email = "hippo@goshipppo.com",
                Metadata = "Customer ID 123456"
            };

            return await GetShippoClient().CreateAddress(parameters);
        }
    }
}
