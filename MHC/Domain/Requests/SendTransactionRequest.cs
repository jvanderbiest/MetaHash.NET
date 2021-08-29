namespace MHC.Domain.Requests
{
    public class SendTransactionRequest : BaseRequest
    {
        public SendTransactionRequest()
        {
            Method = "mhc_send";
        }

        public string To { get; set; }
        public string Value { get; set; }
        public string Fee { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }
        public string PublicKey { get; set; }

        /// <summary>
        /// Generate the signature using <see cref="Wallet"/> Sign
        /// </summary>
        public string Sign { get; set; }
    }
}
