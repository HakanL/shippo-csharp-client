using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Address : ShippoId
    {
        [JsonProperty(PropertyName = "is_complete")]
        public bool IsComplete { get; set; }

        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "street1")]
        public string Street1 { get; set; }

        [JsonProperty(PropertyName = "street_no")]
        public string StreetNo { get; set; }

        [JsonProperty(PropertyName = "street2")]
        public string Street2 { get; set; }

        [JsonProperty(PropertyName = "street3")]
        public string Street3;

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "is_residential")]
        public bool? IsResidential { get; set; }

        [JsonProperty(PropertyName = "validate")]
        public bool? Validate;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test;

        [JsonProperty(PropertyName = "validation_results")]
        public ValidationResults ValidationResults;

        public static Address CreateForPurchase(string name, string street1,
                                                string street2, string city, string state, string zip,
                                                string country, string phone, string email)
        {
            return new Address
            {
                Name = name,
                Street1 = street1,
                Street2 = street2,
                City = city,
                State = state,
                Zip = zip,
                Country = country,
                Phone = phone,
                Email = email,
            };
        }
    }
}
