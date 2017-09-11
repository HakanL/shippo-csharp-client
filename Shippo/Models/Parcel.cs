using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Parcel : ShippoId
    {
        [JsonProperty(PropertyName = "object_state")]
        public ShippoEnums.ObjectStates ObjectState { get; set; }

        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "length")]
        public decimal Length { get; set; }

        [JsonProperty(PropertyName = "width")]
        public decimal Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public decimal Height { get; set; }

        [JsonProperty(PropertyName = "distance_unit")]
        public DistanceUnits DistanceUnit { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public decimal Weight { get; set; }

        [JsonProperty(PropertyName = "mass_unit")]
        public MassUnits MassUnit { get; set; }

        [JsonProperty(PropertyName = "template")]
        public string Template;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public ParcelExtra Extra;

        [JsonProperty(PropertyName = "test")]
        public bool Test;
    }
}
