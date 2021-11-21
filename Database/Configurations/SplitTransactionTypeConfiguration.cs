using PmfBackend.Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace PmfBackend.Database.Configurations {
    public class SplitTransactionEntityTypeConfiguration : IEntityTypeConfiguration<SplitTransactionEntity> {
         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SplitTransactionEntity> builder)
        {
            builder.ToTable("splitTransactions");    
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).IsRequired();
        
        }

    
    
    }
}

