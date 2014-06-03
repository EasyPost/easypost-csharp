using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Client {
        public static string apiKey { get; set; }

        internal RestClient restClient;

        public Client() {
            restClient = new RestClient("https://api.easypost.com/v2");
        }
        public Client(string apiBase) {
            restClient = new RestClient(apiBase);
        }

        public T Execute<T>(Request request) where T : new() {
            RestResponse<T> response = (RestResponse<T>) restClient.Execute<T>(prepareRequest(request));

            if (response.ErrorMessage != null) {
                throw new InvalidRequest(response.ErrorMessage);
            }  else if (response.StatusCode == HttpStatusCode.BadRequest) {
                string message = JsonConvert.DeserializeObject<IDictionary<string, string>>(response.Content)["error"];
                throw new InvalidRequest(message);
            } else if (response.StatusCode == HttpStatusCode.NotFound) {
                throw new ResourceNotFound();
            }

            return response.Data;
        }

        internal RestRequest prepareRequest(Request request) {
            RestRequest restRequest = (RestRequest) request;

            restRequest.AddHeader("user_agent", "EasyPost/v2 CSharp");
            restRequest.AddHeader("authorization", "Bearer " + apiKey);
            restRequest.AddHeader("content_type", "application/x-www-form-urlencoded");

            return restRequest;
        }
    }
}
