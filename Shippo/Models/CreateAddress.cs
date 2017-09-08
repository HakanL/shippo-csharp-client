using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateAddress : BaseAddress
    {
        [JsonProperty(PropertyName = "validate")]
        public bool? Validate;

        public static CreateAddress CreateForPurchase(
            string name,
            string street1,
            string street2,
            string city,
            string state,
            string postalCode,
            string country,
            string phone,
            string email)
        {
            return new CreateAddress
            {
                Name = name,
                Street1 = street1,
                Street2 = street2,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country,
                Phone = phone,
                Email = email
            };
        }
    }
}
