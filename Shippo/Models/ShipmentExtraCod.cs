using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtraCod
    {
        [JsonProperty(PropertyName = "payment_method", NullValueHandling = NullValueHandling.Ignore)]
        public CodPaymentMethods? PaymentMethod { get; set; }

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
    }
}
