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

        public async Task<List<Trend>> GetSpendingTrendsAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
            var trends = expenses
                .GroupBy(e => e.Date.Date)
                .Select(g => new Trend { Date = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();
            return trends;
        }

        public async Task<List<CategoryExpense>> GetCategoryWiseExpensesAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
            var categoryExpenses = expenses
                .GroupBy(e => e.Category.Name)
                .Select(g => new CategoryExpense { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();
            return categoryExpenses;
        }

        public async Task<ReportSummary> GetIncomeExpenseSummaryAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
            var incomes = await _incomeService.GetIncomesByDateRangeAsync(userId, startDate, endDate);

            var totalExpenses = expenses.Sum(e => e.Amount);
            var totalIncomes = incomes.Sum(i => i.Amount);

            return new ReportSummary
            {
                TotalExpenses = totalExpenses,
                TotalIncomes = totalIncomes
            };
        }

    }
}
