using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//using System.ComponentModel.DataAnnotations;
namespace PmfBackend.Database.Entities {
    public class SplitTransactionEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public TransactionEntity Transaction { get; set; }
        [JsonIgnore]
        public CategoryEntity CategoryEntity { get; set; }
        public string CatCode { get; set; }
        public double  Amount { get; set; }
    }
}