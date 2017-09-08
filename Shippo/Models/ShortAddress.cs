using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShortAddress
    {
        [JsonProperty(PropertyName = "city")]
        public string City;

        [JsonProperty(PropertyName = "state")]
        public string State;

        [JsonProperty(PropertyName = "Zip")]
        public string PostalCode;

        [JsonProperty(PropertyName = "Country")]
        public string Country;
    }
}
