namespace MHC.Domain.Requests
{
    public class TransactionRequest : BaseRequest
    {
        public TransactionRequest()
        {
            Method = "get-tx";
        }

        public string Hash { get; set; }
    }
}