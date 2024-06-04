using System;
using Microsoft.AspNetCore.Mvc;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("import/csv")]
        public IActionResult ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                _dataService.ImportCsv(stream);
            }

            return Ok();
        }

        [HttpGet("export/csv")]
        public IActionResult ExportCsv()
        {
            var csvData = _dataService.ExportCsv();
            return File(new System.Text.UTF8Encoding().GetBytes(csvData), "text/csv", "expenses.csv");
        }
    }
}