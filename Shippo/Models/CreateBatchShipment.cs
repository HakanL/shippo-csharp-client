using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateBatchShipment
    {
        private CreateShipment shipmentObject;
        private string shipmentObjectId;

        [JsonProperty(PropertyName = "carrier_account")]
        public string CarrierAccount;

        [JsonProperty(PropertyName = "servicelevel_token")]
        public string ServicelevelToken;

        [JsonProperty(PropertyName = "shipment")]
        public object Shipment
        {
            get
            {
                if (this.shipmentObject != null)
                    return this.shipmentObject;
                else
                    return this.shipmentObjectId;
            }
            set
            {
                if (value is CreateShipment)
                {
                    this.shipmentObject = (CreateShipment)value;
                    this.shipmentObjectId = null;
                }
                else if (value is string)
                {
                    this.shipmentObject = null;
                    this.shipmentObjectId = (string)value;
                }
                else
                    throw new ArgumentException();
            }
        }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata;

        public static CreateBatchShipment CreateForBatchShipments(string carrierAccount, string servicelevelToken, CreateShipment shipment)
        {
            return new CreateBatchShipment
            {
                CarrierAccount = carrierAccount,
                ServicelevelToken = servicelevelToken,
                Shipment = shipment
            };
        }
    }
}
