using System;
using Projekt.Data;
using Projekt.Entities;

namespace Projekt.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly ExpenseContext _context;

        public IncomeService(ExpenseContext context)
        {
            _context = context;
        }

        public IEnumerable<Income> GetIncomes()
        {
            return _context.Incomes.ToList();
        }

        public Income GetIncomeById(int id)
        {
            return _context.Incomes.Find(id);
        }

        public void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }

        public void UpdateIncome(Income income)
        {
            _context.Incomes.Update(income);
            _context.SaveChanges();
        }

        public void DeleteIncome(int id)
        {
            var income = _context.Incomes.Find(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                _context.SaveChanges();
            }
        }
    }
}
