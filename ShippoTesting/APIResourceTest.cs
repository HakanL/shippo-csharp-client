using NUnit.Framework;
using System;
using System.Linq;
using System.Net;

using Shippo;
using System.Net.Http;

namespace ShippoTesting
{
    [TestFixture]
    public class APIResourceTest : ShippoTest
    {
        public class MockAPIResource : ApiClient
        {
            public MockAPIResource(string inputToken) : base(inputToken) { }

            public HttpRequestMessage SetupRequestTest(HttpMethod method, string url)
            {
                return SetupRequest(method, url);
            }
        }

        [Test]
        public void TestValidHeader()
        {
            var dummyMethod = HttpMethod.Post;
            string dummyUrl = "http://example.com";
            string dummyApiToken = "1234abcd";
            string dummyApiVersion = "2014-02-11";
            var resource = new MockAPIResource(dummyApiToken);
            resource.ApiVersion = dummyApiVersion;
            HttpRequestMessage request = resource.SetupRequestTest(dummyMethod, dummyUrl);

            Assert.AreEqual("ShippoToken " + dummyApiToken, request.Headers.GetValues("Authorization").First());
            Assert.AreEqual(dummyApiVersion, request.Headers.GetValues("Shippo-API-Version").First());
        }
    }

}
