using System.Collections.Generic;

namespace MHC.Domain.Responses
{
    public class FetchHistoryResponse
    {
        public int Id { get; set; }

        public List<FetchHistoryResponseResult> Result { get; set; }
    }
}