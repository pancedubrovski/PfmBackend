
using System.Text.Json;

namespace PmfBackend.Models.Exceptions {

    public class ErrorMessage {
        public string tag { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        public override string ToString(){
            return  " { \n" +
               " \"errors\": { \n"+
                "\"tag\": "+ "\""+tag+"\"" + "\n"+
               " \"error\": "+"\""+ error+"\"" + "\n"+
               " \"message\": " +"\""+ message+"\"" +"\n" + 
            "} \n"+
        "}";
        }
    }
}