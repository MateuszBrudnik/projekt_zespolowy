using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Migrations;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IIncomeService _incomeService;
        private readonly PdfService _pdfService;
        private readonly IReportService _reportService;

        public ReportsController(IExpenseService expenseService, PdfService pdfService, IReportService reportService, IIncomeService incomeService)
        {
            _expenseService = expenseService;
            _pdfService = pdfService;
            _reportService = reportService;
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId ,startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
            return Ok(expenses);
        }

        [HttpGet("1")]
        public async Task<IActionResult> GetReports1([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomes = await _incomeService.GetIncomesByDateRangeAsync(userId, startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
            return Ok(incomes);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var totalExpenses = await _reportService.GetTotalExpensesAsync(userId, startDate, endDate);
            var totalIncomes = await _reportService.GetTotalIncomesAsync(userId, startDate, endDate);

            return Ok(new { TotalExpenses = totalExpenses, TotalIncomes = totalIncomes });
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
            var incomes = await _incomeService.GetIncomesByDateRangeAsync(userId, startDate ?? DateTime.Now.AddMonths(-1), endDate ?? DateTime.Now);
            var csvContent = GenerateCsvReport(expenses, incomes);
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

        private string GenerateCsvReport(IEnumerable<Expense> expenses, IEnumerable<Income> incomes)
        {
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Nazwa;Kwota;Data;Kategoria");
            decimal sum = 0;
            foreach (var income in incomes)
            {
                csv.AppendLine($"{income.Name};{income.Amount};{income.Date.ToShortDateString()};Przychód");
                sum = sum + income.Amount;
            }
            foreach (var expense in expenses)
            {
                csv.AppendLine($"{expense.Name};{((expense.Amount)*(-1))};{expense.Date.ToShortDateString()};{expense.Category.Name}");
                sum = sum - expense.Amount;
            }
            csv.AppendLine($"Podsumowanie:;{sum};;");
            return csv.ToString();
        }
    }
}
