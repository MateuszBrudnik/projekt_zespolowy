using System;
using Projekt.Entities;

namespace Projekt.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses();
        Expense GetExpenseById(int id);
        void AddExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int id);
        IEnumerable<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate);
    }
}
