namespace MHC.Domain.Requests
{
    public class TransactionRequest : BaseRequest
    {
        public TransactionRequest()
        {
            Method = "get-tx";
        }

        public TransactionRequestParams Params { get; set; }
    }

    public class TransactionRequestParams
    {
        public string Hash { get; set; }
    }
}