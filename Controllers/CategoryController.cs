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
        public async Task<IActionResult> ImportCategories(IFormFile file){
            if (!file.FileName.EndsWith(".csv")){
                return NotFound();
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