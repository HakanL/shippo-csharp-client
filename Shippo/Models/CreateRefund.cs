using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateRefund
    {
        [JsonProperty(PropertyName = "transaction")]
        public string Transaction { get; set; }

        [JsonProperty(PropertyName = "async")]
        public bool Async { get; set; }
    }
}

