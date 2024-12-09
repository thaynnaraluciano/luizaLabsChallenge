namespace Infrastructure.Data.Models
{
    public class EmailModel
    {
        public string? ReceiverName { get; set; }

        public string? ReceiverEmail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? BodyType { get; set; }
    }
}
