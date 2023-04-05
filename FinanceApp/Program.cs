global using Microsoft.EntityFrameworkCore;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Models.Validators;
using FinanceApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Aplikacja do zarzadzania finanasami 


//****************************************************************************************************
// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();   
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidators>();//Wywo³anie walidatora do rejestrowanych u¿ytkowników
builder.Services.AddDbContext<FinanceAppContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("FinanceAppDbConnection"))); // <== con do bazy

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<DapperContex>();

var app = builder.Build();




//****************************************************************************************************
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//test adrian
//komentarz Janek
