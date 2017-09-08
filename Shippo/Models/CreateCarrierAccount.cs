using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateCarrierAccount
    {
        [JsonProperty(PropertyName = "carrier")]
        public string Carrier { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}

