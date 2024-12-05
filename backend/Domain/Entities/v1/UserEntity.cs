namespace Domain.Entities.v1
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
