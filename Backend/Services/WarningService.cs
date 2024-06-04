using System;
using Projekt.Data;
using Projekt.Entities;

namespace Projekt.Services
{
    public class WarningService : IWarningService
    {
        private readonly ExpenseContext _context;
        private readonly PushNotificationService _pushNotificationService;

        public WarningService(ExpenseContext context, PushNotificationService pushNotificationService)
        {
            _context = context;
            _pushNotificationService = pushNotificationService;
        }

        public IEnumerable<string> GenerateWarnings()
        {
            var warnings = new List<string>();

            var currentMonthExpenses = _context.Expenses
                .Where(e => e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                .ToList();

            var smokingExpenses = currentMonthExpenses
                .Where(e => e.Category.Name == "Smoking")
                .Sum(e => e.Amount);

            if (smokingExpenses > 100)
            {
                var warningMessage = "Reduce spending on smoking.";
                warnings.Add(warningMessage);
                SendWarningNotification(warningMessage);
            }

            var entertainmentExpenses = currentMonthExpenses
                .Where(e => e.Category.Name == "Entertainment")
                .Sum(e => e.Amount);

            if (entertainmentExpenses > 200)
            {
                var warningMessage = "Reduce spending on entertainment.";
                warnings.Add(warningMessage);
                SendWarningNotification(warningMessage);
            }

            var totalExpenses = currentMonthExpenses.Sum(e => e.Amount);
            var monthlyIncome = 3000; // This value could be dynamic or user-defined

            if (totalExpenses > monthlyIncome)
            {
                var warningMessage = "Your total expenses exceed your income. Consider reducing expenses.";
                warnings.Add(warningMessage);
                SendWarningNotification(warningMessage);
            }

            return warnings;
        }

        private void SendWarningNotification(string message)
        {
            var subscriptions = _context.PushSubscriptions.ToList(); // Ensure this line is using the correct DbSet
            foreach (var sub in subscriptions)
            {
                var payload = new PushNotificationPayload
                {
                    Title = "Expense Tracker Warning",
                    Message = message,
                    Url = "/"
                };
                _pushNotificationService.SendNotification(sub.Endpoint, sub.P256dh, sub.Auth, payload);
            }
        }
    }
}