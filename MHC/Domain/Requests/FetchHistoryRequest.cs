namespace MHC.Domain.Requests
{
    public class FetchHistoryRequest : BaseRequest
    {
        public FetchHistoryRequest()
        {
            Method = "fetch-history";
        }

        /// <summary>
        /// The address of the wallet to get the history from
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The total number of transaction history records to retrieve
        /// </summary>
        public int CountTx { get; set; }

        /// <summary>
        /// The start of the transaction history to retrieve in the whole transaction list
        /// </summary>
        public int BeginTx { get; set; }
    }
}