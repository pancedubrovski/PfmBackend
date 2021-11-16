using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PmfBackend.Database.Entities;
using PmfBackend.Models;

namespace PmfBackend.Database {
    public class TransactionDbContext : DbContext {

        public DbSet<TransactionEntity> Transactions {get; set;}
        public DbSet<CategoryEntity> Categories {get; set;}
        public DbSet<AnalyticsModel> AnalyticsModels { get; set; }

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

            modelBuilder.Entity<CategoryEntity>()
                .HasOne(c=>c.Category);
            modelBuilder.Entity<AnalyticsModel>(e => {
                e.HasNoKey();
            });
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    

    }

}