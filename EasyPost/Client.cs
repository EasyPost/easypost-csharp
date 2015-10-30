using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyPost {
    public class Client {
        public static string apiKey { get; set; }
        public static string apiBase { get; set; }

        public string version;

        internal RestClient client;

        public Client(string apiBaseUrl = "https://api.easypost.com/v2") {
            System.Net.ServicePointManager.SecurityProtocol = Security.GetProtocol();

            apiBaseUrl = apiBase ?? apiBaseUrl;
            client = new RestClient(apiBaseUrl);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = info.FileVersion;
        }

        public IRestResponse Execute(Request request) {
            return client.Execute(PrepareRequest(request));
        }

        public T Execute<T>(Request request) where T : new() {
            RestResponse<T> response = (RestResponse<T>)client.Execute<T>(PrepareRequest(request));
            int StatusCode = Convert.ToInt32(response.StatusCode);

            if (StatusCode > 399) {
                try {
                    Dictionary<string, Dictionary<string, object>> Body = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                    throw new HttpException(StatusCode, (string)Body["error"]["message"], (string)Body["error"]["code"]);
                } catch {
                    throw new HttpException(StatusCode, "RESPONSE.PARSE_ERROR", response.Content);
                }
            }

            return response.Data;
        }

        internal RestRequest PrepareRequest(Request request) {
            RestRequest restRequest = (RestRequest)request;

            restRequest.AddHeader("user_agent", string.Concat("EasyPost/v2 CSharp/", version));
            restRequest.AddHeader("authorization", "Bearer " + apiKey);
            restRequest.AddHeader("content_type", "application/x-www-form-urlencoded");

            return restRequest;
        }
    }
}