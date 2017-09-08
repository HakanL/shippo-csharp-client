using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BatchShipment : ShippoId
    {
        [JsonProperty(PropertyName = "status")]
        public string Status;

        [JsonProperty(PropertyName = "carrier_account")]
        public string CarrierAccount;

        [JsonProperty(PropertyName = "servicelevel_token")]
        public string ServicelevelToken;

        [JsonProperty(PropertyName = "shipment")]
        public Object Shipment;

        [JsonProperty(PropertyName = "transaction")]
        public string Transaction;

        [JsonProperty(PropertyName = "messages")]
        public object Messages;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata;

        public static BatchShipment CreateForBatchShipments(String carrierAccount, string servicelevelToken, CreateShipment shipment)
        {
            return new BatchShipment
            {
                CarrierAccount = carrierAccount,
                ServicelevelToken = servicelevelToken,
                Shipment = shipment
            };
        }

        public override string ToString()
        {
            return string.Format("[BatchShipment: Status={0}, CarrierAccount={1}, ServicelevelToken={2}, " +
                                 "Shipment={3}, Transaction={4}, Messages={5}, Metadata={6}]", Status,
                                 CarrierAccount, ServicelevelToken, Shipment, Transaction, Messages, Metadata);
        }
    }
}
