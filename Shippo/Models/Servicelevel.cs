using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Servicelevel
    {
        [JsonProperty(PropertyName = "token")]
        public string Token;

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "terms")]
        public string Terms;

        public override string ToString()
        {
            return string.Format("[Servicelevel: Token={0}, Name={1}]", Token, Name);
        }
    }
}
