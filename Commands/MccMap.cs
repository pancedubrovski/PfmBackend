using TinyCsvParser.Mapping;


namespace PmfBackend.Commands {
    public class MccMap : CsvMapping<CreateMccCommand> {

        public MccMap() : base() {
            MapProperty(0,m => m.Code);
            MapProperty(1,m => m.MerchactType);
        }
    }   
}