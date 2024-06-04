using System;
using Projekt.Data;
using Projekt.Entities;

namespace Projekt.Services
{
        public class ExpenseService : IExpenseService
        {
            private readonly ExpenseContext _context;

            public ExpenseService(ExpenseContext context)
            {
                _context = context;
            }

            public IEnumerable<Expense> GetExpenses()
            {
                return _context.Expenses.ToList();
            }

            public Expense GetExpenseById(int id)
            {
                return _context.Expenses.Find(id);
            }

            public void AddExpense(Expense expense)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
            }

            public void UpdateExpense(Expense expense)
            {
                _context.Expenses.Update(expense);
                _context.SaveChanges();
            }

            public void DeleteExpense(int id)
            {
                var expense = _context.Expenses.Find(id);
                if (expense != null)
                {
                    _context.Expenses.Remove(expense);
                    _context.SaveChanges();
                }
            }

            public IEnumerable<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate)
            {
                return _context.Expenses.Where(e => e.Date >= startDate && e.Date <= endDate).ToList();
            }
        }

}


