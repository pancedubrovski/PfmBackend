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

        
      
        private readonly IMapper _mapper;
        private readonly ICategoryRepositroy _categoryRepository;


        public CategoryService(IMapper mapper,ICategoryRepositroy categoryRepository){
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CreateCategoryCommand>> saveCategories(IFormFile file){
            List<CreateCategoryCommand> categories = new List<CreateCategoryCommand>();
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
                // var categoriesEnitiy =  _mapper.Map<List<CategoryEntity>>(categories);
                List<CategoryEntity> categoriesEnitiy = new List<CategoryEntity>();
                foreach(var c in categories){
                    CategoryEntity entity = new CategoryEntity {
                        Code = c.Code,
                        ParentCode = c.ParentCode,
                        Name = c.Name
                    };
                    categoriesEnitiy.Add(entity);
                }
                await _categoryRepository.saveCategories(categoriesEnitiy);
                return categories;
        }
    }
}

