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

namespace PmfBackend.Controllers{
    

    [ApiController]
    [Route("categories")]
    public class CategoryController :  ControllerBase
    {
       

        private readonly ILogger<TranscationController> _logger;
        private readonly ICategoryService _categoryService;
        

        public CategoryController(ILogger<TranscationController> logger,ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportCategories(IFormFile file=null){
           
             
            if (file == null){
                var error = new ErrorMessage {
                    tag = "Csv File",
                    error = "Not found",
                    message = "Csv file not found"
                };
                return BadRequest(error.ToString());
            }
            var categories = await _categoryService.saveCategories(file);
            return Ok(categories);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoies([FromQuery(Name = "parent-id")] string ParentCode=null){

            var categories = await _categoryService.GetCaetegories(ParentCode);
            return Ok(categories);
        }
        
    }   
}