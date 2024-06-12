using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projekt.Entities;
using Projekt.Services;

public interface IReportService
{
    Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
    Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
    Task<ReportSummary> GetReportSummaryAsync(DateTime startDate, DateTime endDate, string userId);
    Task<SpendingTrends> GetSpendingTrendsAsync(DateTime startDate, DateTime endDate, string userId);
    Task<CategoryWiseExpenses> GetCategoryWiseExpensesAsync(DateTime startDate, DateTime endDate, string userId);
    Task<MonthlyIncomeExpenseSummary> GetMonthlyIncomeExpenseSummaryAsync(string userId);
    Task<decimal> GetTotalExpensesAsync(string userId, DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalIncomesAsync(string userId, DateTime startDate, DateTime endDate);
}
