using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtraAlcohol
    {
        [JsonProperty(PropertyName = "contains_alcohol", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ContainsAlcohol { get; set; }

        [JsonProperty(PropertyName = "recipient_type", NullValueHandling = NullValueHandling.Ignore)]
        public AlcoholRecipientTypes? RecipientType { get; set; }
    }
}
