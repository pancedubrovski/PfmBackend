using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Globalization;
using CsvHelper;
using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System.Linq;
using PmfBackend.Database.Repositories;
using AutoMapper;
using PmfBackend.Database.Entities;
using System.Threading.Tasks;
using PmfBackend.Models;
using TinyCsvParser;
using TinyCsvParser.Mapping;




namespace PmfBackend.Services {
    public class TransactionSerive : ITransactionService {

        private readonly ITransactionReposoiry _transactionReposoiry;
        private readonly IMapper _mapper;

        public TransactionSerive(ITransactionReposoiry transactionReposoiry,IMapper mapper){
                _transactionReposoiry = transactionReposoiry;
                _mapper = mapper;
        }

        public async Task<List<CreateTransactionCommand>> createTransactions(IFormFile file){
            List<CreateTransactionCommand> transactions = new List<CreateTransactionCommand>();
            

             var reader = new StreamReader(file.OpenReadStream());
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            TransactionMap csvMapper = new TransactionMap();
             CsvParser<CreateTransactionCommand> csvParser = new CsvParser<CreateTransactionCommand>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();
            for (int i = 0; i < result.Count; i++)
            {
                CreateTransactionCommand dataForDb = new CreateTransactionCommand
                {
                    Id = result[i].Result.Id,
                    BeneficiaryName = result[i].Result.BeneficiaryName,
                    Date = result[i].Result.Date,
                    Direction = result[i].Result.Direction,
                    Amount = result[i].Result.Amount,
                    Description = result[i].Result.Description,
                    Currency = result[i].Result.Currency,
                    Kind = result[i].Result.Kind,
                    Mcc = result[i].Result.Mcc
                };
                transactions.Add(dataForDb);
            }
             List<TransactionEntity> transactionEntities = new List<TransactionEntity>();
            foreach (var e in transactions) {
                if (!string.IsNullOrEmpty(e.Id)){
                    TransactionEntity transaction = _mapper.Map<TransactionEntity>(e);   
                transactionEntities.Add(transaction);
                }
            }
            
          
            await _transactionReposoiry.saveTransaction(transactionEntities);
            return transactions;
        }
        public async Task<PagedSortedList<TransactionEntity>> GetTransactions(int page =1,int pageSize =10,string sortBy=null,
        SortOrder sortOrder = SortOrder.Asc,string startDate= null,string endDate= null,string kind=null){
            PagedSortedList<TransactionEntity> list = await _transactionReposoiry.GetTransactions(page,pageSize,sortBy,sortOrder,startDate,endDate,kind);
          
            return list;
        }

        public async Task<TransactionEntity> SaveCateoryOnTransactin(string transactionId,CategorizeTransactionRequest request){
            return await _transactionReposoiry.SaveCategoryOnTransaction(transactionId,request);
        }

    }
}