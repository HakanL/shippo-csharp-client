using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using Shippo;
using Shippo.Models;
using System.Threading.Tasks;

namespace ShippoTesting
{
    [TestFixture]
    public class CustomsDeclarationTest : ShippoTest
    {

        [Test]
        public async Task TestValidCreate()
        {
            CustomsDeclaration testObject = await CustomsDeclarationTest.GetDefaultObject();
            Assert.AreEqual(ShippoEnums.ObjectStates.VALID, testObject.ObjectState);
        }

        [Test]
        public async Task TestValidRetrieve()
        {
            CustomsDeclaration testObject = await CustomsDeclarationTest.GetDefaultObject();
            CustomsDeclaration retrievedObject;

            retrievedObject = await shippoClient.RetrieveCustomsDeclaration((string)testObject.ObjectId);
            Assert.AreEqual(testObject.ObjectId, retrievedObject.ObjectId);
        }

        [Test]
        public async Task TestListAll()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("results", "1");
            parameters.Add("page", "1");

            var parcels = await shippoClient.AllCustomsDeclarations(parameters);
            Assert.AreNotEqual(0, parcels.Data.Count);
        }

        public static async Task<CustomsDeclaration> GetDefaultObject()
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

            CustomsItem customsItem = await CustomsItemTest.GetDefaultObject();
            parameters.Items.Add(customsItem.ObjectId);

            return await GetShippoClient().CreateCustomsDeclaration(parameters);
        }
    }
}

