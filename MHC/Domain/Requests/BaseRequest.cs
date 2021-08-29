using Newtonsoft.Json;

namespace MHC.Domain.Requests
{
    public class BaseRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc = "2.0";

        public string Method { get; protected set; }
    }
}
