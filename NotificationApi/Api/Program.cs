using FluentValidation;
using Domain.Commands.v1.SendEmail;
using Domain.Commands.v1;
using Infrastructure.Services.Interfaces.v1;
using Infrastructure.Services.Services.v1;
using Domain.MapperProfiles;
using CrossCutting.Configuration;
using Api.Utils;
using MediatR;
using CrossCutting.Exceptions.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
