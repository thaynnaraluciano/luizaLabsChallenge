namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommand
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
