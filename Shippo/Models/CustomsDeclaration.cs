using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CustomsDeclaration : ShippoId
    {
        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "object_state")]
        public ShippoEnums.ObjectStates ObjectState { get; set; }

        [JsonProperty(PropertyName = "exporter_reference")]
        public string ExporterReference { get; set; }

        [JsonProperty(PropertyName = "importer_reference")]
        public string ImporterReference { get; set; }

        [JsonProperty(PropertyName = "contents_type")]
        public ShippoEnums.ContentsTypes ContentsType { get; set; }

        [JsonProperty(PropertyName = "contents_explanation")]
        public string ContentsExplanation { get; set; }

        [JsonProperty(PropertyName = "invoice")]
        public string Invoice { get; set; }

        [JsonProperty(PropertyName = "license")]
        public string License { get; set; }

        [JsonProperty(PropertyName = "certificate")]
        public string Certificate { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "eel_pfc")]
        public ShippoEnums.EelPfcs EelPfc { get; set; }

        [JsonProperty(PropertyName = "aes_itn")]
        public string AesItn { get; set; }

        [JsonProperty(PropertyName = "non_delivery_option")]
        public ShippoEnums.NonDeliveryOptions NonDeliveryOption { get; set; }

        [JsonProperty(PropertyName = "certify")]
        public bool Certify { get; set; }

        [JsonProperty(PropertyName = "certify_signer")]
        public string CertifySigner { get; set; }

        [JsonProperty(PropertyName = "disclaimer")]
        public string Discliamer { get; set; }

        [JsonProperty(PropertyName = "incoterm")]
        public ShippoEnums.IncoTerms? Incoterm { get; set; }

        [JsonProperty(PropertyName = "items")]
        public string[] ItemObjectIds { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }
    }
}
