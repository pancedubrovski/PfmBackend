using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
namespace PmfBackend.Services {
    public interface ITransactionService  {
        public List<CreateTransactionCommand> createTransactions(IFormFile file);
    }
}