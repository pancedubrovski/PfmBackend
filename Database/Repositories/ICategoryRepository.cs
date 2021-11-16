using System.Threading.Tasks;
using PmfBackend.Database.Entities;
using System.Collections.Generic;
namespace PmfBackend.Database.Repositories {
    
    public interface ICategoryRepositroy {

        public Task<List<CategoryEntity>> saveCategories(List<CategoryEntity> categories);

        public Task<CategoryEntity> saveCategory(CategoryEntity category);

        public Task<List<CategoryEntity>> GetCaetegories(string ParentCode);

    }   
  

}