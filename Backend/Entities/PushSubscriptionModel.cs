using System;
namespace Projekt.Entities
{
    public class PushSubscriptionModel
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }
}

