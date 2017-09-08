using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Address : BaseAddress
    {
        [JsonProperty(PropertyName = "object_id")]
        public string ObjectId { get; set; }

        [JsonProperty(PropertyName = "is_complete")]
        public bool IsComplete { get; set; }

        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "validate")]
        public bool? Validate;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test;

        [JsonProperty(PropertyName = "validation_results")]
        public ValidationResults ValidationResults;
    }
}
