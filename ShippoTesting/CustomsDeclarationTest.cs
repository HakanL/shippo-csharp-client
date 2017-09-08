using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;

namespace ShippoTesting
{
    [TestFixture]
    public class CustomsDeclarationTest : ShippoTest
    {

        [Test]
        public void TestValidCreate()
        {
            CustomsDeclaration testObject = CustomsDeclarationTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public void TestValidRetrieve()
        {
            CustomsDeclaration testObject = CustomsDeclarationTest.GetDefaultObject();
            CustomsDeclaration retrievedObject;

            retrievedObject = shippoClient.RetrieveCustomsDeclaration((string)testObject.ObjectId).Result;
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public void TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = shippoClient.AllCustomsDeclarations(parameters).Result;
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static CustomsDeclaration GetDefaultObject()
        {
            var parameters = new CreateCustomsDeclaration
            {
                ExporterReference = "",
                ImporterReference = "",
                ContentsType = ShippoEnums.ContentsTypes.MERCHANDISE,
                ContentsExplanation = "T-Shirt purchase",
                Invoice = "#123123",
                License = "",
                Certificate = "",
                Notes = "",
                EelPfc = ShippoEnums.EelPfcs.NOEEI_30_37_a,
                AesItn = "",
                NonDeliveryOption = ShippoEnums.NonDeliveryOptions.ABANDON,
                Certify = true,
                CertifySigner = "Laura Behrens Wu",
                Disclaimer = "",
                Incoterm = null,
                Metadata = "Order ID #123123"
            };

            CustomsItem customsItem = CustomsItemTest.GetDefaultObject();
            parameters.Items.Add(customsItem.ObjectId);

            return GetShippoClient().CreateCustomsDeclaration(parameters).Result;
        }
    }
}

