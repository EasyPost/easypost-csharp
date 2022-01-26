using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EasyPost
{
    public class Request
    {
        internal RestRequest restRequest;

        public string RootElement
        {
            get { return this.restRequest.RootElement; }
            set { this.restRequest.RootElement = value; }
        }

        public static explicit operator RestRequest(Request request)
        {
            return request.restRequest;
        }

        public Request(string IResource, Method method = Method.GET)
        {
            restRequest = new RestRequest(IResource, method);
            restRequest.AddHeader("Accept", "application/json");
        }

        /// <summary>
        /// Execute the request.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public T Execute<T>() where T : new()
        {
            Client client = ClientManager.Build();
            return client.Execute<T>(this);
        }

        /// <summary>
        /// Execute the request.
        /// </summary>
        /// <returns>An IRestResponse object instance.</returns>
        public IRestResponse Execute()
        {
            Client client = ClientManager.Build();
            return client.Execute(this);
        }

        /// <summary>
        /// Add a URL segment to the request.
        /// </summary>
        /// <param name="name">Name of segment.</param>
        /// <param name="value">Value of segment.</param>
        public void AddUrlSegment(string name, string value)
        {
            restRequest.AddUrlSegment(name, value);
        }

        /// <summary>
        /// Add a parameter to the request.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <param name="type">Type of parameter.</param>
        public void AddParameter(string name, string value, ParameterType type)
        {
            restRequest.AddParameter(name, value, type);
        }

        /// <summary>
        /// Add a query string parameter to the request.
        /// </summary>
        /// <param name="parameters">Dictionary of key-value pairs for creating URL query parameters.</param>
        public void AddQueryString(IDictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                AddParameter((string)pair.Key, Convert.ToString(pair.Value), ParameterType.QueryString);
            }
        }

        /// <summary>
        /// Add a body to the request.
        /// </summary>
        /// <param name="parameters">Dictionary of key-value pairs for creating request body.</param>
        public void AddBody(Dictionary<string, object> parameters)
        {
            AddParameter("application/json", JsonConvert.SerializeObject(parameters), ParameterType.RequestBody);
        }
    }
}
