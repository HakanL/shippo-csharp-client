using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BatchShipments
    {
        [JsonProperty(PropertyName = "count")]
        public int Count;

        [JsonProperty(PropertyName = "next")]
        public string Next;

        [JsonProperty(PropertyName = "previous")]
        public string Previous;

        [JsonProperty(PropertyName = "results")]
        public BatchShipment[] Results;

        public override string ToString()
        {
            return string.Format("[BatchShipments: Count={0}, Next={1}, Previous={2}, Results={3}]",
                                 Count, Next, Previous, Results);
        }
    }
}
