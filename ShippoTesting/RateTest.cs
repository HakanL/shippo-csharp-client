using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class RateTest : ShippoTest
    {
        [Test]
        public void TestValidCreate()
        {
            ShippoCollection<Rate> testObject = RateTest.GetDefaultObject();
            Assert.IsNotNull(testObject.Data);
        }

        public static ShippoCollection<Rate> GetDefaultObject()
        {
            Shipment testObject = ShipmentTest.GetDefaultObject();
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", testObject.ObjectId);
            parameters.Add("currency_code", "USD");

            // Use Synchronized rates method
            return GetShippoClient().GetShippingRatesSync(parameters).Result;
        }
    }
}
