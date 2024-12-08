using Microsoft.EntityFrameworkCore;
using Domain.Commands.v1.CreateUser;
using Domain.MapperProfiles;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Api.Utils;
using Infrastructure.Services.Interfaces.v1;
using Infrastructure.Services.Services;
using Domain.Commands.v1.Login;
using Infrastructure.Services.Services.v1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(LoginProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
#endregion

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<ICryptograpghyService, CryptograpghyService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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