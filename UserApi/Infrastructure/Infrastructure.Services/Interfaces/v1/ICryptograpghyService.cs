namespace Infrastructure.Services.Interfaces.v1
{
    public interface ICryptograpghyService
    {
        string HashPassword(string? password);
    }
}
