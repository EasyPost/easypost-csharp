using RestSharp;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        /// <summary>Execute the client query as defined in the request object</summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public T Execute<T>(string apiKey = null) where T : new()
        {
            //Use the passed in apiKey if defined otherwise use what is in the singleton ClientManager
            Client client = apiKey == null ? ClientManager.Build() : new Client(new ClientConfiguration(apiKey));
            return client.Execute<T>(this);
        }

        /// <summary>
        /// </summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public IRestResponse Execute(string apiKey = null) {
            //Use the passed in apiKey if defined otherwise use what is in the singleton ClientManager
            Client client = apiKey == null ? ClientManager.Build() : new Client(new ClientConfiguration(apiKey));
            return client.Execute(this);
        }

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        public void AddParameter(string name, string value, ParameterType type) {
            restRequest.AddParameter(name, value, type);
        }

        public void AddBody(Dictionary<string, object> parameters, string parent) {
            string encoded = EncodeParameters(FlattenParameters(parameters, parent));
            AddParameter("application/x-www-form-urlencoded", encoded, ParameterType.RequestBody);
        }

        public void AddQueryString(IDictionary<string, object> parameters) {
            foreach (KeyValuePair<string, object> pair in parameters) {
                AddParameter((string)pair.Key, Convert.ToString(pair.Value), ParameterType.QueryString);
            }
        }

        public void AddBody(List<Dictionary<string, object>> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.AddRange(FlattenParameters(parameters.ToList()[i], string.Concat(parent, "[", i, "]")));
            }
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(result), ParameterType.RequestBody);
        }

        public void AddBody(List<Tuple<string, string>> parameters) {
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(parameters), ParameterType.RequestBody);
        }

        public void AddBody(List<string> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.Add(new Tuple<string, string>(string.Concat(parent, "[", i.ToString(), "]"), parameters.ElementAt(i)));
            }
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(result), ParameterType.RequestBody);
        }

        internal string EncodeParameters(List<Tuple<string, string>> parameters) {
            return string.Join("&", parameters.Select(parameter => EncodeParameter(parameter)).ToArray());
        }

        internal string EncodeParameter(Tuple<string, string> parameter) {
            return string.Concat(Uri.EscapeDataString(parameter.Item1), "=", Uri.EscapeDataString(parameter.Item2));
        }

        internal List<Tuple<string, string>> FlattenParameters(IDictionary<string, object> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            foreach (KeyValuePair<string, object> pair in parameters) {
                if (pair.Value is Dictionary<string, object>) {
                    result.AddRange(FlattenParameters((Dictionary<string, object>)pair.Value, string.Concat(parent, "[", pair.Key, "]")));
                } else if (pair.Value is IResource) {
                    IResource value = (IResource)pair.Value;
                    result.AddRange(FlattenParameters(value.AsDictionary(), string.Concat(parent, "[", pair.Key, "]")));
                } else if (pair.Value is List<IResource>) {
                    FlattenList(parent, result, pair);
                } else if (pair.Value is IList && pair.Value.GetType().GetGenericArguments().Single().GetInterfaces().Contains(typeof(IResource))) {
                    FlattenList(parent, result, pair);
                } else if (pair.Value is List<string>) {
                    List<string> list = (List<string>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "][", i, "]"), list[i]));
                    }
                } else if (pair.Value is List<Dictionary<string, object>>) {
                    List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.AddRange(FlattenParameters(list[i], string.Concat(parent, "[", pair.Key, "][", i, "]")));
                    }
                } else if (pair.Value is DateTime) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), Convert.ToString(((DateTime)pair.Value).ToString("yyyy-MM-ddTHH:mm:ssZ"))));
                } else if (pair.Value is decimal) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), ((decimal)pair.Value).ToString(CultureInfo.InvariantCulture)));
                } else if (pair.Value is double) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), ((double)pair.Value).ToString(CultureInfo.InvariantCulture)));
                } else if (pair.Value is float) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), ((float)pair.Value).ToString(CultureInfo.InvariantCulture)));
                } else if (pair.Value != null) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), pair.Value.ToString()));
                }
            }
            return result;
        }

        private void FlattenList(string parent, List<Tuple<string, string>> result, KeyValuePair<string, object> pair) {
            var index = 0;
            foreach (IResource IResource in pair.Value as IEnumerable) {
                result.AddRange(FlattenParameters(IResource.AsDictionary(), string.Concat(parent, "[", pair.Key, "][", index, "]")));
                index++;
            }
        }
    }
}
