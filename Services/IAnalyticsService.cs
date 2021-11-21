using PmfBackend.Models;
using System.Collections.Generic;


namespace PmfBackend.Services {
    public interface IAnalyticsService {
        public AnalyticsGroupModel AnalyticsByCategory(string catCode,string startDate,string endDate,Direction? direction);
    }
}