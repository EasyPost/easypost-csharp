using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using RestSharp;
using Xunit;

namespace EasyPost.Tests
{
    public class ErrorTest : UnitTest
    {
        public ErrorTest() : base("error")
        {
        }

        [Fact]
        public async Task TestBadParameters()
        {
            UseVCR("bad_parameters");

            try
            {
                var _ = await Client.Shipment.Create(new Dictionary<string, object>());
            }
            catch (InvalidRequestError error)
            {
                Assert.Equal(422, error.StatusCode);
                Assert.Equal("PARAMETER.REQUIRED", error.Code);
                Assert.Equal("Missing required parameter.", error.Message);
                Assert.True(error.Errors.Count == 1);
            }
        }

        [Fact(Skip = "Test is no longer valid")]
        public void TestEmptyApiKey()
        {
            // No longer possible to have an empty API key
        }

        [Fact]
        public Task TestKnownApiExceptionGeneration()
        {
            // all the error status codes the library should be able to handle
            Dictionary<int, Type> exceptionsMap = new Dictionary<int, Type>
            {
                { 0, typeof(VcrError) }, // EasyVCR uses 0 as the status code when a recording cannot be found
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

                // Generate a dummy RestResponse with the given status code to parse
                HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCodeInt.ToString());
                RestResponse response = new RestResponse { StatusCode = statusCode };

                ApiError generatedError = ApiError.FromErrorResponse(response);

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

            return Task.CompletedTask;
        }

        [Fact]
        public void TestUnknownApiException4xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 4xx error, it should throw an UnknownApiError
            const int unexpectedStatusCode = 418;

            // Generate a dummy RestResponse with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString());
            RestResponse response = new RestResponse { StatusCode = statusCode };

            ApiError generatedError = ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnknownApiError
            Assert.Equal(typeof(UnknownApiError), generatedError.GetType());
        }

        [Fact]
        public void TestUnknownApiException5xxGeneration()
        {
            // library does not have a specific exception for this status code
            // Since it's a 5xx error, it should throw an UnexpectedHttpError
            const int unexpectedStatusCode = 502;

            // Generate a dummy RestResponse with the given status code to parse
            HttpStatusCode statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), unexpectedStatusCode.ToString());
            RestResponse response = new RestResponse { StatusCode = statusCode };

            ApiError generatedError = ApiError.FromErrorResponse(response);

            // the exception should be of base type ApiError
            Assert.Equal(typeof(ApiError), generatedError.GetType().BaseType);
            // specifically, the exception should be of type UnexpectedHttpError
            Assert.Equal(typeof(UnexpectedHttpError), generatedError.GetType());
        }

        [Fact]
        public void TestExceptionMessageFormatting()
        {
            Type type = typeof(Address);
            JsonError jsonError = new JsonDeserializationError(type);

            string expectedMessage = string.Format(Constants.ErrorMessages.JsonDeserializationError, type.FullName);

            Assert.Equal(expectedMessage, jsonError.Message);
        }

        [Fact]
        public void TestExceptionErrorMessageParsing()
        {
            string json = "{\"error\": {\"code\": \"ERROR_CODE\", \"message\": [\"ERROR_MESSAGE_1\", \"ERROR_MESSAGE_2\"], \"errors\": []}}";

            RestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = json
            };

            ApiError error = ApiError.FromErrorResponse(response);

            Assert.Equal("ERROR_CODE", error.Code);
            Assert.Equal("ERROR_MESSAGE_1, ERROR_MESSAGE_2", error.Message);
        }

        [Fact]
        public void TestApiExceptionPrettyPrint()
        {
            const int statusCode = 401;

            // Generate a dummy RestResponse with the given status code to parse
            HttpStatusCode httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());
            RestResponse response = new RestResponse { StatusCode = httpStatusCode };

            ApiError generatedError = ApiError.FromErrorResponse(response);

            // unfortunately, we can't easily load error-related JSON data into the response for parsing, so the pretty print is going to fallback to default values.
            string expectedMessage = $@"{Constants.ErrorMessages.ApiErrorDetailsParsingError} ({statusCode}): {Constants.ErrorMessages.ApiDidNotReturnErrorDetails}";

            string prettyPrintedError = generatedError.PrettyPrint;

            Assert.Equal(expectedMessage, prettyPrintedError);
        }
    }
}
