using System;
using Projekt.Entities;

namespace Projekt.Services
{
    public interface IIncomeService
    {
        IEnumerable<Income> GetIncomes();
        Income GetIncomeById(int id);
        void AddIncome(Income income);
        void UpdateIncome(Income income);
        void DeleteIncome(int id);
    }
}

