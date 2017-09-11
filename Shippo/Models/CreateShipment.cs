using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateShipment
    {
        private CreateAddress addressFromObject;
        private CreateAddress addressToObject;
        private CreateAddress addressReturnObject;
        private CustomsDeclaration customsDeclarationObject;
        private string addressFromObjectId;
        private string addressToObjectId;
        private string addressReturnObjectId;
        private string customsDeclarationObjectId;
        private List<object> parcels;

        [JsonProperty(PropertyName = "address_from")]
        public object AddressFrom
        {
            get
            {
                if (this.addressFromObject != null)
                    return this.addressFromObject;
                else
                    return this.addressFromObjectId;
            }
            set
            {
                if (value is CreateAddress)
                {
                    this.addressFromObject = (CreateAddress)value;
                    this.addressFromObjectId = null;
                }
                else if (value is string)
                {
                    this.addressFromObject = null;
                    this.addressFromObjectId = (string)value;
                }
                else
                    throw new ArgumentException();
            }
        }

        [JsonProperty(PropertyName = "address_to")]
        public object AddressTo
        {
            get
            {
                if (this.addressToObject != null)
                    return this.addressToObject;
                else
                    return this.addressToObjectId;
            }
            set
            {
                if (value is CreateAddress)
                {
                    this.addressToObject = (CreateAddress)value;
                    this.addressToObjectId = null;
                }
                else if (value is string)
                {
                    this.addressToObject = null;
                    this.addressToObjectId = (string)value;
                }
                else
                    throw new ArgumentException();
            }
        }

        [JsonProperty(PropertyName = "parcels")]
        public object[] Parcels
        {
            get { return this.parcels.ToArray(); }
            private set { this.parcels = new List<object>(value); }
        }

        public void AddParcel(object parcel)
        {
            if (parcel is CreateParcel || parcel is string)
                this.parcels.Add(parcel);
            else
                throw new ArgumentException("Wrong type");
        }

        [JsonProperty(PropertyName = "shipment_date")]
        public DateTime? ShipmentDate { get; set; }

        [JsonProperty(PropertyName = "address_return")]
        public object AddressReturn
        {
            get
            {
                if (this.addressReturnObject != null)
                    return this.addressReturnObject;
                else
                    return this.addressReturnObjectId;
            }
            set
            {
                if (value is CreateAddress)
                {
                    this.addressReturnObject = (CreateAddress)value;
                    this.addressReturnObjectId = null;
                }
                else if (value is string)
                {
                    this.addressReturnObject = null;
                    this.addressReturnObjectId = (string)value;
                }
                else
                    throw new ArgumentException();
            }
        }

        [JsonProperty(PropertyName = "customs_declaration")]
        public object CustomsDeclaration
        {
            get
            {
                if (this.customsDeclarationObject != null)
                    return this.customsDeclarationObject;
                else
                    return this.customsDeclarationObjectId;
            }
            set
            {
                if (value is CustomsDeclaration)
                {
                    this.customsDeclarationObject = (CustomsDeclaration)value;
                    this.customsDeclarationObjectId = null;
                }
                else if (value is string)
                {
                    this.customsDeclarationObject = null;
                    this.customsDeclarationObjectId = (string)value;
                }
                else
                    throw new ArgumentException();
            }
        }

        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public ShipmentExtra Extra { get; set; }

        [JsonProperty(PropertyName = "async")]
        public bool? Async;

        public CreateShipment()
        {
            this.parcels = new List<object>();
        }

        public static CreateShipment CreateForBatch(
            BaseAddress addressFrom,
            BaseAddress addressTo,
            CreateParcel[] parcels)
        {
            return new CreateShipment
            {
                AddressFrom = addressFrom,
                AddressTo = addressTo,
                Parcels = parcels
            };
        }
    }
}
