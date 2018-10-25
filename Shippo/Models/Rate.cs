using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Rate : ShippoId
    {
        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public string[] Attributes { get; set; }

        [JsonProperty(PropertyName = "amount_local")]
        public decimal AmountLocal { get; set; }

        [JsonProperty(PropertyName = "currency_local")]
        public string CurrencyLocal { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "provider_image_75")]
        public string ProviderImage75 { get; set; }

        [JsonProperty(PropertyName = "provider_image_200")]
        public string ProviderImage200 { get; set; }

        [JsonProperty(PropertyName = "servicelevel")]
        public RateServiceLevel Servicelevel { get; set; }

        [JsonProperty(PropertyName = "estimated_days")]
        public string EstimatedDays { get; set; }

        [JsonProperty(PropertyName = "arrives_by")]
        public string ArrivesBy { get; set; }

        [JsonProperty(PropertyName = "duration_terms")]
        public string DurationTerms { get; set; }

        [JsonProperty(PropertyName = "carrier_account")]
        public string CarrierAccount { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public Message[] Messages { get; set; }

        [JsonProperty(PropertyName = "zone")]
        public string Zone { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }
    }
}
