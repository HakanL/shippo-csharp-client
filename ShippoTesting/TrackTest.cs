using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class TrackTest : ShippoTest
    {
        private static readonly String TRACKING_NO = "9205590164917337534322";
        private static readonly String CARRIER = "usps";

        [Test]
        public void TestValidGetStatus()
        {
            Track track = GetAPIResource().RetrieveTracking(CARRIER, TRACKING_NO).Result;
            Assert.AreEqual(TRACKING_NO, track.TrackingNumber);
            Assert.IsNotNull(track.TrackingStatus);
            Assert.IsNotNull(track.TrackingHistory);
        }

        [Test]
        public void TestInvalidGetStatus()
        {
            Assert.That(() => GetAPIResource().RetrieveTracking(CARRIER, "INVALID_ID"),
                        Throws.TypeOf<ShippoException>());
        }

        [Test]
        public void TestValidRegisterWebhook()
        {
            Track track = GetAPIResource().RetrieveTracking(CARRIER, TRACKING_NO).Result;

            var parameters = new Dictionary<string, object>();
            parameters.Add("carrier", CARRIER);
            parameters.Add("tracking_number", track.TrackingNumber);
            Track register = GetAPIResource().RegisterTrackingWebhook(parameters).Result;
            Assert.IsNotNull(register.TrackingNumber);
            Assert.IsNotNull(register.TrackingHistory);
        }

        [Test]
        public void TestInvalidRegisterWebhook()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("carrier", CARRIER);
            parameters.Add("tracking_number", "INVALID_ID");
            Assert.That(() => GetAPIResource().RegisterTrackingWebhook(parameters),
                        Throws.TypeOf<ShippoException>());
        }
    }
}
