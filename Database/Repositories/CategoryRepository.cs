using System.Collections.Generic;
using PmfBackend.Database.Entities;
using System.Threading.Tasks;
using PmfBackend.Database;
using System;
using Microsoft.EntityFrameworkCore;
using PmfBackend.Models;
using System.Linq;


namespace PmfBackend.Database.Repositories {
    public class CategoryRepository : ICategoryRepositroy {

        private readonly TransactionDbContext _dbContext;

        public CategoryRepository(TransactionDbContext dbContext){
            _dbContext = dbContext;
        }
       public async Task<List<CategoryEntity>> saveCategories(List<CategoryEntity> categories){
           _dbContext.Categories.AsNoTracking();
            foreach(var c in categories){   
                CategoryEntity entity = _dbContext.Categories.AsNoTracking().FirstOrDefault(t => t.Code == c.Code);
                
                if( entity == null){
                    await _dbContext.AddAsync(c);
                    _dbContext.SaveChanges();
                    _dbContext.Entry<CategoryEntity>(c).State = EntityState.Detached;
               }else {
                    // _dbContext.Entry(entity).Property(p => p.Name).IsModified = true;
                    // _dbContext.Attach(entity);
                    // _dbContext.Update(entity);
                    // _dbContext.ChangeTracker.DetectChanges();
                    // _dbContext.SaveChanges();
                    // _dbContext.Entry<CategoryEntity>(entity).State = EntityState.Detached;
                    var commandSql  = "UPDATE categories SET \"Name\" = '"+c.Name+"' where \"Code\" = '"+entity.Code+"'";
                    _dbContext.Database.ExecuteSqlRaw(commandSql);
                    _dbContext.SaveChanges();

               }
             
                
           }
       
           
             
            return categories;    
        }

        public async Task<CategoryEntity> saveCategory(CategoryEntity category){
            CategoryEntity c = _dbContext.Categories.AsNoTracking().FirstOrDefault(c => c.Code == category.Code);
            if (c == null){
                     await _dbContext.AddAsync(c);
                _dbContext.SaveChanges();
                _dbContext.Entry<CategoryEntity>(c).State = EntityState.Detached;
             

                
            }else {
                  
                _dbContext.Entry(c).Property(p => p.Name).IsModified = true;
                if (c.Code == "E"){
                    Console.WriteLine("asd");
                }
                _dbContext.Update(c);
                _dbContext.ChangeTracker.DetectChanges();
                _dbContext.SaveChanges();
                _dbContext.Entry<CategoryEntity>(c).State = EntityState.Detached;
               
             //  _dbContext.ChangeTracker.DbEntityEntry<CategoryEntity> entityEntry =_dbContext.Entry(c);
                
            }
            

            return category;
        }

        
    }
}