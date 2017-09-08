using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateParcel
    {
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
        public string Extra;

        public static CreateParcel CreateForShipment(
            decimal length,
            decimal width,
            decimal height,
            DistanceUnits distance_unit,
            decimal weight,
            MassUnits massUnit)
        {
            return new CreateParcel
            {
                Length = length,
                Width = width,
                Height = height,
                DistanceUnit = distance_unit,
                Weight = weight,
                MassUnit = massUnit
            };
        }
    }
}
