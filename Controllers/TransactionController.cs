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
        public async Task<IActionResult> ImportData(IFormFile file){


            List<CreateTransactionCommand> transactions = await _transactionService.createTransactions(file);
            return Ok(transactions);

        }
        [HttpGet]
        public async Task<IActionResult> getTransaction([FromQuery(Name = "page")] int? page,[FromQuery(Name = "pageSize")] int? pageSize,[FromQuery(Name = "sortBy")] string sortBy,
        [FromQuery(Name = "sortOrder")] SortOrder sortOrder,[FromQuery(Name = "start-date")] string startDate,[FromQuery(Name = "end-date")] string endDate,[FromQuery(Name = "kind")] string kind=null){
            Console.WriteLine(startDate+"alo");
            Console.WriteLine(endDate);
            page ??= 1;
            pageSize ??= 10;
            var list = await _transactionService.GetTransactions(page.Value,pageSize.Value,sortBy,sortOrder,startDate,endDate,kind);
            return Ok(list);
        }
        [HttpPost]
        [Route("{transcationId}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] string transcationId,[FromBody] CategorizeTransactionRequest catCode){
            try {
                await _transactionService.SaveCateoryOnTransactin(transcationId,catCode);

            } catch (NotFoundTransactionException e){
                return NotFound(e.Message);
            }
            return Ok(catCode.catCode);
        }
    }
}