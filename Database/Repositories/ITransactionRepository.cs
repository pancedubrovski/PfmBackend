using PmfBackend.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PmfBackend.Models;
using System;

namespace PmfBackend.Database.Repositories {

    public interface ITransactionReposoiry {

        public Task<List<TransactionEntity>> saveTransaction(List<TransactionEntity> transactionEntities);

        public Task<PagedSortedList<TransactionEntity>> GetTransactions(int page =1,int pageSize =10,string sortBy=null,
        SortOrder sortOrder = SortOrder.Asc,string startDate=null,string endTime= null,string kind=null);

        public Task<TransactionEntity> SaveCategoryOnTransaction(string transactionId,CategorizeTransactionRequest request);

    }
}