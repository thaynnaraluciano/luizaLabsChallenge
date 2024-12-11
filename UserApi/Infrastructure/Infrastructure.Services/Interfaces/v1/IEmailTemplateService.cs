namespace Infrastructure.Services.Interfaces.v1
{
    public interface IEmailTemplateService
    {
        string GenerateConfirmationEmail(string receiverName, string verificationCode);
    }
}
