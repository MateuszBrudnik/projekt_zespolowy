using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projekt.Entities;

public interface IReportService
{
    Task<decimal> GetTotalExpensesAsync(string userId, DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalIncomesAsync(string userId, DateTime startDate, DateTime endDate);
}
