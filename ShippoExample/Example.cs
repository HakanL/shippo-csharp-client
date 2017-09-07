/*
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shippo;
using Shippo.Models;

namespace ShippoExample
{
    class Example
    {

        static readonly string TRACKING_NO = "9205590164917312751089";

        private static async Task RunBatchExample(ShippoClient resource)
        {
            ShippoCollection<CarrierAccount> carrierAccounts = await resource.AllCarrierAccount();
            string defaultCarrierAccount = "";
            foreach (CarrierAccount account in carrierAccounts)
            {
                if (account.Carrier.ToString() == "usps")
                    defaultCarrierAccount = account.ObjectId;
            }

            Address addressFrom = Address.CreateForPurchase("Mr. Hippo", "965 Mission St.", "Ste 201", "SF",
                                                            "CA", "94103", "US", "4151234567", "ship@gmail.com");
            Address addressTo = Address.CreateForPurchase("Mrs. Hippo", "965 Missions St.", "Ste 202", "SF",
                                                          "CA", "94103", "US", "4151234568", "msship@gmail.com");
            Parcel[] parcels = { Parcel.createForShipment(5, 5, 5, "in", 2, "oz") };
            Shipment shipment = Shipment.createForBatch(addressFrom, addressTo, parcels);
            BatchShipment batchShipment = BatchShipment.createForBatchShipments(defaultCarrierAccount, "usps_priority", shipment);

            List<BatchShipment> batchShipments = new List<BatchShipment>();
            batchShipments.Add(batchShipment);

            Batch batch = await resource.CreateBatch(defaultCarrierAccount, "usps_priority", ShippoEnums.LabelFiletypes.PDF_4x6,
                                               "BATCH #170", batchShipments);
            Console.WriteLine("Batch Status = " + batch.Status);
            Console.WriteLine("Metadata = " + batch.Metadata);
        }

        private static async Task RunTrackingExample(ShippoClient resource)
        {
            Track track = await resource.RetrieveTracking("usps", TRACKING_NO);
            Console.WriteLine("Carrier = " + track.Carrier.ToUpper());
            Console.WriteLine("Tracking number = " + track.TrackingNumber);
        }

        private static async Task RunInternationalAddressValidationExample(ShippoClient resource)
        {
            var parameters = new CreateAddress
            {
                Name = "Shippo Hippo",
                Company = "Shippo",
                StreetNo = null,
                Street1 = "40 Bay St",
                Street2 = null,
                City = "Toronto",
                State = "ON",
                Zip = "M5J 2X2",
                Country = "CA",
                Phone = "+1 555 341 9393",
                Email = "hippo@goshippo.com",
                Metadata = "Customer ID 123456",
                Validate = true
            };
            Address address = await resource.CreateAddress(parameters);
            Console.Out.WriteLine("Address IsValid: " + address.ValidationResults.IsValid);
            if (address.ValidationResults.Messages != null)
            {
                foreach (ValidationMessage message in address.ValidationResults.Messages)
                {
                    Console.Out.WriteLine("Address Message Code: " + message.Code);
                    Console.Out.WriteLine("Address Message Text: " + message.Text);
                    Console.Out.WriteLine();
                }
            }
        }

        public static void Main(string[] args)
        {
            // replace with your Shippo Token
            // don't have one? get more info here
            // (https://goshippo.com/docs/#overview)
            ShippoClient resource = new ShippoClient("<YourShippoToken>");

            // to address
            var toAddressTable = new Dictionary<string, object>();
            toAddressTable.Add("name", "Mr. Hippo");
            toAddressTable.Add("company", "Shippo");
            toAddressTable.Add("street1", "215 Clayton St.");
            toAddressTable.Add("city", "San Francisco");
            toAddressTable.Add("state", "CA");
            toAddressTable.Add("zip", "94117");
            toAddressTable.Add("country", "US");
            toAddressTable.Add("phone", "+1 555 341 9393");
            toAddressTable.Add("email", "support@goshipppo.com");

            // from address
            var fromAddressTable = new Dictionary<string, object>();
            fromAddressTable.Add("name", "Ms Hippo");
            fromAddressTable.Add("company", "San Diego Zoo");
            fromAddressTable.Add("street1", "2920 Zoo Drive");
            fromAddressTable.Add("city", "San Diego");
            fromAddressTable.Add("state", "CA");
            fromAddressTable.Add("zip", "92101");
            fromAddressTable.Add("country", "US");
            fromAddressTable.Add("email", "hippo@goshipppo.com");
            fromAddressTable.Add("phone", "+1 619 231 1515");
            fromAddressTable.Add("metadata", "Customer ID 123456");

            // parcel
            var parcelTable = new Dictionary<string, object>();
            parcelTable.Add("length", "5");
            parcelTable.Add("width", "5");
            parcelTable.Add("height", "5");
            parcelTable.Add("distance_unit", "in");
            parcelTable.Add("weight", "2");
            parcelTable.Add("mass_unit", "lb");
            var parcels = new List<Dictionary<string, object>>();
            parcels.Add(parcelTable);


            // shipment
            var shipmentTable = new Dictionary<string, object>();
            shipmentTable.Add("address_to", toAddressTable);
            shipmentTable.Add("address_from", fromAddressTable);
            shipmentTable.Add("parcels", parcels);
            shipmentTable.Add("object_purpose", "PURCHASE");
            shipmentTable.Add("async", false);

            // create Shipment object
            Console.WriteLine("Creating Shipment object..");
            Shipment shipment = resource.CreateShipment(shipmentTable).Result;

            // select desired shipping rate according to your business logic
            // we simply select the first rate in this example
            Rate rate = shipment.Rates[0];

            Console.WriteLine("Getting shipping label..");
            var transactionParameters = new Dictionary<string, object>();
            transactionParameters.Add("rate", rate.ObjectId);
            transactionParameters.Add("async", false);
            Transaction transaction = resource.CreateTransaction(transactionParameters).Result;

            if (((String)transaction.Status).Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Label url : " + transaction.LabelURL);
                Console.WriteLine("Tracking number : " + transaction.TrackingNumber);
            }
            else
            {
                Console.WriteLine("An Error has occured while generating your label. Messages : " + transaction.Messages);
            }

            Console.WriteLine("\nBatch\n");
            Task.Run(async () => await RunBatchExample(resource)).Wait();

            Console.WriteLine("\nTrack\n");
            Task.Run(async () => await RunTrackingExample(resource)).Wait();

            Console.WriteLine("\nValidating International Address\n");
            Task.Run(async () => await RunInternationalAddressValidationExample(resource)).Wait();
        }
    }
}
