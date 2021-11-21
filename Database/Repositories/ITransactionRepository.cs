using PmfBackend.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PmfBackend.Models;
using PmfBackend.Models.Requests;
using System;
using PmfBackend.Models.Exceptions;

namespace PmfBackend.Database.Repositories {

    public interface ITransactionReposoiry {

        public Task<List<TransactionEntity>> saveTransaction(List<TransactionEntity> transactionEntities);

        public Task<PagedSortedList<TransactionEntity>> GetTransactions(string sortBy,
        string startDate,string endDate,Kind kind,int page =1,int pageSize =10,SortOrder sortOrder = SortOrder.Asc);

        public Task<ErrorMessage> SaveCategoryOnTransaction(string transactionId,CategorizeTransactionRequest request);

        public Task<ErrorMessage> SplitTransactionByCategory(string transactionId,SplitTransactionRequest request);


        public Task<List<MccEntity>> SaveMccCodes(List<MccEntity> mccEntities);

        public int AutoCategorize();

    }
}