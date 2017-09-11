using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting
{
    [TestFixture]
    public class RateTest : ShippoTest
    {
        [Test]
        public async Task TestValidCreate()
        {
            ShippoCollection<Rate> testObject = await RateTest.GetDefaultObject();
            Assert.IsNotNull(testObject.Data);
        }

        public static async Task<ShippoCollection<Rate>> GetDefaultObject()
        {
            Shipment testObject = await ShipmentTest.GetDefaultObject();

            // Use Synchronized rates method
            return await GetShippoClient().GetShippingRatesSync(testObject.ObjectId, "USD");
        }
    }
}
