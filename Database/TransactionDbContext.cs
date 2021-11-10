using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PmfBackend.Database.Entities;

namespace PmfBackend.Database {
    public class TransactionDbContext : DbContext {

        public DbSet<TransactionEntity> Transactions {get; set;}

        public TransactionDbContext() {

        }

       public TransactionDbContext(DbContextOptions options) : base(options){
        
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    

    }

}