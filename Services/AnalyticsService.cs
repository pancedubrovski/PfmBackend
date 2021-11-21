using System.Collections.Generic;
using PmfBackend.Models;
using PmfBackend.Database.Repositories;

namespace PmfBackend.Services {
    public class AnalyticsService : IAnalyticsService {

        private readonly IAnalyticsRepoository _analysticsRepository;

        public AnalyticsService(IAnalyticsRepoository analyticsRepoository){
            _analysticsRepository = analyticsRepoository;
        }

         public AnalyticsGroupModel AnalyticsByCategory(string catCode,string startDate=null,string endDate=null,Direction? direction=null){
            var list = _analysticsRepository.AnalyticsByCategory(catCode,startDate,endDate,direction);
            return list;
        }

    }
}