using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projekt.Entities;
using Projekt.Services;

    public interface IReportService
    {
        Task<List<Trend>> GetSpendingTrendsAsync(DateTime startDate, DateTime endDate, string userId);
        Task<List<CategoryExpense>> GetCategoryWiseExpensesAsync(DateTime startDate, DateTime endDate, string userId);
        Task<ReportSummary> GetIncomeExpenseSummaryAsync(DateTime startDate, DateTime endDate, string userId);

        Task<decimal> GetTotalExpensesAsync(string userId, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalIncomesAsync(string userId, DateTime startDate, DateTime endDate);
    }

