using System;
using System.Threading.Tasks;
using Projekt.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    }
}
