using System;

namespace Shippo
{
    public static class ShippoEnums
    {
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
    }
}
