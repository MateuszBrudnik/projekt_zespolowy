using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
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

        

        

        [HttpGet]
        public async Task<ActionResult<AnalysisResult>> GetAnalysis()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesAsync(userId);

            // Implement your analysis logic here based on expenses
            var analysisResult = new AnalysisResult
            {
                TotalExpenses = expenses.Sum(e => e.Amount),
                // More analysis logic here
            };

            return Ok(analysisResult);
        }
    }
}