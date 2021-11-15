using System.Collections.Generic;

namespace PmfBackend.Database.Entities {
    public class CategoryEntity {
        
        public string Code { get; set; }
        
        public string? ParentCode { get; set; }
        public string Name { get; set; }
        public CategoryEntity Category { get; set; }
        public List<TransactionEntity> Transactions {get; set;}
        
    }
}