using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using PmfBackend.Commands;


namespace PmfBackend.Services {
    public interface ICategoryService {
        public List<CreateCategoryCommand> saveCategories(IFormFile file);
    }
}