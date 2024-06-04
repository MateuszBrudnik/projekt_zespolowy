using System;
using Projekt.Data;
using Projekt.Entities;
using System.Text.Json;
using WebPush;

namespace Projekt.Services
{
    public class PushNotificationService
    {
        private readonly VapidDetails _vapidDetails;
        private readonly ExpenseContext _context;

        public PushNotificationService(string subject, string publicKey, string privateKey, ExpenseContext context)
        {
            _vapidDetails = new VapidDetails(subject, publicKey, privateKey);
            _context = context;
        }

        public void SendNotification(string endpoint, string p256dh, string auth, PushNotificationPayload payload)
        {
            var subscription = new PushSubscription(endpoint, p256dh, auth);
            var webPushClient = new WebPushClient();
            var jsonPayload = JsonSerializer.Serialize(payload);

            webPushClient.SendNotification(subscription, jsonPayload, _vapidDetails);

            // Save notification to database
            var notification = new Notification
            {
                Title = payload.Title,
                Message = payload.Message,
                Url = payload.Url,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}

