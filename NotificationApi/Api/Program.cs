using FluentValidation;
using FluentValidation.AspNetCore;
using Domain.Commands.v1.SendEmail;
using Domain.Commands.v1;
using Infrastructure.Services.Interfaces.v1;
using Infrastructure.Services.Services.v1;
using Domain.MapperProfiles;
using CrossCutting.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(SendEmailCommandHandler).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(EmailProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<SendEmailCommand>, SendEmailCommandValidator>();
#endregion

builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddFluentValidationAutoValidation();

var appSettings = builder.Configuration.GetSection("Settings").Get<AppSettings>();
AppSettings.Settings = appSettings!;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
