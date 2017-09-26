using NUnit.Framework;
using System;
using System.Collections;

using Shippo;
using System.Collections.Generic;

namespace ShippoTesting
{
    [TestFixture]
    public class ShippoTest
    {
        static internal ShippoClient shippoClient;
        public ShippoClient staticAPIResource;

        [SetUp]
        public void Init()
        {
            shippoClient = new ShippoClient("<YourShippoToken>");
        }

        public static ShippoClient GetShippoClient()
        {
            return shippoClient;
        }
    }
}
