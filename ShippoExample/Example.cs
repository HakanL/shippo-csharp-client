﻿/*
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
            ShippoCollection<CarrierAccount> carrierAccounts = await resource.AllCarrierAccounts();
            string defaultCarrierAccount = "";
            foreach (CarrierAccount account in carrierAccounts)
            {
                if (account.Carrier.ToString() == "usps")
                    defaultCarrierAccount = account.ObjectId;
            }

            var addressFrom = CreateAddress.CreateForPurchase("Mr. Hippo", "965 Mission St.", "Ste 201", "SF",
                                                            "CA", "94103", "US", "4151234567", "ship@gmail.com");
            var addressTo = CreateAddress.CreateForPurchase("Mrs. Hippo", "965 Missions St.", "Ste 202", "SF",
                                                          "CA", "94103", "US", "4151234568", "msship@gmail.com");
            CreateParcel[] parcels = { CreateParcel.CreateForShipment(5, 5, 5, DistanceUnits.@in, 2, MassUnits.oz) };
            var shipment = CreateShipment.CreateForBatch(addressFrom, addressTo, parcels);
            var batchShipment = BatchShipment.CreateForBatchShipments(defaultCarrierAccount, "usps_priority", shipment);

            var batchShipments = new List<BatchShipment>();
            batchShipments.Add(batchShipment);

            Batch batch = await resource.CreateBatch(
                defaultCarrierAccount,
                "usps_priority",
                ShippoEnums.LabelFiletypes.PDF_4x6,
                "BATCH #170",
                batchShipments);

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
                PostalCode = "M5J 2X2",
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
            var client = new ShippoClient("<YourShippoToken>");

            // to address
            var toAddressTable = new CreateAddress
            {
                Name = "Mr. Hippo",
                Company = "Shippo",
                Street1 = "215 Clayton St.",
                City = "San Francisco",
                State = "CA",
                PostalCode = "94117",
                Country = "US",
                Phone = "+1 555 341 9393",
                Email = "support@goshipppo.com"
            };

            // from address
            var fromAddressTable = new CreateAddress
            {
                Name = "Ms Hippo",
                Company = "San Diego Zoo",
                Street1 = "2920 Zoo Drive",
                City = "San Diego",
                State = "CA",
                PostalCode = "92101",
                Country = "US",
                Email = "hippo@goshipppo.com",
                Phone = "+1 619 231 1515",
                Metadata = "Customer ID 123456"
            };

            // parcel
            var parcelTable = new CreateParcel
            {
                Length = 5,
                Width = 5,
                Height = 5,
                DistanceUnit = DistanceUnits.@in,
                Weight = 2,
                MassUnit = MassUnits.lb
            };

            // shipment
            var shipmentTable = new CreateShipment
            {
                AddressTo = toAddressTable,
                AddressFrom = fromAddressTable,
                Async = false
            };
            shipmentTable.AddParcel(parcelTable);

            // create Shipment object
            Console.WriteLine("Creating Shipment object..");
            Task.Run(async () =>
            {
                Shipment shipment = await client.CreateShipment(shipmentTable);

                // select desired shipping rate according to your business logic
                // we simply select the first rate in this example
                Rate rate = shipment.Rates[0];

                Console.WriteLine("Getting shipping label..");
                var transactionParameters = new Dictionary<string, object>();
                transactionParameters.Add("rate", rate.ObjectId);
                transactionParameters.Add("async", false);
                Transaction transaction = await client.CreateTransaction(transactionParameters);

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
                await RunBatchExample(client);

                Console.WriteLine("\nTrack\n");
                await RunTrackingExample(client);

                Console.WriteLine("\nValidating International Address\n");
                await RunInternationalAddressValidationExample(client);

            }).Wait();
        }
    }
}
