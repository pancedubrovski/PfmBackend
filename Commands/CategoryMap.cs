using CsvHelper.Configuration;

namespace PmfBackend.Commands {
    public class CategoryMap  : ClassMap<CreateCategoryCommand> {

        public CategoryMap(){
            Map(m => m.Code).Name("code");
            Map(m => m.ParentCode).Name("parent-code");
            Map(m=> m.Name).Name("name");
        }   
    }
}