using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateShipment
    {
        [JsonProperty(PropertyName = "address_from")]
        public object AddressFrom { get; set; }

        [JsonProperty(PropertyName = "address_to")]
        public object AddressTo { get; set; }

        [JsonProperty(PropertyName = "parcels")]
        public object[] Parcels { get; set; }

        [JsonProperty(PropertyName = "shipment_date")]
        public DateTime? ShipmentDate { get; set; }

        [JsonProperty(PropertyName = "address_return")]
        public object AddressReturn { get; set; }

        [JsonProperty(PropertyName = "customs_declaration")]
        public object CustomsDeclaration { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public object Extra { get; set; }

        [JsonProperty(PropertyName = "async")]
        public bool? Async;

        public static CreateShipment CreateForBatch(
            BaseAddress addressFrom,
            BaseAddress addressTo,
            CreateParcel[] parcels)
        {
            return new CreateShipment
            {
                AddressFrom = addressFrom,
                AddressTo = addressTo,
                Parcels = parcels
            };
        }
    }
}
