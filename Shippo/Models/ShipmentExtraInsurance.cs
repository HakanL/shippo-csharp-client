using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtraInsurance
    {
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "provider", NullValueHandling = NullValueHandling.Ignore)]
        public InsuranceProviders? Provider { get; set; }
    }
}
