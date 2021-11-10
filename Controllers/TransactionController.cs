using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using PmfBackend.Services;
using PmfBackend.Commands;


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
        public IActionResult ImportData(IFormFile file){


            List<CreateTransactionCommand> transactions = _transactionService.createTransactions(file);
            return Ok(transactions);

        }
    }
}