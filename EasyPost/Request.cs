using RestSharp;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Request(string resource, Method method = Method.GET) {
            restRequest = new RestRequest(resource, method);
            restRequest.AddHeader("Accept", "application/json");
        }

        public T Execute<T>() where T : new() {
            Client client = new Client();
            return client.Execute<T>(this);
        }

        public IRestResponse Execute() {
            Client client = new Client();
            return client.Execute(this);
        }

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        public void AddParameter(string name, string value, ParameterType type) {
            restRequest.AddParameter(name, value, type);
        }

        public void AddBody(IDictionary<string, object> parameters, string parent) {
            string encoded = EncodeParameters(FlattenParameters(parameters, parent));
            AddParameter("application/x-www-form-urlencoded", encoded, ParameterType.RequestBody);
        }

        public void AddQueryString(IDictionary<string, object> parameters) {
            foreach (KeyValuePair<string, object> pair in parameters) {
                AddParameter((string)pair.Key, (string)pair.Value, ParameterType.QueryString);
            }
        }

        public void AddBody(IEnumerable<IDictionary<string, object>> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.AddRange(FlattenParameters(parameters.ToList()[i], string.Concat(parent, "[", i, "]")));
            }
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(result), ParameterType.RequestBody);
        }

        public void AddBody(IEnumerable<Tuple<string, string>> parameters) {
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(parameters), ParameterType.RequestBody);
        }

        public void AddBody(IEnumerable<string> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.Add(new Tuple<string, string>(string.Concat(parent, "[", i.ToString(), "]"), parameters.ElementAt(i)));
            }
            AddParameter("application/x-www-form-urlencoded", EncodeParameters(result), ParameterType.RequestBody);
        }

        internal string EncodeParameters(IEnumerable<Tuple<string, string>> parameters) {
            return string.Join("&", parameters.Select(parameter => EncodeParameter(parameter)).ToList());
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
                } else if (pair.Value is IList && pair.Value.GetType().GetGenericArguments().Single().GetInterfaces().Contains(typeof(IResource))) {
                    foreach (IResource resource in parameters[pair.Key as string] as IEnumerable) {
                        result.AddRange(FlattenParameters(resource.AsDictionary(), string.Concat(parent, "[", pair.Key, "][", 0, "]")));
                    }
                } else if (pair.Value is List<Dictionary<string, object>>) {
                    List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.AddRange(FlattenParameters(list[i], string.Concat(parent, "[", pair.Key, "][", i, "]")));
                    }
                } else if (pair.Value != null) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), pair.Value.ToString()));
                }
            }
            return result;
        }
    }
}
