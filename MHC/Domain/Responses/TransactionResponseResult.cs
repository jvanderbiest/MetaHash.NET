namespace MHC.Domain.Responses
{
    public class TransactionResponseResult
    {
        public Transaction Transaction { get; set; }

        public long CountBlocks { get; set; }

        public long KnownBlocks { get; set; }
    }
}