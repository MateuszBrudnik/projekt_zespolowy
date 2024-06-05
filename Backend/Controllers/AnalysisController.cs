using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetMonthlySummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesAsync(userId);
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
        public async Task<IActionResult> GetCategorySummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesAsync(userId);
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
