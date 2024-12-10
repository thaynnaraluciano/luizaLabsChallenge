namespace Infrastructure.Data.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public DateTime? ConfirmedAt { get; set; }

        public string? VerificationCode { get; set; }
    }
}
