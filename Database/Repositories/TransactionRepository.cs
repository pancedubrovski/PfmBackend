using PmfBackend.Database.Entities;
using PmfBackend.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PmfBackend.Models;
using System.Linq;
using Npgsql;
using System.Data.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using PmfBackend.Models.Exceptions;
using PmfBackend.Models.Requests;

using System.Configuration;
using System.Collections.Specialized;

namespace PmfBackend.Database.Repositories {
    public class TransactionsRespositroy : ITransactionReposoiry {

        private readonly TransactionDbContext _dbContext;
        public TransactionsRespositroy(TransactionDbContext dbContext){
            _dbContext = dbContext;
        }
        
        public async Task<List<TransactionEntity>> saveTransaction(List<TransactionEntity> transactionEntities){
            
            
          

            foreach (var e in transactionEntities){
                if (  _dbContext.Transactions.FirstOrDefault(t => t.Id == e.Id) == null){
                      await _dbContext.AddAsync(e);
                    
                }
            }
            
            await _dbContext.SaveChangesAsync();
            return transactionEntities;
        }
         public async Task<PagedSortedList<TransactionEntity>> GetTransactions(string sortBy,
            string startDate,string endDate,Kind kind,int page =1,int pageSize =10,SortOrder sortOrder = SortOrder.Asc){
            var query = _dbContext.Transactions.AsQueryable();
           
            var list= await _dbContext.Transactions.ToListAsync();
            
           
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
           
                DateTime s = DateTime.ParseExact(converDateFromString(startDate),"MM/dd/yyyy",null);
                DateTime e = DateTime.ParseExact(converDateFromString(endDate),"MM/dd/yyyy",null);
                query = query.Where(a => a.Date.Date > s && a.Date < e);
            
            
            query = query.Where(a => a.Kind == kind);
            
             var total = query.Count();
            var totalPages = (int)Math.Ceiling(total * 1.0 / pageSize);

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortOrder == SortOrder.Desc)
                {
                    query = query.OrderByDescending(sortBy, p => p.Id);
                }
                else
                {
                    query = query.OrderBy(sortBy, p => p.Id);
                    sortOrder = SortOrder.Asc;
                }
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }
           
            
           var items = (List<TransactionEntity>) query.Include(t => t.splits).Select(t => new TransactionEntity {
                Id = t.Id,
                BeneficiaryName = t.BeneficiaryName,
                Date = t.Date,
                Direction = t.Direction,
                Description = t.Description,
                Mcc = t.Mcc,
                Kind = t.Kind,
                Currency = t.Currency,
                Amount = t.Amount,
                splits = t.splits
                 
            }).ToList();
            
            return new PagedSortedList<TransactionEntity>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = items
            };
        }

        public async Task<ErrorMessage> SaveCategoryOnTransaction(string transactionId,CategorizeTransactionRequest request){
            TransactionEntity transactionEntity = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            CategoryEntity categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Code == request.catCode);
            if(transactionEntity == null){
                return new ErrorMessage {
                    tag = " transaction entity",
                    error = " don't exist transaction entity",
                    message = " can't find in database"
                };
            }
            if(categoryEntity == null){
                return new ErrorMessage {
                      tag = " Category Entity",
                      error = "don't exist category entity",
                      message = "can't find in database "
                  };
            }
            transactionEntity.CatCode = categoryEntity.Code;
            transactionEntity.Category = categoryEntity;
            _dbContext.Attach(transactionEntity).State = EntityState.Modified; 
            await _dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<ErrorMessage> SplitTransactionByCategory(string transactionId,SplitTransactionRequest request){

           
            var transactions = _dbContext.SplitTransactionEntities.Where(t => EF.Property<string>(t, "TransactionId") == transactionId);
            foreach (var t in transactions)
            {
                _dbContext.SplitTransactionEntities.Remove(t);
            }



            TransactionEntity transactionEntity =  _dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);
            if(transactionEntity == null) {
                  return new ErrorMessage {
                      tag = " transaction entity",
                      error = " don't exist transaction entity",
                      message = " can't find in database"
                  };
            }
            double splitAmounts = request.splits.Sum(s =>s.Amount);
            if (splitAmounts != transactionEntity.Amount) {
                return new ErrorMessage {
                      tag = " Amount",
                      error = "Amount is not same with trasncation entity",
                      message = "Amount is not same with trasncation entity "
                  };
            }
            foreach(var s in request.splits){

               
                CategoryEntity categoryEntity =  _dbContext.Categories.FirstOrDefault(c => c.Code == s.CatCode);
                if (categoryEntity == null) {
                    return new ErrorMessage {
                      tag = " Category Entity",
                      error = "don't exist category entity",
                      message = "can't find in database "
                  };
                }
                SplitTransactionEntity st = new SplitTransactionEntity {
                    Transaction = transactionEntity,
                    CategoryEntity = categoryEntity,
                    Amount = s.Amount
                };
                _dbContext.Add(st);
            }
            transactionEntity.IsSplit = true;
            _dbContext.Attach(transactionEntity).State = EntityState.Modified; 
             _dbContext.SaveChanges();
            
            return null;
        }

        public async Task<List<MccEntity>> SaveMccCodes(List<MccEntity> mccEntities){

            foreach (var m in mccEntities){
            MccEntity entity = _dbContext.MccCodes.AsNoTracking().FirstOrDefault(t => t.Code == m.Code);
                
                if( entity == null){
                    await _dbContext.AddAsync(m);
                    _dbContext.SaveChanges();
                    _dbContext.Entry<MccEntity>(m).State = EntityState.Detached;
                }
                else {
                     _dbContext.Update(m);
                     _dbContext.ChangeTracker.DetectChanges();
                     _dbContext.SaveChanges();
                     _dbContext.Entry<MccEntity>(m).State = EntityState.Detached;
                }
            }
            return mccEntities;
        }
         public string converDateFromString(string input){
           
           
            string[] dateString = input.Split('/');
            if (dateString.Length != 3 ){
                return dateString.ToString();
            }
            if (dateString[0].Length == 1 && dateString[1].Length == 1){
                return '0'+dateString[0]+"/"+'0'+dateString[1]+"/"+dateString[2];
            }
            else if (dateString[0].Length == 1) {  
                return ('0'+dateString[0]+"/"+dateString[1]+"/"+dateString[2]);
            }
            else if (dateString[1].Length == 1){
                return (dateString[0]+"/"+'0'+dateString[1]+"/"+dateString[2]);
            }
            return (dateString[0]+"/"+dateString[1]+"/"+dateString[2]);
        }
        public void AutoCategorize(){
            var all = ConfigurationManager.AppSettings;
            foreach(var k in all.AllKeys){
                Console.WriteLine(k +" "+ all.Get(k));
            }
            
            
            List<TransactionEntity> transactions = _dbContext.Transactions.ToList();
            Dictionary<string,string> MccMap = new Dictionary<string,string>();
            MccMap.Add("8011","45");
            MccMap.Add("5499","E");
            MccMap.Add("5200","56");
            MccMap.Add("7711","104");
            MccMap.Add("6300","33");
            MccMap.Add("5192","23");
            MccMap.Add("5912","49");
            MccMap.Add("7519","105");
            MccMap.Add("5941","92");
            MccMap.Add("5655","50");
            MccMap.Add("9311","R");
            Dictionary<string,string> dMap = new Dictionary<string,string>();
            dMap.Add("Allowance","67");
            dMap.Add("Book","88");
            dMap.Add("Books","88");
            dMap.Add("ATM","25");
            dMap.Add("Baby","68");
            dMap.Add("Onlie banking fee","26");
            dMap.Add("Food delivery","E");
            dMap.Add("Supermaket shopping","E");
            dMap.Add("Mobile Phone Bill", "11");
            dMap.Add("Internet bill","11");
            dMap.Add("Parking fee","5");

            foreach(var t in transactions){
                if (t.Category == null && t.IsSplit == false){
                    if(!string.IsNullOrWhiteSpace(t.Mcc) && MccMap.Keys.Contains(t.Mcc)){
                        CategoryEntity categoryEntity = _dbContext.Categories.FirstOrDefault(c => c.Code == MccMap[t.Mcc]);
                        t.Category = categoryEntity;   
                        _dbContext.SaveChanges(); 
                    }
                    if(dMap.Keys.Contains(t.Description)){
                        CategoryEntity categoryEntity = _dbContext.Categories.FirstOrDefault(c =>c.Code == dMap[t.Description]);
                        t.Category = categoryEntity;
                        _dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}