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

namespace PmfBackend.Database.Repositories {
    public class TransactionsRespositroy : ITransactionReposoiry {

        private readonly TransactionDbContext _dbContext;
        public TransactionsRespositroy(TransactionDbContext dbContext){
            _dbContext = dbContext;
        }
        
        public async Task<List<TransactionEntity>> saveTransaction(List<TransactionEntity> transactionEntities){
            
            
            //await _dbContext.AddAsync(transactionEntities[0]);

            foreach (var e in transactionEntities){
                if (  _dbContext.Transactions.FirstOrDefault(t => t.Id == e.Id) == null){
                    await _dbContext.AddAsync(e);
                }
            }
            
            await _dbContext.SaveChangesAsync();
            return transactionEntities;
        }
        public async Task<PagedSortedList<TransactionEntity>> GetTransactions(int page =1,int pageSize =10,string sortBy=null,
        SortOrder sortOrder = SortOrder.Asc,string startDate=null,string endDate=null,string kind = null){
            var query = _dbContext.Transactions.AsQueryable();
           
            var list= await _dbContext.Transactions.ToListAsync();
            
           
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            if (startDate != null  && endDate != null){ 
                DateTime s = DateTime.ParseExact(converDateFromString(startDate),"MM/dd/yyyy",null);
                DateTime e = DateTime.ParseExact(converDateFromString(endDate),"MM/dd/yyyy",null);
                query = query.Where(a => a.Date.Date > s && a.Date < e);
            }
            if(kind != null){
                query = query.Where(a => a.Kind == kind);
            }
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
           
          //  query = query.Where(a=> SqlFunctions.DateDiff("DAY", a.Date, startDate) > 0);
            List<TransactionEntity> items = await query.ToListAsync();
            
            
            // var queryList = from t in items
            // where t.Date > startDate && t.Date < endDate select t;
            
            return new PagedSortedList<TransactionEntity>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = items,
            };
        }

        public async Task<TransactionEntity> SaveCategoryOnTransaction(string transactionId,CategorizeTransactionRequest request){
            TransactionEntity transactionEntity = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            CategoryEntity categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Code == request.catCode);
            if(transactionEntity == null){
                throw new NotFoundTransactionException(System.Net.HttpStatusCode.NotFound,"transaction  not exist");
            }
            if(categoryEntity == null){
                throw new NotFoundTransactionException(System.Net.HttpStatusCode.NotFound,"category not exist");
            }
            transactionEntity.CatCode = categoryEntity.Code;
            transactionEntity.Category = categoryEntity;
            _dbContext.Attach(transactionEntity).State = EntityState.Modified; 
            await _dbContext.SaveChangesAsync();
            return transactionEntity;
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



    }
}