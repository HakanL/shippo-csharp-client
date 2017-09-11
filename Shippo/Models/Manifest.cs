using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Manifest : ShippoId
    {
        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "status")]
        public ShippoEnums.ManifestStatuses Status { get; set; }

        [JsonProperty(PropertyName = "carrier_account")]
        public string CarrierAccount { get; set; }

        [JsonProperty(PropertyName = "shipment_date")]
        public DateTime ShipmentDate { get; set; }

        [JsonProperty(PropertyName = "address_from")]
        public string AddressFromObjectId { get; set; }

        [JsonProperty(PropertyName = "transactions")]
        public string[] TransactionsIds { get; set; }

        [JsonProperty(PropertyName = "documents")]
        public string[] DocumentsURLs { get; set; }
    }
}
