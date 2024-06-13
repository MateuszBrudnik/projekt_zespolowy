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

    [HttpGet("summary")]
    public async Task<IActionResult> GetIncomeExpenseSummary(DateTime startDate, DateTime endDate, string userId)
    {
        var summary = await _reportService.GetIncomeExpenseSummaryAsync(startDate, endDate, userId);
        return Ok(summary);
    }

    [HttpGet("trends")]
    public async Task<IActionResult> GetSpendingTrends(DateTime startDate, DateTime endDate, string userId)
    {
        var trends = await _reportService.GetSpendingTrendsAsync(startDate, endDate, userId);
        return Ok(trends);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoryWiseExpenses(DateTime startDate, DateTime endDate, string userId)
    {
        var expenses = await _reportService.GetCategoryWiseExpensesAsync(startDate, endDate, userId);
        return Ok(expenses);
    }
}
