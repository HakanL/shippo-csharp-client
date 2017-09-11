using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtraBilling
    {
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public BillingTypes? Type { get; set; }

        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string country { get; set; }

        [JsonProperty(PropertyName = "customer_branch", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerBranch { get; set; }

        [JsonProperty(PropertyName = "participation_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ParticipationCode { get; set; }
    }
}
