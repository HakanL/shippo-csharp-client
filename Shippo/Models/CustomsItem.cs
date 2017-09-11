﻿using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CustomsItem : ShippoId
    {
        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "object_state")]
        public ShippoEnums.ObjectStates ObjectState { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "net_weight")]
        public decimal NetWeight { get; set; }

        [JsonProperty(PropertyName = "mass_unit")]
        public MassUnits MassUnit { get; set; }

        [JsonProperty(PropertyName = "value_amount")]
        public decimal ValueAmount { get; set; }

        [JsonProperty(PropertyName = "value_currency")]
        public string ValueCurrency { get; set; }

        [JsonProperty(PropertyName = "tariff_number")]
        public string TariffNumber { get; set; }

        [JsonProperty(PropertyName = "sku_code")]
        public string SkuCode { get; set; }

        [JsonProperty(PropertyName = "origin_country")]
        public string OriginCountry { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }
    }
}

