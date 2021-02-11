using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Substatus
    {
        [JsonProperty(PropertyName = "code")]
        public string Code;

        [JsonProperty(PropertyName = "text")]
        public string Test;

        [JsonProperty(PropertyName = "action_required")]
        public bool ActionRequired;
    }
}
