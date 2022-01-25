using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace EasyPost
{
    public class Client
    {
        private readonly string _libraryVersion;
        private readonly string _dotNetVersion;
        private readonly RestClient _restClient;
        private readonly ClientConfiguration _configuration;
        private string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion}";

        private RestRequest PrepareRequest(Request request)
        {
            var restRequest = (RestRequest)request;

            restRequest.AddHeader("user_agent", UserAgent);
            restRequest.AddHeader("authorization", "Bearer " + _configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }

        internal IRestResponse Execute(Request request)
        {
            return _restClient.Execute(PrepareRequest(request));
        }

        internal T Execute<T>(Request request) where T : new()
        {
            var response = (RestResponse<T>)_restClient.Execute<T>(PrepareRequest(request));
            var statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode < 400)
            {
                return response.Data;
            }

            Dictionary<string, Dictionary<string, object>> body;
            List<Error> errors;

            try
            {
                body = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                errors = JsonConvert.DeserializeObject<List<Error>>(
                    JsonConvert.SerializeObject(body["error"]["errors"]));
            }
            catch
            {
                throw new HttpException(statusCode, "RESPONSE.PARSE_ERROR", response.Content, new List<Error>());
            }

            throw new HttpException(
                statusCode,
                (string)body["error"]["code"],
                (string)body["error"]["message"],
                errors
            );
        }

        public Client(ClientConfiguration clientConfiguration)
        {
            System.Net.ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            _configuration = clientConfiguration ?? throw new ArgumentNullException("clientConfiguration");

            _restClient = new RestClient(clientConfiguration.ApiBase);

            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            _libraryVersion = info.FileVersion;

            var dotNetVersion = Environment.Version.ToString();
            if (dotNetVersion == "4.0.30319.42000")
            {
                /*
                 * We're on a v4.6+ version (or pre-.NET Core 3.0, which we don't support),
                 * but we can't get the exact version.
                 * See: https://docs.microsoft.com/en-us/dotnet/api/system.environment.version?view=net-6.0#remarks
                 */
                dotNetVersion = "4.6 or higher";
            }

            _dotNetVersion = dotNetVersion;
        }
    }
}