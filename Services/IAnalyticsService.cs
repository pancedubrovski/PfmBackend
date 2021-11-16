using PmfBackend.Models;
using System.Collections.Generic;


namespace PmfBackend.Services {
    public interface IAnalyticsService {
        public List<AnalyticsModel> AnalyticsByCategory(string catCode,string startDate,string endDate,string direction);
    }
}