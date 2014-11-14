using RestSharp;

using System;
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

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        public void AddParameter(string contentType, string content, ParameterType type) {
            restRequest.AddParameter(contentType, content, type);
        }

        public void addBody(IDictionary<string, object> parameters, string parent) {
            string encoded = encodeParameters(flattenParameters(parameters, parent));
            AddParameter("application/x-www-form-urlencoded", encoded, ParameterType.RequestBody);
        }

        public void addBody(IEnumerable<IDictionary<string, object>> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.AddRange(flattenParameters(parameters.ToList()[i], string.Concat(parent, "[", i, "]")));
            }
            AddParameter("application/x-www-form-urlencoded", encodeParameters(result), ParameterType.RequestBody);
        }

        public void addBody(IEnumerable<Tuple<string, string>> parameters) {
            AddParameter("application/x-www-form-urlencoded", encodeParameters(parameters), ParameterType.RequestBody);
        }

        public void addBody(IEnumerable<string> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count(); i++) {
                result.Add(new Tuple<string, string>(string.Concat(parent, "[", i.ToString(), "]"), parameters.ElementAt(i)));
            }
            AddParameter("application/x-www-form-urlencoded", encodeParameters(result), ParameterType.RequestBody);
        }

        internal string encodeParameters(IEnumerable<Tuple<string, string>> parameters) {
            return string.Join("&", parameters.Select(parameter => encodeParameter(parameter)).ToList());
        }

        internal string encodeParameter(Tuple<string, string> parameter) {
            return string.Concat(Uri.EscapeDataString(parameter.Item1), "=", Uri.EscapeDataString(parameter.Item2));
        }

        internal List<Tuple<string, string>> flattenParameters(IDictionary<string, object> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            foreach (KeyValuePair<string, object> pair in parameters) {
                if (pair.Value is Dictionary<string, object>) {
                    result.AddRange(flattenParameters((Dictionary<string, object>)pair.Value, string.Concat(parent, "[", pair.Key, "]")));
                } else if (pair.Value is IResource) {
                    IResource value = (IResource)pair.Value;
                    result.AddRange(flattenParameters(value.AsDictionary(), string.Concat(parent, "[", pair.Key, "]")));
                } else if (pair.Value is List<IResource>) {
                    List<IResource> list = (List<IResource>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.AddRange(flattenParameters(list[i].AsDictionary(), string.Concat(parent, "[", pair.Key, "][", i, "]")));
                    }
                } else if (pair.Value is List<Dictionary<string, object>>) {
                    List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.AddRange(flattenParameters(list[i], string.Concat(parent, "[", pair.Key, "][", i, "]")));
                    }
                } else if (pair.Value != null) {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), pair.Value.ToString()));
                }
            }
            return result;
        }
    }
}
