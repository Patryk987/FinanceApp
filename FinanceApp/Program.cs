global using Microsoft.EntityFrameworkCore;
using FinanceApp.Entities;

var builder = WebApplication.CreateBuilder(args);
// Aplikacja do zarzadzania finanasami 


//****************************************************************************************************
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();   //dodal janek

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
