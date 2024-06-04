using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Projekt.Data;
using Projekt.Entities;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly PushNotificationService _pushNotificationService;
        private readonly ExpenseContext _context; // Dodajemy kontekst

        public NotificationController(PushNotificationService pushNotificationService, ExpenseContext context)
        {
            _pushNotificationService = pushNotificationService;
            _context = context; // Wstrzykujemy kontekst
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] PushSubscriptionModel model)
        {
            // Store subscription details to the database (not shown here for brevity)
            // Send a welcome notification
            var payload = new PushNotificationPayload
            {
                Title = "Welcome",
                Message = "Welcome to Expense Tracker!",
                Url = "/"
            };
            _pushNotificationService.SendNotification(model.Endpoint, model.P256dh, model.Auth, payload);
            return Ok();
        }

        [HttpGet("history")]
        public IActionResult GetNotificationHistory()
        {
            var notifications = _context.Notifications
                                        .OrderByDescending(n => n.Timestamp)
                                        .ToList();
            return Ok(notifications);
        }
    }
}
