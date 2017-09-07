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
        static internal ShippoClient apiResource;
        static internal String now;
        public ShippoClient staticAPIResource;

        [SetUp]
        public void Init()
        {
            apiResource = new ShippoClient("<Shippo Token>");
            now = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
        }

        public static ShippoClient GetAPIResource()
        {
            return apiResource;
        }
    }
}


