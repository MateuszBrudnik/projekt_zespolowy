using System.Collections.Generic;
using System.Threading.Tasks;
using Projekt.Entities;

namespace Projekt.Services
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetIncomesAsync(string userId);
        Task<Income> GetIncomeByIdAsync(int id);
        Task AddIncomeAsync(Income income);
        Task UpdateIncomeAsync(Income income);
        Task DeleteIncomeAsync(int id);
        Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
    }
}
