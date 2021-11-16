using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using PmfBackend.Commands;
using System.Threading.Tasks;
using PmfBackend.Database.Entities;

namespace PmfBackend.Services {
    public interface ICategoryService {
        public  Task<List<CreateCategoryCommand>> saveCategories(IFormFile file);

        public Task<List<CategoryEntity>> GetCaetegories(string ParentCode);
    }
}