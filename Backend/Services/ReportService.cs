using System;
using System.Threading.Tasks;
using Projekt.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Projekt.Entities;

namespace Projekt.Services
{
    public class ReportService : IReportService
    {
        private readonly ExpenseContext _context;
        private readonly IExpenseService _expenseService;
        private readonly IIncomeService _incomeService;

        public ReportService(ExpenseContext context, IExpenseService expenseService, IIncomeService incomeService)
        {
            _context = context;
            _expenseService = expenseService;
            _incomeService = incomeService;
        }

        public async Task<decimal> GetTotalExpensesAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
            return expenses.Sum(e => e.Amount);
        }

        public async Task<decimal> GetTotalIncomesAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var incomes = await _incomeService.GetIncomesByDateRangeAsync(userId, startDate, endDate);
            return incomes.Sum(i => i.Amount);
        }

        public async Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate, string userId)
        {
            return await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
        }

        public async Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate, string userId)
        {
            return await _incomeService.GetIncomesByDateRangeAsync(userId, startDate, endDate);
        }

        public async Task<ReportSummary> GetReportSummaryAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await GetExpensesByDateRangeAsync(startDate, endDate, userId);
            var incomes = await GetIncomesByDateRangeAsync(startDate, endDate, userId);

            return new ReportSummary
            {
                TotalExpenses = expenses.Sum(e => e.Amount),
                TotalIncomes = incomes.Sum(i => i.Amount),
                NetBalance = incomes.Sum(i => i.Amount) - expenses.Sum(e => e.Amount)
            };
        }

        public async Task<SpendingTrends> GetSpendingTrendsAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await GetExpensesByDateRangeAsync(startDate, endDate, userId);
            var groupedByDate = expenses.GroupBy(e => e.Date.Date)
                                        .Select(g => new { Date = g.Key, Total = g.Sum(e => e.Amount) })
                                        .OrderBy(g => g.Date)
                                        .ToList();

            return new SpendingTrends { Trends = (IEnumerable<Trend>)groupedByDate };
        }

        public async Task<CategoryWiseExpenses> GetCategoryWiseExpensesAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await GetExpensesByDateRangeAsync(startDate, endDate, userId);
            var groupedByCategory = expenses.GroupBy(e => e.Category.Name)
                                            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
                                            .OrderByDescending(g => g.Total)
                                            .ToList();

            return new CategoryWiseExpenses { Expenses = (IEnumerable<CategoryExpense>)groupedByCategory };
        }

        public async Task<MonthlyIncomeExpenseSummary> GetMonthlyIncomeExpenseSummaryAsync(string userId)
        {
            var expenses = await _expenseService.GetExpensesAsync(userId);
            var incomes = await _incomeService.GetIncomesAsync(userId);

            var groupedExpenses = expenses.GroupBy(e => new { e.Date.Year, e.Date.Month })
                                          .Select(g => new { g.Key.Year, g.Key.Month, TotalExpenses = g.Sum(e => e.Amount) })
                                          .ToList();

            var groupedIncomes = incomes.GroupBy(i => new { i.Date.Year, i.Date.Month })
                                        .Select(g => new { g.Key.Year, g.Key.Month, TotalIncomes = g.Sum(i => i.Amount) })
                                        .ToList();

            var monthlySummary = from e in groupedExpenses
                                 join i in groupedIncomes on new { e.Year, e.Month } equals new { i.Year, i.Month } into ei
                                 from subIncome in ei.DefaultIfEmpty()
                                 select new MonthlyIncomeExpense
                                 {
                                     Year = e.Year,
                                     Month = e.Month,
                                     TotalExpenses = e.TotalExpenses,
                                     TotalIncomes = subIncome?.TotalIncomes ?? 0,
                                     NetBalance = (subIncome?.TotalIncomes ?? 0) - e.TotalExpenses
                                 };

            return new MonthlyIncomeExpenseSummary { Summary = monthlySummary.ToList() };
        }

    }
}
