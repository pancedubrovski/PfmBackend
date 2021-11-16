using TinyCsvParser.Mapping;

namespace PmfBackend.Commands {
    public class CategoryMap  : CsvMapping<CreateCategoryCommand> {

        public CategoryMap(): base(){
            MapProperty(0,c =>c.Code);
            MapProperty(1,c=>c.ParentCode);
            MapProperty(2,c=>c.Name);
        }   
    }
}