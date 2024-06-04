using System;
using Microsoft.AspNetCore.Mvc;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public AnalysisController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("monthly-summary")]
        public IActionResult GetMonthlySummary()
        {
            var expenses = _expenseService.GetExpenses();
            var summary = expenses.GroupBy(e => new { e.Date.Year, e.Date.Month })
                                  .Select(g => new
                                  {
                                      Year = g.Key.Year,
                                      Month = g.Key.Month,
                                      Total = g.Sum(e => e.Amount)
                                  })
                                  .ToList();
            return Ok(summary);
        }

        [HttpGet("category-summary")]
        public IActionResult GetCategorySummary()
        {
            var expenses = _expenseService.GetExpenses();
            var summary = expenses.GroupBy(e => e.Category.Name)
                                  .Select(g => new
                                  {
                                      Category = g.Key,
                                      Total = g.Sum(e => e.Amount)
                                  })
                                  .ToList();
            return Ok(summary);
        }
    }
}