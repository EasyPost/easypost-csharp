using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class InsuranceTest
    {
        private Dictionary<string, object> FromAddressData { get; set; }
        private Dictionary<string, object> ParcelData { get; set; }
        private Dictionary<string, object> ToAddressData { get; set; }
        private Dictionary<string, object> InsuranceData { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
            FromAddressData = new Dictionary<string, object>
            {
                {
                    "company", "Simpler Postage Inc"
                },
                {
                    "street1", "164 Townsend St"
                },
                {
                    "street2", "Unit 1"
                },
                {
                    "city", "San Francisco"
                },
                {
                    "state", "CA"
                },
                {
                    "zip", "94107"
                },
                {
                    "country", "US"
                }
            };
            ToAddressData = new Dictionary<string, object>()
            {
                {
                    "company", "The White House"
                },
                {
                    "street1", "1600 Pennsylvania Avenue NW"
                },
                {
                    "street2", "DC 20500"
                },
                {
                    "city", "Washington"
                },
                {
                    "state", "DC"
                },
                {
                    "zip", "20500"
                },
                {
                    "country", "US"
                }
            };
            ParcelData = new Dictionary<string, object>
            {
                {
                    "weight", 8.0
                }
            };

            Shipment shipment = Shipment.Create(new Dictionary<string, object>
            {
                {
                    "from_address", FromAddressData
                },
                {
                    "to_address", ToAddressData
                },
                {
                    "parcel", ParcelData
                },
                {
                    "service", "First"
                },
                {
                    "carrier_accounts", new List<string>
                    {
                        {
                            "ca_7642d249fdcf47bcb5da9ea34c96dfcf"
                        }
                    }
                }
            });

            InsuranceData = new Dictionary<string, object>
            {
                {
                    "to_address", ToAddressData
                },
                {
                    "from_address", FromAddressData
                },
                {
                    "tracking_code", shipment.tracking_code
                },
                {
                    "amount", "100.00"
                },
                {
                    "carrier", "USPS"
                }
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            Insurance insurance = Insurance.Create(InsuranceData);
            Assert.IsNotNull(insurance.id);
            Assert.AreEqual("SAN FRANCISCO", insurance.from_address.city);

            Insurance retrieved = Insurance.Retrieve(insurance.id);
            Assert.AreEqual(insurance.id, retrieved.id);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            InsuranceCollection insuranceCollection = Insurance.All();
            Assert.IsNotNull(insuranceCollection);
            foreach (var insurance in insuranceCollection.insurances)
            {
                Assert.IsNotNull(insurance.id);
                Assert.AreEqual(insurance.id.Substring(0, 4), "ins_");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestRetrieveInvalidId() => Insurance.Retrieve("not-an-id");
    }
}
