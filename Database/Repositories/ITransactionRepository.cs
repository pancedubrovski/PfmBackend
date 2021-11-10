using PmfBackend.Database.Entities;
using System.Collections.Generic;

namespace PmfBackend.Database.Repositories {

    public interface ITransactionReposoiry {

        public List<TransactionEntity> saveTransaction(TransactionEntity transactionEntity);

    }
}