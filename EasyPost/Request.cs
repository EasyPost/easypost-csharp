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
        }

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        public void AddParameter(string contentType, string content, ParameterType type) {
            restRequest.AddParameter(contentType, content, type);
        }

        public void addBody(Dictionary<string, object> parameters, string parent) {
            string encoded = encodeParameters(flattenParameters(parameters, parent));
            AddParameter("application/x-www-form-urlencoded", encoded, ParameterType.RequestBody);
        }

        public void addBody(List<string> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            for (int i = 0; i < parameters.Count; i++) {
                result.Add(new Tuple<string, string>(string.Concat(parent, "[", i.ToString(), "]"), parameters[i]));
            }
            AddParameter("application/x-www-form-urlencoded", encodeParameters(result), ParameterType.RequestBody);
        }

        internal string encodeParameters(List<Tuple<string, string>> parameters) {
            return string.Join("&", parameters.Select(parameter => encodeParameter(parameter)).ToList());
        }

        internal List<Tuple<string, string>> flattenParameters(Dictionary<string, object> parameters, string parent) {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            foreach (KeyValuePair<string, object> pair in parameters) {
                if (pair.Value is Dictionary<string, object>) {
                    result.AddRange(flattenParameters((Dictionary<string, object>)pair.Value, string.Concat(parent, "[", pair.Key, "]")));
                } else if (pair.Value is List<Dictionary<string, object>>) {
                    List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)pair.Value;
                    for (int i = 0; i < list.Count; i++) {
                        result.AddRange(flattenParameters(list[i], string.Concat(parent, "[", pair.Key, "][", i, "]")));
                    }
                } else {
                    result.Add(new Tuple<string, string>(string.Concat(parent, "[", pair.Key, "]"), pair.Value.ToString()));
                }
            }
            return result;
        }

        internal string encodeParameter(Tuple<string, string> parameter) {
            return string.Concat(Uri.EscapeDataString(parameter.Item1), "=", Uri.EscapeDataString(parameter.Item2));
        }
    }
}
