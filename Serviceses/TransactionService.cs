using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Globalization;
using CsvHelper;
using System;
using CsvHelper.Configuration;
using System.Linq;

namespace PmfBackend.Services {
    public class TransactionSerive : ITransactionService {

        public List<CreateTransactionCommand> createTransactions(IFormFile file){
            List<CreateTransactionCommand> transactions = new List<CreateTransactionCommand>();

            if(file.FileName.EndsWith(".csv")){
               
                 var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                 {
                     NewLine = Environment.NewLine,
                 };
                 using (var reader = new StreamReader(file.OpenReadStream())){
                    using (var csv = new CsvReader(reader, config)){
                        transactions = csv.GetRecords<CreateTransactionCommand>().ToList();
                    }
                 }
            }
            
            return transactions;
        }

    }
}