using System;

namespace PizzeriaFileImplement.Models
{
    public class MessageInfo
    {
        public string MessageId { get; set; }

        public int? ClientId { get; set; }

        public string SenderName { get; set; }

        public DateTime DateDelivery { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool HasBeenRead { get; set; }

        public string Response { get; set; }
    }
}
