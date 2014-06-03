using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Request {
        internal RestRequest restRequest;
        internal List<Parameter> payload;

        public string RootElement {
            get { return this.restRequest.RootElement; }
            set { this.restRequest.RootElement = value; }
        }

        public static explicit operator RestRequest(Request request) {
            request.addPayloadToBody();
            return request.restRequest;
        }

        public Request(string resource, Method method = Method.GET) {
            restRequest = new RestRequest(resource, method);
            payload = new List<Parameter>();
        }

        public void AddUrlSegment(string name, string value) {
            restRequest.AddUrlSegment(name, value);
        }

        internal void addPayload(string name, string value) {
            if (value != null) {
                payload.Add(new Parameter { Name = name, Value = value, Type = ParameterType.GetOrPost });
            }
        }

        internal void addPayloadToBody() {
            restRequest.AddParameter("application/x-www-form-urlencoded", encodePayload(), ParameterType.RequestBody);
        }

        internal string encodePayload() {
            return string.Join("&", payload.Select(parameter => encodeParameter(parameter)).ToList());
        }

        internal string encodeParameter(Parameter parameter) {
            return string.Concat(Uri.EscapeDataString(parameter.Name), "=", Uri.EscapeDataString(parameter.Value.ToString()));
        }
    }
}
