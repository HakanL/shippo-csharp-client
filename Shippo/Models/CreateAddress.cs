using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateAddress
    {
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
    }
}
