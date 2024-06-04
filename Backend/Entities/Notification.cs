using System;
namespace Projekt.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

