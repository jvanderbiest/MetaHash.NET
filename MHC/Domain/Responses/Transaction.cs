using Newtonsoft.Json;

namespace MHC.Domain.Responses
{
    public class Transaction
    {
        public string From { get; set; }

        public string To { get; set; }

        public long Value { get; set; }

        public string TransactionTransaction { get; set; }

        public string Data { get; set; }

        public long Timestamp { get; set; }

        public string Type { get; set; }

        public long BlockNumber { get; set; }

        public long BlockIndex { get; set; }

        public string Signature { get; set; }

        [JsonProperty("publickey")]
        public string PublicKey { get; set; }

        public long Fee { get; set; }

        public long RealFee { get; set; }

        public long Nonce { get; set; }

        public long IntStatus { get; set; }

        public string Status { get; set; }
    }
}
