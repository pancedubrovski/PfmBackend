using PmfBackend.Models;
using System.Collections.Generic;

namespace PmfBackend.Database.Repositories {
    public interface IAnalyticsRepoository {
        

        public List<AnalyticsModel> AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,string direction=null);
        
    }
}