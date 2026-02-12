using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class FedExRegistrationServiceTests : UnitTest
    {
        public FedExRegistrationServiceTests() : base("fedex_registration_service", TestUtils.ApiKey.Production)
        {
        }

        protected override IEnumerable<TestUtils.MockRequest> MockRequests
        {
            get
            {
                return new List<TestUtils.MockRequest>
                {
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/fedex_registrations\/\S*\/address$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new FedExAccountValidationResponse
                        {
                            EmailAddress = "test@example.com",
                            PhoneNumber = "5555555555",
                            Options = new List<string> { "SMS", "CALL", "INVOICE" },
                        })
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/fedex_registrations\/\S*\/pin$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new FedExRequestPinResponse
                        {
                            Message = "Your secured PIN has been sent to your phone.",
                        })
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/fedex_registrations\/\S*\/pin\/validate$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new FedExAccountValidationResponse
                        {
                            Id = "ca_test123",
                            ObjectType = "CarrierAccount",
                            Type = "FedexAccount",
                            Credentials = new Dictionary<string, string>
                            {
                                { "account_number", "123456789" },
                                { "mfa_key", "test_mfa_key" },
                            },
                        })
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/fedex_registrations\/\S*\/invoice$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new FedExAccountValidationResponse
                        {
                            Id = "ca_test123",
                            ObjectType = "CarrierAccount",
                            Type = "FedexAccount",
                            Credentials = new Dictionary<string, string>
                            {
                                { "account_number", "123456789" },
                                { "mfa_key", "test_mfa_key" },
                            },
                        })
                    ),
                };
            }
        }

        #region Tests

        [Fact]
        public async Task TestRegisterAddress()
        {
            UseMockClient();

            Parameters.FedExRegistration.RegisterAddress parameters = new Parameters.FedExRegistration.RegisterAddress
            {
                Name = "test_name",
                Company = "test_company",
                Street1 = "test_street",
                City = "test_city",
                State = "test_state",
                Zip = "test_zip",
                Country = "US",
                Phone = "test_phone",
            };

            FedExAccountValidationResponse response = await Client.FedExRegistration.RegisterAddress("123456789", parameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.Contains("SMS", response.Options);
            Assert.Contains("CALL", response.Options);
            Assert.Contains("INVOICE", response.Options);
            Assert.NotNull(response.PhoneNumber);
        }

        [Fact]
        public async Task TestRequestPin()
        {
            UseMockClient();

            FedExRequestPinResponse response = await Client.FedExRegistration.RequestPin("123456789", "SMS");

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.Contains("secured PIN", response.Message);
        }

        [Fact]
        public async Task TestValidatePin()
        {
            UseMockClient();

            Parameters.FedExRegistration.ValidatePin parameters = new Parameters.FedExRegistration.ValidatePin
            {
                Name = "test_name",
                Pin = "123456",
            };

            FedExAccountValidationResponse response = await Client.FedExRegistration.ValidatePin("123456789", parameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Credentials);
            Assert.True(response.Credentials.ContainsKey("account_number"));
            Assert.True(response.Credentials.ContainsKey("mfa_key"));
        }

        [Fact]
        public async Task TestSubmitInvoice()
        {
            UseMockClient();

            Parameters.FedExRegistration.SubmitInvoice parameters = new Parameters.FedExRegistration.SubmitInvoice
            {
                Name = "test_name",
                InvoiceNumber = "test_invoice",
                InvoiceAmount = "100.00",
                InvoiceDate = "2023-01-01",
            };

            FedExAccountValidationResponse response = await Client.FedExRegistration.SubmitInvoice("123456789", parameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Credentials);
            Assert.True(response.Credentials.ContainsKey("account_number"));
            Assert.True(response.Credentials.ContainsKey("mfa_key"));
        }

        #endregion
    }
}
