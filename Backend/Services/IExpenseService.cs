using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projekt.Entities;

namespace Projekt.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync(string userId);
        Task<Expense> GetExpenseByIdAsync(int id);
        Task AddExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int id);
        Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
    }
}
