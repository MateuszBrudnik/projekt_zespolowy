using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult GetFilteredReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string category)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetExpensesAsync(userId).Result.ToList();

            if (startDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date <= endDate.Value).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                expenses = expenses.Where(e => e.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(expenses);
        }

        [HttpGet("pdf")]
        public IActionResult GetPdfReport()
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetExpensesByDateRangeAsync(userId, DateTime.Now.AddMonths(-1), DateTime.Now).Result.ToList();
            var htmlContent = GenerateHtmlReport(expenses);
            var pdfBytes = _pdfService.GenerateReportPdf(htmlContent);
            return File(pdfBytes, "application/pdf", "Report.pdf");
        }

        [HttpGet("csv")]
        public IActionResult GetCsvReport()
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetExpensesByDateRangeAsync(userId, DateTime.Now.AddMonths(-1), DateTime.Now).Result.ToList();
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
