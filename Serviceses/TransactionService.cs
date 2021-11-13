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

            if(file.FileName.EndsWith(".csv")){
               
                 var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                 {
                     NewLine = Environment.NewLine,
                 };
                 using (var reader = new StreamReader(file.OpenReadStream())){
                    using (var csv = new CsvReader(reader, config)){
                        try {
                        csv.Context.RegisterClassMap<TransactionMap>();
                        
                        transactions = csv.GetRecords<CreateTransactionCommand>().ToList();
                        } 
                        catch(UnauthorizedAccessException e){
                            throw new Exception(e.Message);
                        }
                        catch (FieldValidationException e){
                            throw new Exception(e.Message);
                        }
                        catch (CsvHelperException e){
                            throw new Exception(e.Message);
                        }
                        catch (Exception e){
                            throw new Exception(e.Message);
                        }
                    }
                 }
            }
            List<TransactionEntity> transactionEntities = new List<TransactionEntity>();
            foreach (var e in transactions) {
                if (!string.IsNullOrEmpty(e.Id)){
                    TransactionEntity transaction = _mapper.Map<TransactionEntity>(e);
        
                    // transaction = new TransactionEntity {
                    //     Id = e.Id,
                    //     Amount = e.Amount,
                    //     BeneficiaryName = e.BeneficiaryName,
                    //     Currency = e.Currency,
                    //     Mcc = e.Mcc,
                    //     Kind = e.Kind,
                    //     Date = DateTime.Parse(e.Date),
                    //     Description = e.Description,
                    //     Direction = e.Direction
                   // };
        
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

    }
}