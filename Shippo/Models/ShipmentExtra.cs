using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static Shippo.ShippoEnums;

namespace Shippo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShipmentExtra
    {
        [JsonProperty(PropertyName = "signature_confirmation", NullValueHandling = NullValueHandling.Ignore)]
        public SignatureConfirmations? SignatureConfirmation { get; set; }

        [JsonProperty(PropertyName = "saturday_delivery", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SaturdayDelivery { get; set; }

        [JsonProperty(PropertyName = "bypass_address_validation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? BypassAddressValidation { get; set; }

        [JsonProperty(PropertyName = "request_retail_rates", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RequestRetailRates { get; set; }

        [JsonProperty(PropertyName = "customer_branch", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerBranch { get; set; }

        [JsonProperty(PropertyName = "premium", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Premium { get; set; }

        [JsonProperty(PropertyName = "preferred_delivery_timeframe", NullValueHandling = NullValueHandling.Ignore)]
        public string PreferredDeliveryTimeframe { get; set; }

        [JsonProperty(PropertyName = "lasership_attrs", NullValueHandling = NullValueHandling.Ignore)]
        public string LasershipAttributes { get; set; }

        [JsonProperty(PropertyName = "lasership_declared_value", NullValueHandling = NullValueHandling.Ignore)]
        public string LasershipDeclaredCalue { get; set; }

        [JsonProperty(PropertyName = "container_type", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerTypes? ContainerType { get; set; }

        [JsonProperty(PropertyName = "usps_sort_type", NullValueHandling = NullValueHandling.Ignore)]
        public UspsSortTypes? UspsSortType { get; set; }

        [JsonProperty(PropertyName = "usps_entry_facility", NullValueHandling = NullValueHandling.Ignore)]
        public UspsEntryFacilities? UspsEntryFacility { get; set; }

        [JsonProperty(PropertyName = "dangerous_goods_code", NullValueHandling = NullValueHandling.Ignore)]
        public DangerousGoodsCodes? DangerousGoodsCode { get; set; }

        [JsonProperty(PropertyName = "is_return", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsReturn { get; set; }

        [JsonProperty(PropertyName = "reference_1", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference1 { get; set; }

        [JsonProperty(PropertyName = "reference_2", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference2 { get; set; }

        [JsonProperty(PropertyName = "alcohol", NullValueHandling = NullValueHandling.Ignore)]
        public ShipmentExtraAlcohol Alcohol { get; set; }

        [JsonProperty(PropertyName = "billing", NullValueHandling = NullValueHandling.Ignore)]
        public ShipmentExtraBilling Billing { get; set; }

        [JsonProperty(PropertyName = "COD", NullValueHandling = NullValueHandling.Ignore)]
        public ShipmentExtraCod Cod { get; set; }

        [JsonProperty(PropertyName = "dry_ice", NullValueHandling = NullValueHandling.Ignore)]
        public ShipmentExtraDryIce DryIce { get; set; }

        [JsonProperty(PropertyName = "insurance", NullValueHandling = NullValueHandling.Ignore)]
        public ShipmentExtraInsurance Insurance { get; set; }
    }
}
