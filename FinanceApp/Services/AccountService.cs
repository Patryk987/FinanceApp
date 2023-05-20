using System;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApp.Entities;
using FinanceApp.Exceptions;
using FinanceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceApp.Services
{

    // Klasa odpowiedzialna za tworzenie nowych kont 

    public interface IAccountService
    {
        JwtResponseDto RegisterUser(RegisterUserDto dto);
        JwtResponseDto GenerateJwt(LoginDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly FinanceAppContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(FinanceAppContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public JwtResponseDto RegisterUser(RegisterUserDto dto)
        {
            var user = new User()
            {
                Login = dto.Login,
                Name = dto.Name,
                Surname = dto.Surname,
                CreateDate = DateTime.Now
            };

            // Hashowanie hasła

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.Password = hashedPassword;
            _context.Users.Add(user);
            _context.SaveChanges();

            var request = new JwtResponseDto
            {
                TokenJWT = "",
                status = 200
            };

            return request;
        }

        public JwtResponseDto GenerateJwt(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == dto.Login);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim("user_id", user.Id.ToString()),
               new Claim(ClaimTypes.Name, $"{user.Name}"),
               new Claim(ClaimTypes.Surname, $"{user.Surname}")
           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresDate = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expiresDate,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var request = new JwtResponseDto
            {
                TokenJWT = tokenHandler.WriteToken(token),
                status = 200
            };

            return request;
        }
    }

}
