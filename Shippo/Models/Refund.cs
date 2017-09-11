using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Refund : ShippoId
    {
        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "status")]
        public ShippoEnums.RefundStatuses Status { get; set; }

        [JsonProperty(PropertyName = "transaction")]
        public string Transaction { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }
    }
}

