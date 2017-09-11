using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateBatch
    {
        [JsonProperty(PropertyName = "default_carrier_account")]
        public string DefaultCarrierAccount;

        [JsonProperty(PropertyName = "default_servicelevel_token")]
        public string DefaultServicelevelToken;

        [JsonProperty(PropertyName = "label_filetype")]
        public ShippoEnums.LabelFiletypes? LabelFiletype;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata;

        [JsonProperty(PropertyName = "batch_shipments")]
        public List<CreateBatchShipment> BatchShipments;

        public CreateBatch()
        {
            BatchShipments = new List<CreateBatchShipment>();
        }
    }
}
