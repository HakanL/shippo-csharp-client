using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateCarrierAccount
    {
        [JsonProperty(PropertyName = "parameters", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Parameters { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }
    }
}
