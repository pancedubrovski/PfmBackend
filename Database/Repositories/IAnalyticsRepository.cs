using PmfBackend.Models;
using System.Collections.Generic;

namespace PmfBackend.Database.Repositories {
    public interface IAnalyticsRepoository {
        

        public AnalyticsGroupModel AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,Direction? direction=null);
        
    }
}