using Newtonsoft.Json;

namespace MHC.Domain.Requests
{
    public class FetchBalanceRequest : BaseRequest
    {
        public FetchBalanceRequest()
        {
            Method = "fetch-balance";
        }

        [JsonProperty("params")]
        public FetchBalanceRequestParams Parameters { get; set; }
    }
}