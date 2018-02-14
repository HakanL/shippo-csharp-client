using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shippo
{
    public class ApiClient : IDisposable
    {
        private static readonly string userAgent = "Shippo CSharpBindings/4.0";
        private static readonly Encoding encoding = Encoding.UTF8;

        private string apiVersion;
        private string accessToken;
        private HttpClient httpClient;

        public ApiClient(string accessToken)
        {
            this.accessToken = accessToken;
            this.apiVersion = "2017-08-01";

            CreateNewHttpClient();
        }

        private void CreateNewHttpClient(int timeoutSeconds = 25)
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            this.httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

        public string ApiVersion
        {
            get { return this.apiVersion; }
            set { this.apiVersion = value; }
        }

        public int TimeoutSeconds
        {
            get { return (int)this.httpClient.Timeout.TotalSeconds; }
            set
            {
                CreateNewHttpClient(value);
            }
        }

        public void Dispose()
        {
            this.httpClient?.Dispose();
            this.httpClient = null;
        }

        protected HttpRequestMessage SetupRequest(HttpMethod method, string url)
        {
            var req = new HttpRequestMessage(method, url);

            req.Headers.Add("Authorization", "ShippoToken " + accessToken);
            if (apiVersion != null)
            {
                req.Headers.Add("Shippo-API-Version", apiVersion);
            }

            return req;
        }

        // GET Requests
        public async Task<T> DoRequestAsync<T>(string endpoint, HttpMethod method, string body = null)
        {
            var json = await DoRequestAsync(endpoint, method, body);

            return JsonConvert.DeserializeObject<T>(json);
        }

        // GET Requests Helper
        // Requests Main Function
        private async Task<string> DoRequestAsync(string endpoint, HttpMethod method, string body)
        {
            HttpRequestMessage request = SetupRequest(method, endpoint);
            if (body != null)
            {
                byte[] bytes = encoding.GetBytes(body);

                request.Content = new StringContent(body, encoding, "application/json");
            }

            var response = await this.httpClient.SendAsync(request);

            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(responseBody))
                    throw new ShippoException(responseBody, new Exception("Error from Shippo service"));

                response.EnsureSuccessStatusCode();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
