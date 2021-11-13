using CsvHelper.Configuration.Attributes;

namespace PmfBackend.Commands {
    
    public class CreateCategoryCommand {
        [Name("code")]
        public string Code { get; set; }
        [Name("parent-code")]
        public string ParentCode { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }
}