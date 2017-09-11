using System;
using System.Runtime.Serialization;

namespace Shippo
{
    public static class ShippoEnums
    {
        //TODO: Add EnumMember to all enums so they match the API but can be changed in this solution for better readability, etc

        public enum LabelFiletypes { NONE, PNG, PDF, PDF_4x6, ZPLII }

        public enum Statuses { VALIDATING, VALID, INVALID, PURCHASING, PURCHASED }

        public enum ObjectResults { none, creation_failed, creation_succeeded, purchase_failed, purchase_succeeded }

        public enum TrackingStatus { UNKNOWN, DELIVERED, TRANSIT, FAILURE, RETURNED }

        public enum ObjectStates { VALID, INVALID }

        public enum NonDeliveryOptions { ABANDON, RETURN }

        public enum ContentsTypes { DOCUMENTS, GIFT, SAMPLE, MERCHANDISE, HUMANITARIAN_DONATION, RETURN_MERCHANDISE, OTHER }

        public enum EelPfcs { NOEEI_30_37_a, NOEEI_30_37_h, NOEEI_30_37_f, NOEEI_30_36, AES_ITN }

        public enum IncoTerms { DDP, DDU }

        public enum ShippingStatuses { WAITING, QUEUED, SUCCESS, ERROR }

        public enum ManifestStatuses { QUEUED, SUCCESS, ERROR }

        public enum SignatureConfirmations { STANDARD, ADULT, CERTIFIED, INDIRECT, CARRIER_CONFIRMATION }

        public enum ContainerTypes { Box, Tube, Pak, Envelope, CustomPackaging }

        public enum BillingTypes { SENDER, RECIPIENT, THIRD_PARTY }

        public enum CodPaymentMethods { SECURED_FUNDS, CASH, ANY }

        public enum AlcoholRecipientTypes { licensee, consumer }

        public enum InsuranceProviders { FEDEX, UPS, ONTRAC }

        public enum UspsSortTypes { NDC, FiveDigit, MixedNDC, Nonpresorted, Presorted, SCF, SinglePiece, ThreeDigit }

        public enum UspsEntryFacilities { DNDC, DDU, DSCF, ONDC, Other }

        public enum DangerousGoodsCodes
        {
            [EnumMember(Value = "01")]
            Code01,
            [EnumMember(Value = "02")]
            Code02,
            [EnumMember(Value = "03")]
            Code03,
            [EnumMember(Value = "04")]
            Code04,
            [EnumMember(Value = "05")]
            Code05,
            [EnumMember(Value = "06")]
            Code06,
            [EnumMember(Value = "07")]
            Code07,
            [EnumMember(Value = "08")]
            Code08,
            [EnumMember(Value = "09")]
            Code09
        }
    }
}
