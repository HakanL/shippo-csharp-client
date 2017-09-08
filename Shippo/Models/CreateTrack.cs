using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateTrack
    {
        [JsonProperty(PropertyName = "carrier")]
        public string Carrier { get; set; }

        [JsonProperty(PropertyName = "tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }
    }
}
