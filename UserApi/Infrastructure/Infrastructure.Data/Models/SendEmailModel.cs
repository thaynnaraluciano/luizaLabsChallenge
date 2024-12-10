namespace Infrastructure.Data.Models
{
    public class SendEmailModel
    {
        public string? ReceiverName { get; set; }

        public string? ReceiverEmail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? BodyType { get; set; }
    }
}
