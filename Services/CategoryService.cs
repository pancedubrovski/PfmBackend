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

namespace PmfBackend.Services {
    public class CategoryService : ICategoryService {

        
      
        private readonly IMapper _mapper;
        private readonly ICategoryRepositroy _categoryRepository;


        public CategoryService(IMapper mapper,ICategoryRepositroy categoryRepository){
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CreateCategoryCommand>> saveCategories(IFormFile file){
            List<CreateCategoryCommand> categories = new List<CreateCategoryCommand>();
            var reader = new StreamReader(file.OpenReadStream());
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            CategoryMap csvMapper = new CategoryMap();
             CsvParser<CreateCategoryCommand> csvParser = new CsvParser<CreateCategoryCommand>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();
            for (int i = 0; i < result.Count; i++){
                CreateCategoryCommand c = new CreateCategoryCommand {
                    Code = result[i].Result.Code,
                    ParentCode = result[i].Result.ParentCode,
                    Name = result[i].Result.Name
                };
                categories.Add(c);
            }
            
            List<CategoryEntity> list = new List<CategoryEntity>();
            foreach(var c in categories){
                CategoryEntity entity = new CategoryEntity {
                    Code = c.Code,
                    ParentCode = c.ParentCode,
                    Name = c.Name
                };
                list.Add(entity);
            }
                await _categoryRepository.saveCategories(list);
                return categories;
        }
        public async Task<List<CategoryEntity>> GetCaetegories(string ParentCode=null){
            
            return await _categoryRepository.GetCaetegories(ParentCode);
        }   

    }
}

