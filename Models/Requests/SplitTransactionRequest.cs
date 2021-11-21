using PmfBackend.Models;
using System.Collections.Generic;

namespace PmfBackend.Models.Requests  { 

    public class SplitTransactionRequest {
        public List<SplitTransactionModel> splits { get; set; }
    }    
}