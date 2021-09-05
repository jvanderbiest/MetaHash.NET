using Newtonsoft.Json;

namespace MHC.Domain.Responses
{
    public class FetchBalanceResponse
    {
        [JsonProperty("result")]
        public FetchBalanceResponseResult Result { get; set; }
    }
}