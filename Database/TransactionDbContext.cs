using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PmfBackend.Database.Entities;
using PmfBackend.Models;

namespace PmfBackend.Database {
    public class TransactionDbContext : DbContext {

        public DbSet<TransactionEntity> Transactions {get; set;}
        public DbSet<CategoryEntity> Categories {get; set;}
        public DbSet<AnalyticsModel> AnalyticsModels { get; set; }
        public DbSet<SplitTransactionEntity> SplitTransactionEntities {get; set;}
        public DbSet<MccEntity> MccCodes { get; set; }

        public TransactionDbContext() {

        }

       public TransactionDbContext(DbContextOptions options) : base(options){
           
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<TransactionEntity>()
                .HasOne(t=> t.Category)
                .WithMany(c => c.Transactions).HasForeignKey(t=> t.CatCode);
            modelBuilder.Entity<TransactionEntity>()
                .HasOne(t => t.MccEntity).WithMany(m => m.transactions).HasForeignKey(t => t.Mcc);
                
            modelBuilder.Entity<CategoryEntity>()
                .HasOne(c=>c.Category);
            modelBuilder.Entity<AnalyticsModel>(e => {
                e.HasNoKey();
            });
            modelBuilder.Entity<SplitTransactionEntity>(s => {
                s.HasOne(t => t.Transaction)
                .WithMany(t => t.splits);
            });
            modelBuilder.Entity<SplitTransactionEntity>(s => {
                s.HasOne(t => t.CategoryEntity).WithMany(s => s.Splits).HasForeignKey(c => c.CatCode);
            });
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    

    }

}