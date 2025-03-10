using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ExceptionsTests
{
#pragma warning disable CA2263
    public class ExceptionsTests : UnitTest
    {
        public ExceptionsTests() : base("exceptions")
        {
        }

        #region Tests

        [Fact]
        [Testing.Exception]
        public async Task TestApiExceptionPrettyPrint()
        {
            const int statusCode = 401;

            // Generate a dummy HttpResponseMessage with the given status code to parse
            HttpStatusCode httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString(CultureInfo.InvariantCulture));

            HttpResponseMessage response = new() { StatusCode = httpStatusCode };

            ApiError generatedError = await ApiError.FromErrorResponse(response);

            // we didn't load error-related JSON data into the response for parsing, so the pretty print is going to fallback to default values.
            string expectedMessage = $@"{Constants.ErrorMessages.ApiErrorDetailsParsingError} ({statusCode}): Unauthorized";

            string prettyPrintedError = generatedError.PrettyPrint;

            Assert.Equal(expectedMessage, prettyPrintedError);

            // Now test with some error-related JSON inside the response
            string errorMessageStringJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": \"ERROR_MESSAGE\", \"errors\": []}}";

            // Generate a dummy HttpResponseMessage with the given status code to parse
            httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString(CultureInfo.InvariantCulture));
            response = new()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(errorMessageStringJson)
            };

            generatedError = await ApiError.FromErrorResponse(response);

            expectedMessage = $@"ERROR_CODE (401): ERROR_MESSAGE";

            prettyPrintedError = generatedError.PrettyPrint;

            Assert.Equal(expectedMessage, prettyPrintedError);

            // Now test with some error-related JSON inside the response with sub-errors
            errorMessageStringJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": \"ERROR_MESSAGE\", \"errors\": [{\"field\": \"SUB_ERROR_FIELD\", \"message\": \"SUB_ERROR_MESSAGE\"}]}}";
            List<Error> subErrors =
            [
                new Error
                {
                    Field = "SUB_ERROR_FIELD",
                    RawMessage = "SUB_ERROR_MESSAGE"
                }
            ];

            // Generate a dummy HttpResponseMessage with the given status code to parse
            httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString(CultureInfo.InvariantCulture));
            response = new()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(errorMessageStringJson)
            };

            generatedError = await ApiError.FromErrorResponse(response);

            expectedMessage = subErrors.Aggregate($@"ERROR_CODE (401): ERROR_MESSAGE", (current, error) => current + $@"
                            Field: {error.Field}
                            Message: {error.Message}
                    ");

            prettyPrintedError = generatedError.PrettyPrint;

            Assert.Equal(expectedMessage, prettyPrintedError);
        }

        [Fact]
        [Testing.Exception]
        public void TestExceptionConstructors()
        {
            const string testMessage = "This is a test message.";
            const string testPropertyName = "test_property";
            const string testPropertyName2 = "test_property2";
            Type testType = typeof(ExceptionsTests);

            // Test the base EasyPostError constructor
            // Class is abstract, cannot be directly constructed

            // Test the base ApiError constructor
            // Constructor is protected (black-boxed), so we can't access it

            // Test all the API error constructors
            ConnectionError error = new(testMessage, 0);
            Assert.Equal(testMessage, error.Message);

            ExternalApiError externalApiError = new(testMessage, 0);
            Assert.Equal(testMessage, externalApiError.Message);

            GatewayTimeoutError gatewayTimeoutError = new(testMessage, 0);
            Assert.Equal(testMessage, gatewayTimeoutError.Message);

            InternalServerError internalServerError = new(testMessage, 0);
            Assert.Equal(testMessage, internalServerError.Message);

            InvalidRequestError invalidRequestError = new(testMessage, 0);
            Assert.Equal(testMessage, invalidRequestError.Message);

            MethodNotAllowedError methodNotAllowedError = new(testMessage, 0);
            Assert.Equal(testMessage, methodNotAllowedError.Message);

            NotFoundError notFoundError = new(testMessage, 0);
            Assert.Equal(testMessage, notFoundError.Message);

            PaymentError paymentError = new(testMessage, 0);
            Assert.Equal(testMessage, paymentError.Message);

            ProxyError proxyError = new(testMessage, 0);
            Assert.Equal(testMessage, proxyError.Message);

            RateLimitError rateLimitError = new(testMessage, 0);
            Assert.Equal(testMessage, rateLimitError.Message);

            RedirectError redirectError = new(testMessage, 0);
            Assert.Equal(testMessage, redirectError.Message);

            Assert.Throws<NotImplementedException>(() => new RetryError(testMessage, 0));

            ServiceUnavailableError serviceUnavailableError = new(testMessage, 0);
            Assert.Equal(testMessage, serviceUnavailableError.Message);

            SslError sslError = new(testMessage, 0);
            Assert.Equal(testMessage, sslError.Message);

            TimeoutError timeoutError = new(testMessage, 0);
            Assert.Equal(testMessage, timeoutError.Message);

            UnauthorizedError unauthorizedError = new(testMessage, 0);
            Assert.Equal(testMessage, unauthorizedError.Message);

            UnknownHttpError unexpectedHttpError = new(testMessage, 0);
            Assert.Equal(testMessage, unexpectedHttpError.Message);

            // Test the base general error constructors
            // Does not exist

            // Test all the general error constructors
            FilteringError filteringError = new(testMessage);
            Assert.Equal(testMessage, filteringError.Message);

            InvalidObjectError invalidObjectError = new(testMessage);
            Assert.Equal(testMessage, invalidObjectError.Message);

            InvalidParameterError invalidParameterError = new(testPropertyName);
            Assert.Equal($"{string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameter, testPropertyName)}. ", invalidParameterError.Message);

            InvalidParameterPairError invalidParameterPairError = new(testPropertyName, testPropertyName2);
            Assert.Equal($"{string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameterPair, testPropertyName, testPropertyName2)}. ", invalidParameterPairError.Message);

            JsonDeserializationError jsonDeserializationError = new(testType);
            Assert.Equal(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.JsonDeserializationError, testType.FullName), jsonDeserializationError.Message);

            JsonSerializationError jsonSerializationError = new(testType);
            Assert.Equal(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.JsonSerializationError, testType.FullName), jsonSerializationError.Message);

            JsonNoDataError jsonNoDataError = new();
            Assert.Equal(Constants.ErrorMessages.JsonNoDataToDeserialize, jsonNoDataError.Message);

            MissingParameterError missingParameterError = new(testPropertyName);
            Assert.Equal(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingRequiredParameter, testPropertyName), missingParameterError.Message);

            object testObject = new List<string>();
            MissingPropertyError missingPropertyError = new(testObject, testPropertyName);
#pragma warning disable CA2241 // Provide correct arguments to formatting methods
            Assert.Equal(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingProperty, new object[] { testObject.GetType().Name, testPropertyName }), missingPropertyError.Message);
#pragma warning restore CA2241 // Provide correct arguments to formatting methods

            SignatureVerificationError signatureVerificationError = new();
            Assert.Equal(Constants.ErrorMessages.InvalidWebhookSignature, signatureVerificationError.Message);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestBadParameters()
        {
            UseVCR("bad_parameters");

            try
            {
                Shipment _ = await Client.Shipment.Create(new Dictionary<string, object>());
            }
            catch (InvalidRequestError error)
            {
                Assert.Equal(422, error.StatusCode);
                Assert.Equal("PARAMETER.REQUIRED", error.Code);
                Assert.Equal("Missing required parameter.", error.Message);
                Assert.True(error.Errors.Count == 1);
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

            ApiError error = await ApiError.FromErrorResponse(response);

            Assert.Equal("ERROR_MESSAGE_1", error.Message);

            const string errorMessageArrayJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": [\"ERROR_MESSAGE_1\", \"ERROR_MESSAGE_2\"], \"errors\": []}}";
            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageArrayJson),
            };

            error = await ApiError.FromErrorResponse(response);
            Assert.Equal("ERROR_MESSAGE_1, ERROR_MESSAGE_2", error.Message);

            // Test that it can go down multiple levels into sub-dictionaries and collect multiple key-value pairs across multiple levels
            const string errorMessageDictJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": {\"errors\": {\"errors\": {\"errors\": {\"errors\": [\"ERROR_MESSAGE_1\", \"ERROR_MESSAGE_2\"], \"second_element\": \"ERROR_MESSAGE_3\"}}, \"third_element\": \"ERROR_MESSAGE_4\"}}, \"errors\": []}}";
            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageDictJson),
            };

            error = await ApiError.FromErrorResponse(response);
            Assert.Equal("ERROR_MESSAGE_1, ERROR_MESSAGE_2, ERROR_MESSAGE_3, ERROR_MESSAGE_4", error.Message);

            const string errorMessageBadFormatJson = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": {bad_key\": \"bad_value\"}, \"ERROR_MESSAGE_2\"], \"errors\": []}}";
            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessageBadFormatJson),
            };

            error = await ApiError.FromErrorResponse(response);
            Assert.Equal("Bad Request", error.Message);
        }

        [Fact]
        [Testing.Exception]
        public void TestExceptionMessageFormatting()
        {
            Type type = typeof(Address);
            JsonError jsonError = new JsonDeserializationError(type);

            string expectedMessage = string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.JsonDeserializationError, type.FullName);

            Assert.Equal(expectedMessage, jsonError.Message);
        }

        [Fact]
        [Testing.Exception]
        public void TestGetEasyPostExceptionTypeByStatusCode()
        {
            // test with valid error code
            Type? exceptionType = Constants.GetEasyPostExceptionType(HttpStatusCode.NotFound);
            Assert.Equal(typeof(NotFoundError), exceptionType);

            // test with non-error code
            exceptionType = Constants.GetEasyPostExceptionType(HttpStatusCode.OK);
            Assert.Null(exceptionType);
        }

        [Fact]
        [Testing.Exception]
        public void TestGetEasyPostExceptionTypeByStatusCodeInt()
        {
            // test with valid error code
            Type? exceptionType = Constants.GetEasyPostExceptionType(404);
            Assert.Equal(typeof(NotFoundError), exceptionType);

            // test with non-error code
            exceptionType = Constants.GetEasyPostExceptionType(200);
            Assert.Null(exceptionType);

            // Test with non-existent error code
            exceptionType = Constants.GetEasyPostExceptionType(1000);
            Assert.Null(exceptionType);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestKnownApiExceptionGeneration()
        {
            // all the error status codes the library should be able to handle
            Dictionary<int, Type> exceptionsMap = new()
            {
                { 100, typeof(UnknownHttpError) },
                { 101, typeof(UnknownHttpError) },
                { 102, typeof(UnknownHttpError) },
                { 103, typeof(UnknownHttpError) },
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
                HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCodeInt.ToString(CultureInfo.InvariantCulture));
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
        // ReSharper disable once InconsistentNaming
        public async Task TestUnknownApiException1xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 1xx error, it should throw an UnexpectedHttpError
            const int unexpectedStatusCode = 199;

            // Generate a dummy HttpResponseMessage with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString(CultureInfo.InvariantCulture));
            HttpResponseMessage response = new() { StatusCode = statusCode };

            ApiError generatedError = await ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnexpectedHttpError
            Assert.Equal(typeof(UnknownHttpError), generatedError.GetType());
        }

        [Fact]
        [Testing.Exception]
        // ReSharper disable once InconsistentNaming
        public async Task TestUnknownApiException3xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 3xx error, it should throw an UnexpectedHttpError
            const int unexpectedStatusCode = 319;

            // Generate a dummy HttpResponseMessage with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString(CultureInfo.InvariantCulture));
            HttpResponseMessage response = new() { StatusCode = statusCode };

            ApiError generatedError = await ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnexpectedHttpError
            Assert.Equal(typeof(UnknownHttpError), generatedError.GetType());
        }

        [Fact]
        [Testing.Exception]
        // ReSharper disable once InconsistentNaming
        public async Task TestUnknownApiException4xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 4xx error, it should throw an UnknownApiError
            const int unexpectedStatusCode = 418;

            // Generate a dummy HttpResponseMessage with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString(CultureInfo.InvariantCulture));
            HttpResponseMessage response = new() { StatusCode = statusCode };

            ApiError generatedError = await ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnknownApiError
            Assert.Equal(typeof(UnknownHttpError), generatedError.GetType());
        }

        [Fact]
        [Testing.Exception]
        // ReSharper disable once InconsistentNaming
        public async Task TestUnknownApiException5xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 5xx error, it should throw an UnexpectedHttpError
            const int unexpectedStatusCode = 502;

            // Generate a dummy HttpResponseMessage with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString(CultureInfo.InvariantCulture));
            HttpResponseMessage response = new() { StatusCode = statusCode };

            ApiError generatedError = await ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnexpectedHttpError
            Assert.Equal(typeof(UnknownHttpError), generatedError.GetType());
        }

        [Fact]
        [Testing.Exception]
        // ReSharper disable once InconsistentNaming
        public async Task TestHTTPTimeoutFriendlyException()
        {
            // create a new client with a very short timeout
            Client client = new(new ClientConfiguration(TestUtils.GetApiKey(TestUtils.ApiKey.Test))
            {
                Timeout = TimeSpan.FromMilliseconds(1),
            });

            // make a real request that should timeout, assert that it threw a friendly TimeoutError rather than a TaskCanceledException
            try
            {
                await client.Address.Create(Fixtures.CaAddress1);
                // if we get here, the request didn't time out, consider test failed
                Assert.Fail("Request did not time out");
            }
            catch (Exception e) // capture any exception
            {
                // we should get here
                // assert that the exception is of type TimeoutError
                Assert.IsType<TimeoutError>(e);
                Assert.Equal(Constants.ErrorMessages.ApiRequestTimedOut, e.Message);
            }
        }

        #endregion
    }
#pragma warning restore CA2263
}
