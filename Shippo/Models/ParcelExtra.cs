using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParcelExtra
    {
        [JsonProperty(PropertyName = "COD")]
        public ShipmentExtraCod Cod { get; set; }

        [JsonProperty(PropertyName = "insurance")]
        public ShipmentExtraInsurance Insurance { get; set; }
    }
}
