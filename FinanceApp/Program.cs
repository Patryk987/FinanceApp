global using Microsoft.EntityFrameworkCore;
using System.Text;
using FinanceApp;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Models.Validators;
using FinanceApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Aplikacja do zarzadzania finanasami 


//****************************************************************************************************
// Add services to the container.

//********************************************************************* Fragment dla JWT token ***************************************************************
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);    //wpis w pliku appsetings.json
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))

    };
});
//******************************************************************************************************************************8
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

//**********************************************Odblokowanie policy CORS ************************************************************************

// builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
// {
//     build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
// }));



//****************************************************************************************************
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// app.UseCors("corspolicy");    //uzycie policy CORS do odblokowania
app.UseAuthentication();   //u¿ycie autentykacji JWT
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//test adrian
//komentarz Janek
