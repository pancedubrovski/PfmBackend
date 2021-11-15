using PmfBackend.Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace PmfBackend.Database.Configurations {
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryEntity> {
         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("categories");    
            builder.HasKey(x => x.Code);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.ParentCode);
            builder.Property(x => x.Name).IsRequired();
        
        }

    
    
    }
}