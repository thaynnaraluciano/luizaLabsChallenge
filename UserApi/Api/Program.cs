using Microsoft.EntityFrameworkCore;
using Domain.Commands.v1.CreateUser;
using Domain.MapperProfiles;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using FluentValidation;
using MediatR;
using Api.Utils;
using Infrastructure.Services.Interfaces.v1;
using Domain.Commands.v1.Login;
using Infrastructure.Services.Services.v1;
using CrossCutting.Configuration;
using Microsoft.Extensions.Options;
using Domain.Commands.v1.ConfirmEmail;
using CrossCutting.Exceptions.Middlewares;
using Domain.Commands.v1.SendEmailConfirmation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ConfirmEmailCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(SendEmailConfirmationCommand).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
builder.Services.AddScoped<IValidator<ConfirmEmailCommand>, ConfirmEmailCommandValidator>();
builder.Services.AddScoped<IValidator<SendEmailConfirmationCommand>, SendEmailConfirmationCommandValidator>();
#endregion

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<ICryptograpghyService, CryptograpghyService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IEmailTemplateService, EmailTemplateService>();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddHttpClient<INotificationService, NotificationService>((provider, client) =>
{
    var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;

    var notificationClient = appSettings.ServiceClientsSettings
        .FirstOrDefault(c => c.Id == "notification");

    if (notificationClient == null || string.IsNullOrWhiteSpace(notificationClient.BaseUrl))
        throw new InvalidOperationException("Base URL para NotificationService n�o configurada.");

    client.BaseAddress = new Uri(notificationClient.BaseUrl);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true
});

builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
