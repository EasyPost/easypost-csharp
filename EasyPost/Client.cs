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

        public string version;

        internal RestClient restClient;

        public Client(string apiBase = "https://api.easypost.com/v2") {
            System.Net.ServicePointManager.SecurityProtocol = Security.GetProtocol();

            restClient = new RestClient(apiBase);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = info.FileVersion;
        }

        public T Execute<T>(Request request) where T : new() {
            RestResponse<T> response = (RestResponse<T>)restClient.Execute<T>(prepareRequest(request));
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode < 200 || statusCode > 299) {
                if (statusCode == 500) {
                    throw new HttpException(statusCode, "We're sorry, something went wrong. If the problem persists please contact us at support@easypost.com or open an issue on GitHub.");
                }

                string message;
                if (response.Content == "") {
                    message = "";
                } else {
                    try {
                        message = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response.Content)["error"]["message"];
                    } catch (JsonSerializationException) {
                        message = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)["error"];
                    }
                }

                throw new HttpException(statusCode, message);
            }

            return response.Data;
        }

        internal RestRequest prepareRequest(Request request) {
            RestRequest restRequest = (RestRequest)request;

            restRequest.AddHeader("user_agent", string.Concat("EasyPost/v2 CSharp/", version));
            restRequest.AddHeader("authorization", "Bearer " + apiKey);
            restRequest.AddHeader("content_type", "application/x-www-form-urlencoded");

            return restRequest;
        }
    }
}