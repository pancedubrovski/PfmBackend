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
    public class CategoryService : ICategoryService {

        List<CreateCategoryCommand> categories = new List<CreateCategoryCommand>();


        public CategoryService(){
            
        }
        public List<CreateCategoryCommand> saveCategories(IFormFile file){
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                     NewLine = Environment.NewLine,
                 };
                 using (var reader = new StreamReader(file.OpenReadStream())){
                    using (var csv = new CsvReader(reader, config)){
                        try {
                        csv.Context.RegisterClassMap<TransactionMap>();
                        
                        categories = csv.GetRecords<CreateCategoryCommand>().ToList();
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
                return categories;
        }
    }
}

