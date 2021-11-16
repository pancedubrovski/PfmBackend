using CsvHelper.Configuration.Attributes;

namespace PmfBackend.Commands {
    
    public class CreateCategoryCommand {
       
        public string Code { get; set; }
        
        public string ParentCode { get; set; }
        
        public string Name { get; set; }
    }
}