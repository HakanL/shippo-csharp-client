using System;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Transaction : ShippoId
    {
        [JsonProperty(PropertyName = "object_state")]
        public ShippoEnums.ObjectStates ObjectState { get; set; }

        [JsonProperty(PropertyName = "status")]
        public ShippoEnums.TransactionStatuses Status { get; set; }

        [JsonProperty(PropertyName = "object_created")]
        public DateTime ObjectCreated { get; set; }

        [JsonProperty(PropertyName = "object_updated")]
        public DateTime ObjectUpdated { get; set; }

        [JsonProperty(PropertyName = "object_owner")]
        public string ObjectOwner { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public string Rate { get; set; }

        [JsonProperty(PropertyName = "tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty(PropertyName = "tracking_status")]
        public ShippoEnums.TrackingStatuses TrackingStatus { get; set; }

        [JsonProperty(PropertyName = "tracking_url_provider")]
        public string TrackingUrlProvider { get; set; }

        [JsonProperty(PropertyName = "eta")]
        public DateTime? Eta { get; set; }

        [JsonProperty(PropertyName = "label_file_type")]
        public ShippoEnums.LabelFiletypes LabelFileType { get; set; }

        [JsonProperty(PropertyName = "label_url")]
        public string LabelURL { get; set; }

        [JsonProperty(PropertyName = "commercial_invoice_url")]
        public string CommercialInvoiceUrl { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public Message[] Messages { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }
    }
}
