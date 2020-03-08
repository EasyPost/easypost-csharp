using RestSharp;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Request {
        internal RestRequest restRequest;

        public string RootElement {
            get { return this.restRequest.RootElement; }
            set { this.restRequest.RootElement = value; }
        }

        public static explicit operator RestRequest(Request request) {
            return request.restRequest;
        }

        public Request(string IResource, Method method = Method.GET) {
            restRequest = new RestRequest(IResource, method);
            restRequest.AddHeader("Accept", "application/json");
        }

        public T Execute<T>() where T : new() {
            Client client = ClientManager.Build();
            return client.Execute<T>(this);
        }

        public IRestResponse Execute() {
            Client client = ClientManager.Build();
            return client.Execute(this);
        }

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        public void AddParameter(string name, string value, ParameterType type) {
            restRequest.AddParameter(name, value, type);
        }

        public void AddQueryString(IDictionary<string, object> parameters) {
            foreach (KeyValuePair<string, object> pair in parameters) {
                AddParameter((string)pair.Key, Convert.ToString(pair.Value), ParameterType.QueryString);
            }
        }

        public void AddBody(Dictionary<string, object> parameters) {
            AddParameter("application/json", JsonConvert.SerializeObject(parameters), ParameterType.RequestBody);
        }
    }
}
