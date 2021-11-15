using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using PmfBackend.Commands;
using System.Threading.Tasks;


namespace PmfBackend.Services {
    public interface ICategoryService {
        public  Task<List<CreateCategoryCommand>> saveCategories(IFormFile file);
    }
}