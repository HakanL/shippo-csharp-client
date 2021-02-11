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
    public class TrackTest : ShippoTest
    {
        private static readonly string TRACKING_NO = "9205590164917337534322";
        private static readonly string CARRIER = "usps";

        [Test]
        public async Task TestValidGetStatus()
        {
            Track track = await GetShippoClient().RetrieveTracking(CARRIER, TRACKING_NO);
            Assert.AreEqual(TRACKING_NO, track.TrackingNumber);
            Assert.IsNotNull(track.TrackingStatus);
            Assert.IsNotNull(track?.TrackingStatus.Substatus);
            Assert.IsNotNull(track.TrackingHistory);
        }

        [Test]
        public void TestInvalidGetStatus()
        {
            Assert.That(async () => await GetShippoClient().RetrieveTracking(CARRIER, "INVALID_ID"),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public async Task TestValidRegisterWebhook()
        {
            Track track = await GetShippoClient().RetrieveTracking(CARRIER, TRACKING_NO);

            Track register = await GetShippoClient().RegisterTrackingWebhook(CARRIER, track.TrackingNumber);

            Assert.IsNotNull(register.TrackingNumber);
            Assert.IsNotNull(register.TrackingHistory);
        }

        [Test]
        public void TestInvalidRegisterWebhook()
        {
            Assert.That(async () => await GetShippoClient().RegisterTrackingWebhook(CARRIER, "INVALID_ID"),
                        Throws.TypeOf<ShippoException>());
        }
    }
}
