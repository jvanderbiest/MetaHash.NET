using Newtonsoft.Json;

namespace MHC.Domain.Responses
{
    public class FetchHistoryResponseResult
    {
        public string From { get; set; }

        public string To { get; set; }

        public long Value { get; set; }

        public string Transaction { get; set; }

        public string Data { get; set; }

        public int Timestamp { get; set; }

        public string Type { get; set; }

        public long BlockNumber { get; set; }

        public long BlockIndex { get; set; }

        public string Signature { get; set; }

        public string Publickey { get; set; }

        public int Fee { get; set; }

        public int RealFee { get; set; }

        public int Nonce { get; set; }

        public int IntStatus { get; set; }

        public string Status { get; set; }

        public bool IsDelegate { get; set; }

        [JsonProperty("delegate_info")]
        public DelegateInfo DelegateInfo { get; set; }

        public long Delegate { get; set; }

        public string DelegateHash { get; set; }

        [JsonIgnore]
        public double RealValue => Value * 0.000001;
    }
}