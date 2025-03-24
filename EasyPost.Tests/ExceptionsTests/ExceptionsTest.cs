using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ExceptionsTests
{
    public class ExceptionsTests : UnitTest
    {
        public ExceptionsTests() : base("exceptions")
        {
        }

        #region Tests

        [Fact]
        [Testing.Exception]
        public async Task TestError()
        {
            UseVCR("error");

            try
            {
                await Client.Shipment.Create(new Dictionary<string, object>());
            }
            catch (InvalidRequestError exception)
            {
                Assert.Equal(422, exception.StatusCode);
                Assert.Equal("PARAMETER.REQUIRED", exception.Code);
                Assert.Equal("Missing required parameter.", exception.Message);

                FieldError fieldError = (FieldError)exception.Errors.First();
                Assert.Equal("cannot be blank", fieldError.Message);
                Assert.Equal("shipment", fieldError.Field);
            }
        }

        [Fact]
        [Testing.Exception]
        public async Task TestErrorAlternativeFormat()
        {
            UseVCR("error_alternative_format");

            Dictionary<string, object> claimData = Fixtures.BasicClaim;
            Parameters.Claim.Create claimParameters = Fixtures.Parameters.Claims.Create(claimData);
            claimParameters.TrackingCode = "123"; // Intentionally pass a bad tracking code

            try
            {
                await Client.Claim.Create(claimParameters);
            }
            catch (NotFoundError exception)
            {
                Assert.Equal(404, exception.StatusCode);
                Assert.Equal("NOT_FOUND", exception.Code);
                Assert.Equal("The requested resource could not be found.", exception.Message);
                Assert.Equal("No eligible insurance found with provided tracking code.", exception.Errors.First());
            }
        }

        [Fact]
        [Testing.Exception]
        public async Task TestKnownApiExceptionGeneration()
        {
            // all the error status codes the library should be able to handle
            Dictionary<int, Type> exceptionsMap = new()
            {
                { 100, typeof(UnknownHttpError) },
                { 300, typeof(RedirectError) },
                { 301, typeof(RedirectError) },
                { 302, typeof(RedirectError) },
                { 303, typeof(RedirectError) },
                { 304, typeof(RedirectError) },
                { 305, typeof(RedirectError) },
                { 306, typeof(RedirectError) },
                { 307, typeof(RedirectError) },
                { 308, typeof(RedirectError) },
                { 400, typeof(BadRequestError) },
                { 401, typeof(UnauthorizedError) },
                { 402, typeof(PaymentError) },
                { 403, typeof(UnauthorizedError) },
                { 404, typeof(NotFoundError) },
                { 405, typeof(MethodNotAllowedError) },
                { 408, typeof(TimeoutError) },
                { 422, typeof(InvalidRequestError) },
                { 429, typeof(RateLimitError) },
                { 500, typeof(InternalServerError) },
                { 503, typeof(ServiceUnavailableError) },
                { 504, typeof(GatewayTimeoutError) }
            };

            foreach (KeyValuePair<int, Type> exceptionDetails in exceptionsMap)
            {
                int statusCodeInt = exceptionDetails.Key;
                Type exceptionType = exceptionDetails.Value;

                // Generate a dummy HttpResponseMessage with the given status code to parse
                HttpStatusCode statusCode = Enum.Parse<HttpStatusCode>(statusCodeInt.ToString(CultureInfo.InvariantCulture));
                HttpResponseMessage response = new() { StatusCode = statusCode };

                ApiError generatedError = await ApiError.FromErrorResponse(response);

                // should be of base type ApiError
                Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
                // should be specifically of the type we expect
                Assert.Equal(exceptionType, generatedError.GetType());
                // should have the correct status code
                Assert.Equal(statusCodeInt, generatedError.StatusCode);
                // should have a message
                Assert.NotNull(generatedError.Message);
                // because we're not giving it a real API response to parse, there's no errors to extract, so the errors list should be null
                // this inherently tests that the property exists as well
                Assert.Null(generatedError.Errors);
                // should have a code
                Assert.NotNull(generatedError.Code);
                // because we're not giving it a real API response to parse, there's no code to extract, so the code should be the default
                Assert.Equal("RESPONSE.PARSE_ERROR", generatedError.Code);
            }
        }

        [Fact]
        [Testing.Exception]
        public async Task TestExceptionErrorMessageParsing()
        {
            const string errorMessageStringJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": \"ERROR_MESSAGE_1\", \"errors\": []}}";
            HttpResponseMessage response = new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageStringJson),
            };

            ApiError exception = await ApiError.FromErrorResponse(response);

            Assert.Equal("ERROR_MESSAGE_1", exception.Message);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestExceptionErrorArrayParsing()
        {
            const string errorMessageArrayJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": [\"ERROR_MESSAGE_1\", \"ERROR_MESSAGE_2\"], \"errors\": []}}";
            HttpResponseMessage response = new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageArrayJson),
            };

            ApiError exception = await ApiError.FromErrorResponse(response);

            Assert.Equal("ERROR_MESSAGE_1, ERROR_MESSAGE_2", exception.Message);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestExceptionErrorObjectParsing()
        {
            const string errorMessageObjectJson = "{\"error\": {\"code\": \"UNPROCESSABLE_ENTITY\", \"message\": {\"errors\": [\"bad error.\", \"second bad error.\"]}, \"errors\": []}}";
            HttpResponseMessage response = new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageObjectJson),
            };

            ApiError exception = await ApiError.FromErrorResponse(response);

            Assert.Equal("bad error., second bad error.", exception.Message);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestExceptionErrorEdgeCaseParsing()
        {
            const string json = "{\"error\": {\"code\": \"UNPROCESSABLE_ENTITY\", \"message\": {\"errors\": [\"Bad format 1\", \"Bad format 2\"], \"bad_data\": [{\"first_message\": \"Bad format 3\", \"second_message\": \"Bad format 4\", \"third_message\": \"Bad format 5\"}]}, \"errors\": []}}";
            HttpResponseMessage response = new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(json),
            };

            ApiError exception = await ApiError.FromErrorResponse(response);

            Assert.Equal("Bad format 1, Bad format 2, Bad format 3, Bad format 4, Bad format 5", exception.Message);
        }

        #endregion
    }
}
