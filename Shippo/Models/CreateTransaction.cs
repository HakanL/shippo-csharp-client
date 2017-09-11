using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateTransaction
    {
        [JsonProperty(PropertyName = "rate")]
        public string Rate { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "label_file_type")]
        public ShippoEnums.LabelFiletypes LabelFileType { get; set; }

        [JsonProperty(PropertyName = "async")]
        public bool Async { get; set; }
    }
}
