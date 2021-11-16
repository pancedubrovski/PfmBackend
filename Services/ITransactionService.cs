using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using PmfBackend.Models;
using PmfBackend.Database.Entities;
using System;
namespace PmfBackend.Services {
    public interface ITransactionService  {
        public Task<List<CreateTransactionCommand>> createTransactions(IFormFile file);

        public Task<PagedSortedList<TransactionEntity>> GetTransactions(int page =1,int pageSize =10,string sortBy=null,
        SortOrder sortOrder = SortOrder.Asc,string startDate= null,string endDate=null,string kind=null);

        public Task<TransactionEntity> SaveCateoryOnTransactin(string transactionId,CategorizeTransactionRequest catCode);
    }
}