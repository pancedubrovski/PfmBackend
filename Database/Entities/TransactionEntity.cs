using System;
using System.ComponentModel.DataAnnotations;
using PmfBackend.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace PmfBackend.Database.Entities {
    public class TransactionEntity {
        [Column("id")]
        public string Id { get; set; }
         [Column("beneficiaryname")]      
        public string BeneficiaryName { get; set; }
        [Column("date")]      
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Column("direction")]      
        public Direction Direction { get; set; }
        [Column("amount")]              
        public double Amount { get; set; } 
        [Column("description")]   
        public string Description { get; set; }
        [Column("currency")]
        public string Currency { get; set; }
        [Column("mcc")]
        public string Mcc { get; set; } = null;
        [Column("kind")]
        public Kind Kind { get; set; } 
        [Column("catcode")]
        public string CatCode { get; set; }
        public CategoryEntity Category { get; set; }
        public List<SplitTransactionEntity> splits { get; set; }
        public MccEntity MccEntity { get; set; }
        [Column("issplit")]
        public bool IsSplit { get; set; }
    }
}