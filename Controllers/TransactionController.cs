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
using PmfBackend.Models.Exceptions;
using PmfBackend.Models.Requests;
using Newtonsoft.Json;



namespace PmfBackend.Controllers {

    [ApiController]
    [Route("transactions")]
    public class TranscationController : ControllerBase
    {
       

        private readonly ILogger<TranscationController> _logger;
        private readonly ITransactionService _transactionService;

        public TranscationController(ILogger<TranscationController> logger,ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

          

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportData(IFormFile file=null){

            if (file == null){
                var error = new ErrorMessage {
                    tag = "Csv File",
                    error = "Not found",
                    message = "Csv file not found"
                };
                return BadRequest(error.ToString());
            }
            List<CreateTransactionCommand> transactions = await _transactionService.createTransactions(file);
            return Ok(transactions);

        }
        [HttpGet]
        public async Task<IActionResult> getTransaction([FromQuery(Name = "page")] int? page,[FromQuery(Name = "pageSize")] int? pageSize,[FromQuery(Name = "sortBy")] string sortBy,
        [FromQuery(Name = "sortOrder")] SortOrder sortOrder,[FromQuery(Name = "start-date")] string startDate,[FromQuery(Name = "end-date")] string endDate,[FromQuery(Name = "kind")] Kind kind){
           
            
            page ??= 1;
            pageSize ??= 10;
            var list = await _transactionService.GetTransactions(startDate,endDate,kind,sortBy,page.Value,pageSize.Value,sortOrder);

            string json = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }
        [HttpPost]
        [Route("/transaction/{transcationId}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] string transcationId,[FromBody] CategorizeTransactionRequest catCode){
            var error = await _transactionService.SaveCateoryOnTransactin(transcationId,catCode);
            if(error!= null){
                return BadRequest(error.ToString());
            }
            return Ok("ransaction splitted");
        }
        [HttpPost]
        [Route("/transaction/{id}/split")]
        public async Task<IActionResult> SplitTransactionByCategory([FromRoute] string id,[FromBody] SplitTransactionRequest requst){
            var error = await _transactionService.SplitTransactinByCategory(id,requst);
            if (error != null){

                return BadRequest(error.ToString());
            }
            return Ok("Transaction splitted");    
        }
        [HttpPost]
        [Route("mmc/import")]
        public async Task<IActionResult> ImportMccCodes(IFormFile file){
            if (!file.FileName.EndsWith(".csv")){
                return NotFound();
            }
            var list = await _transactionService.SaveMccCodes(file);
            return Ok(list);
        }
        [HttpPost]
        [Route("auto-categorize")]
        public IActionResult AutoCategorize(){
            _transactionService.AutoCategorize();
            return Ok();
        }
    }
}