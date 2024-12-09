using FluentValidation;
using FluentValidation.AspNetCore;
using Domain.Commands.v1.SendEmail;
using Domain.Commands.v1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(SendEmailCommandHandler).Assembly));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<SendEmailCommand>, SendEmailCommandValidator>();
#endregion

builder.Services.AddFluentValidationAutoValidation();

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
