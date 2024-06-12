using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Income>> GetIncomesAsync(string userId)
        {
            return await _context.Incomes.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Income> GetIncomeByIdAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task AddIncomeAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            _context.Entry(income).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncomeAsync(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId && i.Date >= startDate && i.Date <= endDate)
                .ToListAsync();
        }
    }
}
