using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class InsuranceTest
    {
        private Insurance _insurance { get; set; }
        private Dictionary<string, object> InsuranceData { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
            Address fromAddress = Address.Create(new Dictionary<string, object>
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
            });
            Address toAddress = Address.Create(new Dictionary<string, object>()
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
            });
            Tracker tracker = Tracker.Create(trackingCode: "EZ1000000001", carrier: "USPS");

            InsuranceData = new Dictionary<string, object>
            {
                {
                    "to_address", toAddress
                },
                {
                    "from_address", fromAddress
                },
                {
                    "tracker", tracker
                },
                {
                    "amount", (float)100
                },
                {
                    "currency", "USD"
                }
            };
            _insurance = new Insurance
            {
                from_address = toAddress,
                to_address = fromAddress,
                tracker = tracker,
                amount = (float)100.00,
                provider = "USPS"
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            Insurance insurance = Insurance.Create(InsuranceData);
            Assert.IsNotNull(insurance.id);
            Assert.AreEqual(insurance.from_address, _insurance.from_address);

            Insurance retrieved = Insurance.Retrieve(insurance.id);
            Assert.AreEqual(insurance.id, retrieved.id);
        }

        [TestMethod]
        public void TestCreateInstance()
        {
            _insurance.Create();
            Assert.IsNotNull(_insurance.id);
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
