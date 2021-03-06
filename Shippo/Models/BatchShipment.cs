﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BatchShipment : ShippoId
    {
        private CreateShipment shipmentObject;
        private string shipmentObjectId;

        [JsonProperty(PropertyName = "status")]
        public ShippoEnums.BatchShipmentStatuses Status;

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

        [JsonProperty(PropertyName = "transaction")]
        public string TransactionObjectId;

        [JsonProperty(PropertyName = "messages")]
        public string[] Messages;

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata;

        public override string ToString()
        {
            return string.Format("[BatchShipment: Status={0}, CarrierAccount={1}, ServicelevelToken={2}, " +
                                 "Shipment={3}, Transaction={4}, Messages={5}, Metadata={6}]", Status,
                                 CarrierAccount, ServicelevelToken, Shipment, TransactionObjectId, Messages, Metadata);
        }
    }
}
