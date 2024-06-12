using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IReportService _reportService;

    public AnalysisController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("spending-trends")]
    public async Task<IActionResult> GetSpendingTrends(DateTime startDate, DateTime endDate, string userId)
    {
        var trends = await _reportService.GetSpendingTrendsAsync(startDate, endDate, userId);
        return Ok(trends);
    }

    [HttpGet("category-wise-expenses")]
    public async Task<IActionResult> GetCategoryWiseExpenses(DateTime startDate, DateTime endDate, string userId)
    {
        var expenses = await _reportService.GetCategoryWiseExpensesAsync(startDate, endDate, userId);
        return Ok(expenses);
    }

    [HttpGet("monthly-summary")]
    public async Task<IActionResult> GetMonthlyIncomeExpenseSummary(string userId)
    {
        var summary = await _reportService.GetMonthlyIncomeExpenseSummaryAsync(userId);
        return Ok(summary);
    }
}
