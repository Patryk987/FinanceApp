global using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FinanceApp;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Models.Validators;
using FinanceApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
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
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidators>();//Wywoï¿½anie walidatora do rejestrowanych uï¿½ytkownikï¿½w
builder.Services.AddDbContext<FinanceAppContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("FinanceAppDbConnection"))); // <== con do bazy

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

string hostName = Dns.GetHostName();
var addresses = Dns.GetHostEntry((Dns.GetHostName()))
                    .AddressList
                    .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                    .Select(x => x.ToString())
                    .ToArray();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContex>();

builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse(addresses.FirstOrDefault().ToString()), 5003));
//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("10.10.60.159"), 5003));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

///*
// * ********************************************Odblokowanie policy CORS ************************************************************************

<<<<<<< HEAD
// builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
// {
//     build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
// }));
=======
//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
//{
//    build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
//}));
>>>>>>> ccb7c208fee1284d51add97e9aa1474bffbfdaac



//****************************************************************************************************
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

<<<<<<< HEAD
// app.UseCors("corspolicy");    //uzycie policy CORS do odblokowania
app.UseAuthentication();   //u¿ycie autentykacji JWT
//app.UseHttpsRedirection();
=======
app.UseCors("corspolicy");    //uzycie policy CORS do odblokowania
app.UseAuthentication();   //uï¿½ycie autentykacji JWT
app.UseHttpsRedirection();
>>>>>>> ccb7c208fee1284d51add97e9aa1474bffbfdaac

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();

//test adrian
//komentarz Janek
