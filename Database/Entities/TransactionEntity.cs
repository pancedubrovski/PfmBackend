using System;
using System.ComponentModel.DataAnnotations;

namespace PmfBackend.Database.Entities {
    public class TransactionEntity {
        public string Id { get; set; }
      
        public string BeneficiaryName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        public string Direction { get; set; }
        
        public double Amount { get; set; } 
       
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Mcc { get; set; }
       
        public string Kind { get; set; } 
        public string CatCode { get; set; }
        public CategoryEntity Category { get; set; }
    }
}