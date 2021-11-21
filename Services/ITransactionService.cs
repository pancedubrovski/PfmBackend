using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using PmfBackend.Models;
using PmfBackend.Database.Entities;
using PmfBackend.Models.Requests;
using PmfBackend.Models.Exceptions;

namespace PmfBackend.Services {
    public interface ITransactionService  {
        public Task<List<CreateTransactionCommand>> createTransactions(IFormFile file);

        public Task<PagedSortedList<TransactionEntity>> GetTransactions(string startDate,string endDate,Kind kind,string sortBy,int page =1,int pageSize =10,
        SortOrder sortOrder = SortOrder.Asc);

        public Task<ErrorMessage> SaveCateoryOnTransactin(string transactionId,CategorizeTransactionRequest catCode);

        public Task<ErrorMessage> SplitTransactinByCategory(string transactionId,SplitTransactionRequest request);

        public Task<List<MccEntity>> SaveMccCodes(IFormFile file);

        public int AutoCategorize();
    }
}