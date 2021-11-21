using System.Collections.Generic;
using System.Text.Json;

namespace PmfBackend.Models.Exceptions {
    public class ErrorList {
        public List<ErrorMessage> errors { get; set; }

        public override string ToString(){
            return JsonSerializer.Serialize(errors);
        }
    }
    
}