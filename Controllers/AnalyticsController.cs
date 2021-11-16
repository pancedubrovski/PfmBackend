using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using PmfBackend.Services;
using PmfBackend.Commands;
using System.Threading.Tasks;
using PmfBackend.Models;



namespace PmfBackend.Controllers {
    [ApiController]
    [Route("spending-analytics")]
    public class AnalyticsController : ControllerBase {


        private readonly ILogger<AnalyticsController> _iLogger;
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(ILogger<AnalyticsController> iLogger,IAnalyticsService analyticsService){
            _iLogger = iLogger;
            _analyticsService = analyticsService;  
        }


        [HttpGet]
        public IActionResult GetAnalistic([FromQuery] string catCode,[FromQuery(Name = "start-date")] string startDate=null,
        [FromQuery(Name = "end-date")] string endDate=null,[FromQuery] string direction=null){
            List<AnalyticsModel> list= _analyticsService.AnalyticsByCategory(catCode,startDate,endDate,direction);
            return Ok(list);
        }
        





    }    
}