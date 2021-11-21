using System.ComponentModel.DataAnnotations;
using PmfBackend.Database.Entities;
using System.Collections.Generic;
namespace PmfBackend.Database.Entities {
    public class MccEntity {

        [Key]
        public string Code { get; set; }
        public string MerchactType { get; set; }
        public List<TransactionEntity> transactions { get; set; }
    }
}