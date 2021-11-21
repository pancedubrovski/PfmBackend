using PmfBackend.Commands;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using PmfBackend.Database.Repositories;
using AutoMapper;
using PmfBackend.Database.Entities;
using System.Threading.Tasks;
using PmfBackend.Models;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using PmfBackend.Models.Exceptions;
using PmfBackend.Models.Requests;




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
            ErrorList errorLists = new ErrorList();
            for (int i = 0; i < result.Count; i++)
            {
                string mccValue = null;
                if(!string.IsNullOrWhiteSpace(result[i].Result.Mcc)){
                    mccValue = result[i].Result.Mcc;
                }
                CreateTransactionCommand transaction = new CreateTransactionCommand
                {
                    Id = result[i].Result.Id,
                    BeneficiaryName = result[i].Result.BeneficiaryName,
                    Date = result[i].Result.Date,
                    Direction = result[i].Result.Direction,
                    Amount = result[i].Result.Amount,
                    Description = result[i].Result.Description,
                    Currency = result[i].Result.Currency,
                    Kind = result[i].Result.Kind,
                    Mcc = mccValue
                };
                ErrorMessage error = ValidateTransaction(transaction);
                if (error!= null){
                    errorLists.errors.Add(error);
                }
                transactions.Add(transaction);
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
        private ErrorMessage ValidateTransaction(CreateTransactionCommand transaction){
            if(transaction.Currency.Length != 3){
                return new ErrorMessage {
                    tag = "Currency", 
                    error= "Currency must be  3 length",
                    message = "Currency must be  3 length"
                };
            }
            return null;
        }
         public async Task<PagedSortedList<TransactionEntity>> GetTransactions(string startDate,string endDate,
         Kind kind,string sortBy,int page =1,int pageSize =10,SortOrder sortOrder = SortOrder.Asc){
            PagedSortedList<TransactionEntity> list = await _transactionReposoiry.GetTransactions(sortBy,startDate,endDate,kind,page,pageSize,sortOrder);
          
            return list;
        }

        public async Task<ErrorMessage> SaveCateoryOnTransactin(string transactionId,CategorizeTransactionRequest request){
            return await _transactionReposoiry.SaveCategoryOnTransaction(transactionId,request);
        }

        public async Task<ErrorMessage> SplitTransactinByCategory(string transactionId,SplitTransactionRequest request){
            return await _transactionReposoiry.SplitTransactionByCategory(transactionId,request);
        }

        public async Task<List<MccEntity>> SaveMccCodes(IFormFile file){
            List<MccEntity> mccEntities = new List<MccEntity>();
            var reader = new StreamReader(file.OpenReadStream());
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            MccMap csvMapper = new MccMap();
             CsvParser<CreateMccCommand> csvParser = new CsvParser<CreateMccCommand>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();
            for (int i = 0; i < result.Count; i++)
            {
                MccEntity dataForDb = new MccEntity
                {
                   Code = result[i].Result.Code,
                   MerchactType = result[i].Result.MerchactType
                };
                mccEntities.Add(dataForDb);
            }
            
            return await _transactionReposoiry.SaveMccCodes(mccEntities);
        }

        public void AutoCategorize(){
            _transactionReposoiry.AutoCategorize();
        }
    }
}