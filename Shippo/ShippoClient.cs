/*
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Shippo.Models;

namespace Shippo
{
    public class ShippoClient : IDisposable
    {
        public static readonly int RatesReqTimeout = 25;
        public static readonly int TransactionReqTimeout = 25;
        public static readonly string apiEndpoint = "https://api.goshippo.com/v1";
        private ApiClient apiClient;

        // API Resource Constructor
        public ShippoClient(string inputToken)
        {
            this.apiClient = new ApiClient(inputToken);
        }

        public void Dispose()
        {
            this.apiClient?.Dispose();
            this.apiClient = null;
        }

        public int TimeoutSeconds
        {
            get { return this.apiClient.TimeoutSeconds; }
            set { this.apiClient.TimeoutSeconds = value; }
        }

        public string ApiVersion
        {
            get { return this.apiClient.ApiVersion; }
            set { this.apiClient.ApiVersion = value; }
        }

        private string GenerateURLEncodedFromHashmap(IDictionary<string, string> propertyMap)
        {
            var str = new StringBuilder();

            if (propertyMap != null)
            {
                foreach (var pair in propertyMap)
                {
                    str.AppendFormat("{0}={1}&", pair.Key, pair.Value);
                }
                if (str.Length > 0)
                    str.Length--;
            }

            return str.ToString();
        }

        // Serialize parameters into JSON for POST requests
        private string Serialize<T>(T data)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return JsonConvert.SerializeObject(data, settings);
        }


        #region Address

        public async Task<Address> CreateAddress(CreateAddress createAddress)
        {
            string ep = string.Format("{0}/addresses", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Address>(ep, HttpMethod.Post, Serialize(createAddress));
        }

        public async Task<Address> RetrieveAddress(string id)
        {
            string ep = string.Format("{0}/addresses/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Address>(ep, HttpMethod.Get);
        }

        public async Task<Address> ValidateAddress(string id)
        {
            string ep = string.Format("{0}/addresses/{1}/validate", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Address>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Address>> AllAddresses(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/addresses?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Address>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Parcel

        public async Task<Parcel> CreateParcel(CreateParcel createParcel)
        {
            string ep = string.Format("{0}/parcels", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Parcel>(ep, HttpMethod.Post, Serialize(createParcel));
        }

        public async Task<Parcel> RetrieveParcel(string id)
        {
            string ep = string.Format("{0}/parcels/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Parcel>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Parcel>> AllParcels(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/parcels?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Parcel>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Shipment

        public async Task<Shipment> CreateShipment(CreateShipment createShipment)
        {
            string ep = string.Format("{0}/shipments", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Shipment>(ep, HttpMethod.Post, Serialize(createShipment));
        }

        public async Task<Shipment> RetrieveShipment(string id)
        {
            string ep = string.Format("{0}/shipments/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Shipment>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Shipment>> AllShipments(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/shipments?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Shipment>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Rate

        public async Task<ShippoCollection<Rate>> GetShippingRates(string shipmentId, string currencyCode)
        {
            string ep = string.Format("{0}/shipments/{1}/rates/{2}", apiEndpoint, shipmentId, currencyCode);
            return await this.apiClient.DoRequestAsync<ShippoCollection<Rate>>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Rate>> GetShippingRatesSync(string shipmentId, string currencyCode = "")
        {
            Shipment shipment = await RetrieveShipment(shipmentId);
            ShippoEnums.ShippingStatuses shippingStatus = shipment.Status;
            long startTime = DateTimeExtensions.UnixTimeNow();

            while (shippingStatus == ShippoEnums.ShippingStatuses.QUEUED || shippingStatus == ShippoEnums.ShippingStatuses.WAITING)
            {
                if (DateTimeExtensions.UnixTimeNow() - startTime > RatesReqTimeout)
                {
                    throw new RequestTimeoutException(
                        "A timeout has occured while waiting for your rates to generate. Try retrieving the Shipment object again and check if object_status is updated. If this issue persists, please contact support@goshippo.com");
                }
                shipment = await RetrieveShipment(shipmentId);
                shippingStatus = shipment.Status;
            }

            return await GetShippingRates(shipmentId, currencyCode);
        }

        public async Task<Rate> RetrieveRate(string id)
        {
            string ep = string.Format("{0}/rates/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Rate>(ep, HttpMethod.Get);
        }

        #endregion

        #region Transaction

        public async Task<Transaction> CreateTransaction(CreateTransaction createTransaction)
        {
            string ep = string.Format("{0}/transactions", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Transaction>(ep, HttpMethod.Post, Serialize(createTransaction));
        }

        public async Task<Transaction> CreateTransactionSync(CreateTransaction createTransaction)
        {
            string ep = string.Format("{0}/transactions", apiEndpoint);
            Transaction transaction = await this.apiClient.DoRequestAsync<Transaction>(ep, HttpMethod.Post, Serialize(createTransaction));
            string transactionId = transaction.ObjectId;
            var transactionStatus = transaction.Status;
            long startTime = DateTimeExtensions.UnixTimeNow();

            while (transactionStatus == ShippoEnums.TransactionStatuses.QUEUED || transactionStatus == ShippoEnums.TransactionStatuses.WAITING)
            {
                if (DateTimeExtensions.UnixTimeNow() - startTime > TransactionReqTimeout)
                {
                    throw new RequestTimeoutException(
                        "A timeout has occured while waiting for your label to generate. Try retrieving the Transaction object again and check if object_status is updated. If this issue persists, please contact support@goshippo.com");
                }
                transaction = await RetrieveTransaction(transactionId);
                transactionStatus = transaction.Status;
            }

            return transaction;
        }

        public async Task<Transaction> RetrieveTransaction(string id)
        {
            string ep = string.Format("{0}/transactions/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Transaction>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Transaction>> AllTransactions(IDictionary<string, string> parameters)
        {
            string ep = string.Format("{0}/transactions?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Transaction>>(ep, HttpMethod.Get);
        }

        #endregion

        #region CustomsItem

        public async Task<CustomsItem> CreateCustomsItem(CreateCustomsItem createCustomsItem)
        {
            string ep = string.Format("{0}/customs/items", apiEndpoint);
            return await this.apiClient.DoRequestAsync<CustomsItem>(ep, HttpMethod.Post, Serialize(createCustomsItem));
        }

        public async Task<CustomsItem> RetrieveCustomsItem(string id)
        {
            string ep = string.Format("{0}/customs/items/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<CustomsItem>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<CustomsItem>> AllCustomsItems(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/customs/items?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<CustomsItem>>(ep, HttpMethod.Get);
        }

        #endregion

        #region CustomsDeclaration

        public async Task<CustomsDeclaration> CreateCustomsDeclaration(CreateCustomsDeclaration createCustomsDeclaration)
        {
            string ep = string.Format("{0}/customs/declarations", apiEndpoint);
            return await this.apiClient.DoRequestAsync<CustomsDeclaration>(ep, HttpMethod.Post, Serialize(createCustomsDeclaration));
        }

        public async Task<CustomsDeclaration> RetrieveCustomsDeclaration(string id)
        {
            string ep = string.Format("{0}/customs/declarations/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<CustomsDeclaration>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<CustomsDeclaration>> AllCustomsDeclarations(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/customs/declarations?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<CustomsDeclaration>>(ep, HttpMethod.Get);
        }

        #endregion

        #region CarrierAccount

        public async Task<CarrierAccount> CreateCarrierAccount(CreateCarrierAccount createCarrierAccount)
        {
            string ep = string.Format("{0}/carrier_accounts", apiEndpoint);
            return await this.apiClient.DoRequestAsync<CarrierAccount>(ep, HttpMethod.Post, Serialize(createCarrierAccount));
        }

        public async Task<CarrierAccount> RetrieveCarrierAccount(string carrierAccountId)
        {
            string ep = string.Format("{0}/carrier_accounts/{1}", apiEndpoint, carrierAccountId);
            return await this.apiClient.DoRequestAsync<CarrierAccount>(ep, HttpMethod.Get);
        }

        public async Task<CarrierAccount> UpdateCarrierAccount(string carrierAccountId, IDictionary<string, string> parameters = null, bool? active = null)
        {
            string ep = string.Format("{0}/carrier_accounts/{1}", apiEndpoint, carrierAccountId);

            var updateCarrierAccount = new UpdateCarrierAccount
            {
                Parameters = parameters,
                Active = active
            };

            return await this.apiClient.DoRequestAsync<CarrierAccount>(ep, HttpMethod.Put, Serialize(parameters));
        }

        public async Task<ShippoCollection<CarrierAccount>> AllCarrierAccounts(IDictionary<string, string> parameters = null)
        {
            string ep = string.Format("{0}/carrier_accounts?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<CarrierAccount>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Refund

        public async Task<Refund> CreateRefund(CreateRefund createRefund)
        {
            string ep = string.Format("{0}/refunds", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Refund>(ep, HttpMethod.Post, Serialize(createRefund));
        }

        public async Task<Refund> RetrieveRefund(string id)
        {
            string ep = string.Format("{0}/refunds/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Refund>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Refund>> AllRefunds(IDictionary<string, string> parameters)
        {
            string ep = string.Format("{0}/refunds?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Refund>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Manifest

        public async Task<Manifest> CreateManifest(CreateManifest createManifest)
        {
            string ep = string.Format("{0}/manifests", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Manifest>(ep, HttpMethod.Post, Serialize(createManifest));
        }

        public async Task<Manifest> RetrieveManifest(string id)
        {
            string ep = string.Format("{0}/manifests/{1}", apiEndpoint, id);
            return await this.apiClient.DoRequestAsync<Manifest>(ep, HttpMethod.Get);
        }

        public async Task<ShippoCollection<Manifest>> AllManifests(IDictionary<string, string> parameters)
        {
            string ep = string.Format("{0}/manifests?{1}", apiEndpoint, GenerateURLEncodedFromHashmap(parameters));
            return await this.apiClient.DoRequestAsync<ShippoCollection<Manifest>>(ep, HttpMethod.Get);
        }

        #endregion

        #region Batch

        public async Task<Batch> CreateBatch(CreateBatch createBatch)
        {
            string ep = string.Format("{0}/batches", apiEndpoint);
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Post, Serialize(createBatch));
        }

        public async Task<Batch> RetrieveBatch(string id, uint page, ShippoEnums.ObjectResults objectResults)
        {
            string ep = string.Format("{0}/batches/{1}", apiEndpoint, System.Net.WebUtility.HtmlEncode(id));
            var queryParams = new StringBuilder();
            if (page > 0)
                queryParams.AppendFormat("&page={0}", page);
            if (objectResults != ShippoEnums.ObjectResults.none)
                queryParams.AppendFormat("&object_results={0}", objectResults);
            if (queryParams.Length > 0)
            {
                string parameters = queryParams.ToString().TrimStart('&');
                ep = string.Format("{0}?{1}", ep, parameters);
            }
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Get);
        }

        public async Task<Batch> AddShipmentsToBatch(string batchId, IEnumerable<CreateBatchShipment> shipments)
        {
            string ep = string.Format("{0}/batches/{1}/add_shipments", apiEndpoint, System.Net.WebUtility.HtmlEncode(batchId));
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Post, Serialize(shipments));
        }

        public async Task<Batch> AddShipmentsToBatch(string batchId, IEnumerable<string> shipmentIds)
        {
            var list = new List<CreateBatchShipment>();
            foreach (string shipmentId in shipmentIds)
            {
                var createBatchShipment = new CreateBatchShipment
                {
                    Shipment = shipmentId
                };
                list.Add(createBatchShipment);
            }
            string ep = string.Format("{0}/batches/{1}/add_shipments", apiEndpoint, System.Net.WebUtility.HtmlEncode(batchId));
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Post, Serialize(list));
        }

        public async Task<Batch> RemoveShipmentsFromBatch(string batchId, IEnumerable<string> shipmentIds)
        {
            string ep = string.Format("{0}/batches/{1}/remove_shipments", apiEndpoint, System.Net.WebUtility.HtmlEncode(batchId));
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Post, Serialize(shipmentIds));
        }

        public async Task<Batch> PurchaseBatch(string id)
        {
            string ep = string.Format("{0}/batches/{1}/purchase", apiEndpoint, System.Net.WebUtility.HtmlEncode(id));
            return await this.apiClient.DoRequestAsync<Batch>(ep, HttpMethod.Post);
        }

        #endregion

        #region Track

        public async Task<Track> RetrieveTracking(string carrier, string trackingNumber)
        {
            string encodedCarrier = System.Net.WebUtility.HtmlEncode(carrier);
            string encodedTrk = System.Net.WebUtility.HtmlEncode(trackingNumber);
            string ep = string.Format("{0}/tracks/{1}/{2}", apiEndpoint, encodedCarrier, encodedTrk);
            return await this.apiClient.DoRequestAsync<Track>(ep, HttpMethod.Get);
        }

        public async Task<Track> RegisterTrackingWebhook(string carrier, string trackingNumber, string metadata = null)
        {
            // For now the trailing '/' is required.
            string ep = string.Format("{0}/tracks/", apiEndpoint);

            var createTrack = new CreateTrack
            {
                Carrier = carrier,
                TrackingNumber = trackingNumber,
                Metadata = metadata
            };

            return await this.apiClient.DoRequestAsync<Track>(ep, HttpMethod.Post, Serialize(createTrack));
        }

        #endregion
    }
}
