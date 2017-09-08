using NUnit.Framework;
using System;
using System.Collections;
using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class AddressTest : ShippoTest
    {
        [Test]
        public void TestValidCreate()
        {
            Address testObject = AddressTest.GetDefaultObject();
            Assert.AreEqual(true, testObject.IsComplete);
        }

        [Test]
        public void TestValidRetrieve()
        {
            Address testObject = AddressTest.GetDefaultObject();
            Address retrievedObject;

            retrievedObject = shippoClient.RetrieveAddress((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        public static Address GetDefaultObject()
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

            return GetShippoClient().CreateAddress(parameters).Result;
        }

        public static Address GetDefaultObject_2()
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

            return GetShippoClient().CreateAddress(parameters).Result;
        }
    }
}
