using Microsoft.EntityFrameworkCore;
using PmfBackend.Database.Entities;

namespace PmfBackend.Database.Configurations {
    public class TrnasactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity> {
         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TransactionEntity> builder)
        {
            
            builder.ToTable("transactions");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.BeneficiaryName);
            builder.Property(x => x.Date);
            builder.Property(x => x.Direction);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Description);
            builder.Property(x=> x.Currency);
            builder.Property(x => x.Mcc);
            builder.Property(x => x.Kind);

        }

    }
}