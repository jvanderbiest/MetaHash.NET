using Newtonsoft.Json;

namespace MHC.Domain.Responses
{
    public class FetchBalanceResponseResult
    {
        public string Address { get; set; }

        public long Received { get; set; }

        public long Spent { get; set; }

        [JsonProperty("count_received")]
        public long CountReceived { get; set; }

        [JsonProperty("count_spent")]
        public int CountSpent { get; set; }

        [JsonProperty("count_txs")]
        public long CountTxs { get; set; }

        [JsonProperty("block_number")]
        public long BlockNumber { get; set; }

        public long CurrentBlock { get; set; }

        public string Hash { get; set; }
    }
}