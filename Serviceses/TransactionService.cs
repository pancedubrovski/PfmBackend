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
            
            return transactions;
        }

    }
}