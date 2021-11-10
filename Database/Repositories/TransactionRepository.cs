using PmfBackend.Database.Entities;
using PmfBackend.Database;
using System.Collections.Generic;

namespace PmfBackend.Database.Repositories {
    public class TransactionsRespositroy : ITransactionReposoiry {

        private readonly TransactionDbContext _dbContext;
        public TransactionsRespositroy(TransactionDbContext dbContext){
            _dbContext = dbContext;
        }
        
        public List<TransactionEntity> saveTransaction(TransactionEntity transactionEntity){
            return new List<TransactionEntity>();
        }


    }
}