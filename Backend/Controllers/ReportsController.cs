using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly PdfService _pdfService;

        public ReportsController(IExpenseService expenseService, PdfService pdfService)
        {
            _expenseService = expenseService;
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId ,startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
            return Ok(expenses);
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> GetPdfReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId ,startDate ?? DateTime.Now.AddMonths(-1), endDate ?? DateTime.Now);
            var htmlContent = GenerateHtmlReport(expenses);
            var pdfBytes = _pdfService.GeneratePdf(htmlContent);
            return File(pdfBytes, "application/pdf", "Report.pdf");
        }

        [HttpGet("csv")]
        public async Task<IActionResult> GetCsvReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate ?? DateTime.Now.AddMonths(-1), endDate ?? DateTime.Now);
            var csvContent = GenerateCsvReport(expenses);
            var csvBytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(csvBytes, "text/csv", "Report.csv");
        }

        private string GenerateHtmlReport(IEnumerable<Expense> expenses)
        {
            var html = "<html><body><h1>Monthly Expense Report</h1><ul>";
            foreach (var expense in expenses)
            {
                html += $"<li>{expense.Name}: {expense.Amount:C} on {expense.Date.ToShortDateString()}</li>";
            }
            html += "</ul></body></html>";
            return html;
        }

        private string GenerateCsvReport(IEnumerable<Expense> expenses)
        {
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Name,Amount,Date,Category");
            foreach (var expense in expenses)
            {
                csv.AppendLine($"{expense.Name},{expense.Amount},{expense.Date.ToShortDateString()},{expense.Category.Name}");
            }
            return csv.ToString();
        }
    }
}
