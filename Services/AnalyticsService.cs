using System.Collections.Generic;
using PmfBackend.Models;
using PmfBackend.Database.Repositories;

namespace PmfBackend.Services {
    public class AnalyticsService : IAnalyticsService {

        private readonly IAnalyticsRepoository _analysticsRepository;

        public AnalyticsService(IAnalyticsRepoository analyticsRepoository){
            _analysticsRepository = analyticsRepoository;
        }

         public List<AnalyticsModel> AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,string direction=null){
             List<AnalyticsModel> list = _analysticsRepository.AnalyticsByCategory(catCode,startDate,endDate,direction);
            return list;
        }

    }
}