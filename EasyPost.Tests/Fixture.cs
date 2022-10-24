using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasyPost.Utilities;

namespace EasyPost.Tests._Utilities
{
    public class Fixtures
    {
        public static byte[] EventBody
        {
            get
            {
                // need to go up levels to get out of the EasyPost.Tests directory
                // this path will get messed up on CI vs local if you change it and/or this Fixture.cs file location
                const string relativePath = "../examples/official/fixtures/event-body.json";
                string fullPath = Path.Combine(TestUtils.GetSourceFileDirectory(), relativePath);

                try
                {
                    string jsonString = File.ReadLines(fullPath).First();
                    return Encoding.UTF8.GetBytes(jsonString);
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to read {fullPath}", e);
                }
            }
        }

        internal static Dictionary<string, object> BasicCarrierAccount => GetFixtureStructure().CarrierAccounts.Basic;

        internal static Dictionary<string, object> BasicCustomsInfo => GetFixtureStructure().CustomsInfos.Basic;

        internal static Dictionary<string, object> BasicCustomsItem => GetFixtureStructure().CustomsItems.Basic;

        internal static Dictionary<string, object> BasicInsurance => GetFixtureStructure().Insurances.Basic;

        internal static Dictionary<string, object> BasicOrder => GetFixtureStructure().Orders.Basic;

        internal static Dictionary<string, object> BasicParcel => GetFixtureStructure().Parcels.Basic;

        internal static Dictionary<string, object> BasicPickup
        {
            get
            {
                Dictionary<string, object> fixture = GetFixtureStructure().Pickups.Basic;

                const string pickupDate = "2022-10-25";

                fixture.AddOrUpdate("min_datetime", pickupDate);
                fixture.AddOrUpdate("max_datetime", pickupDate);

                return fixture;
            }
        }

        internal static Dictionary<string, object> BasicShipment => GetFixtureStructure().Shipments.BasicDomestic;

        internal static Dictionary<string, object> CaAddress1 => GetFixtureStructure().Addresses.CaAddress1;

        internal static Dictionary<string, object> CaAddress2 => GetFixtureStructure().Addresses.CaAddress2;

        internal static Dictionary<string, object> CreditCardDetails => GetFixtureStructure().CreditCards.Test;

        internal static Dictionary<string, object> FullShipment => GetFixtureStructure().Shipments.Full;

        internal static Dictionary<string, object> IncorrectAddress => GetFixtureStructure().Addresses.IncorrectAddress;

        internal static Dictionary<string, object> OneCallBuyShipment => new()
        {
            { "to_address", CaAddress1 },
            { "from_address", CaAddress2 },
            { "parcel", BasicParcel },
            { "service", UspsService },
            { "carrier_accounts", new List<string> { UspsCarrierAccountId } },
            { "carrier", Usps }
        };

        internal static int PageSize => GetFixtureStructure().PageSizes.Five;

        internal static string PickupService => GetFixtureStructure().ServiceNames.Usps.PickupService;

        internal static Dictionary<string, object> ReferralUser => GetFixtureStructure().Users.Referral;

        internal static string ReportDate => "2022-04-12";

        internal static string ReportIdPrefix => "shprep_";

        internal static string ReportType => GetFixtureStructure().ReportTypes.Shipment;

        internal static Dictionary<string, object> RmaFormOptions => GetFixtureStructure().FormOptions.Rma;

        internal static Dictionary<string, object> TaxIdentifier => GetFixtureStructure().TaxIdentifiers.Basic;

        internal static string Usps => GetFixtureStructure().CarrierStrings.Usps;

        // This is the USPS carrier account ID that comes with your EasyPost account by default and should be used for all tests
        internal static string UspsCarrierAccountId
        {
            get
            {
                string? envVar = Environment.GetEnvironmentVariable("USPS_CARRIER_ACCOUNT_ID");
                // Fallback to the EasyPost C# Client Library Test User USPS carrier account
                return envVar ?? "ca_7642d249fdcf47bcb5da9ea34c96dfcf";
            }
        }

        internal static string UspsService => GetFixtureStructure().ServiceNames.Usps.FirstService;

        internal static string WebhookUrl => GetFixtureStructure().WebhookUrl;


        private static FixtureStructure GetFixtureStructure()
        {
            string fixtureData = ReadFixtureData();
            return JsonSerialization.ConvertJsonToObject<FixtureStructure>(fixtureData);
        }

        private static string ReadFixtureData()
        {
            // need to go up levels to get out of the EasyPost.Tests directory
            // this path will get messed up on CI vs local if you change it and/or this Fixture.cs file location
            const string path = "../examples/official/fixtures/client-library-fixtures.json";
            return TestUtils.ReadFile(path);
        }
    }

    internal static class DictionaryExtensions
    {
        internal static void AddOrUpdate(this IDictionary<string, object> dictionary, string key, object value)
        {
            try
            {
                dictionary.Add(key, value);
            }
            catch (ArgumentException)
            {
                dictionary[key] = value;
            }
        }
    }
}
