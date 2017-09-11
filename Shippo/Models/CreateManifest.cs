using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateManifest
    {
        [JsonProperty(PropertyName = "carrier_account")]
        public string CarrierAccount { get; set; }

        [JsonProperty(PropertyName = "shipment_date")]
        public DateTime ShipmentDate { get; set; }

        [JsonProperty(PropertyName = "address_from")]
        public string AddressFromObjectId { get; set; }

        [JsonProperty(PropertyName = "transactions", NullValueHandling = NullValueHandling.Ignore)]
        public string[] TransactionsIds { get; set; }

        [JsonProperty(PropertyName = "async")]
        public bool Async { get; set; }
    }
}
