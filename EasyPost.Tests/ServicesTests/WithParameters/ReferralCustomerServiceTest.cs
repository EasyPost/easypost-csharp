using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.ReferralCustomer;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class ReferralCustomerServiceTests : UnitTest
    {
        public ReferralCustomerServiceTests() : base("referral_customer_service_with_parameters", TestUtils.ApiKey.Partner)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateReferral()
        {
            UseVCR("create_referral");

            Dictionary<string, object> data = Fixtures.ReferralCustomer;

            CreateReferralCustomer parameters = Fixtures.Parameters.ReferralCustomers.CreateReferralCustomer(data);

            ReferralCustomer referralCustomer = await Client.ReferralCustomer.CreateReferral(parameters);

            Assert.NotNull(referralCustomer);
            Assert.IsType<ReferralCustomer>(referralCustomer);
            Assert.StartsWith("user_", referralCustomer.Id);
            Assert.Equal("Test Referral", referralCustomer.Name);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            All parameters = Fixtures.Parameters.ReferralCustomers.All(data);

            ReferralCustomerCollection referralCustomerCollection = await Client.ReferralCustomer.All(parameters);
            List<ReferralCustomer> referralCustomers = referralCustomerCollection.ReferralCustomers;

            Assert.True(referralCustomers.Count <= Fixtures.PageSize);
            foreach (ReferralCustomer item in referralCustomers)
            {
                Assert.IsType<ReferralCustomer>(item);
            }
        }

        #endregion

        #endregion

        private static string ReferralCustomerKey => TestUtils.GetApiKey(TestUtils.ApiKey.Referral);

        private sealed class CreditCard
        {
            internal readonly string Number;

            internal readonly int ExpirationMonth;

            internal readonly int ExpirationYear;

            internal readonly string Cvc;

            internal CreditCard(IReadOnlyDictionary<string, object> details)
            {
                Number = (string)details["number"];
                ExpirationMonth = int.Parse((string)details["expiration_month"], NumberStyles.Number, CultureInfo.InvariantCulture);
                ExpirationYear = int.Parse((string)details["expiration_year"], NumberStyles.Number, CultureInfo.InvariantCulture);
                Cvc = (string)details["cvc"];
            }
        }
    }
}
