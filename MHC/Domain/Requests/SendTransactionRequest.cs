using MHC.Internals;
using Newtonsoft.Json;

namespace MHC.Domain.Requests
{
    public class SendTransactionRequest : BaseRequest
    {
        public SendTransactionRequest()
        {
            Method = "mhc_send";
        }

        [JsonProperty("params")]
        public SendTransactionRequestParams Parameters { get; set; }
    }

    public class SendTransactionRequestParams
    {
        /// <summary>
        /// The recipient
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The amount of mhc HASH
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// An optional fee for the transaction
        /// </summary>
        public string Fee { get; set; }

        /// <summary>
        /// Should match the latest transaction for the wallet + 1
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Message that goes with the transaction
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The public key for the target wallet
        /// </summary>
        [JsonProperty("pubkey")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Generate the signature using <see cref="Crypto.Sign"/> Sign
        /// </summary>
        public string Sign { get; set; }
    }
}
