using System;
using System.ComponentModel.DataAnnotations;
using PmfBackend.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PmfBackend.Database.Entities {
    public class TransactionEntity {
        public string Id { get; set; }
      
        public string BeneficiaryName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        public Direction Direction { get; set; }
        
        public double Amount { get; set; } 
       
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Mcc { get; set; } = null;
       
        public Kind Kind { get; set; } 
        public string CatCode { get; set; }
        public CategoryEntity Category { get; set; }
        public List<SplitTransactionEntity> splits { get; set; }
        public MccEntity MccEntity { get; set; }
        public bool IsSplit { get; set; }
    }
}