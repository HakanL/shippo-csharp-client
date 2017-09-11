using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtraDryIce
    {
        [JsonProperty(PropertyName = "contains_dry_ice", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ContainsDryIce { get; set; }

        [JsonProperty(PropertyName = "weight", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? WeightKg { get; set; }
    }
}
