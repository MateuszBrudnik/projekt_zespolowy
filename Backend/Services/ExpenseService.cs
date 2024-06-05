using Projekt.Entities;
using Projekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Projekt.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseContext _context;

        public ExpenseService(ExpenseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(string userId)
        {
            return await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate)
                .ToListAsync();
        }
    }
}
