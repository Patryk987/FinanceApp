using System.Diagnostics.Eventing.Reader;
using FinanceApp.Entities;
using FinanceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Services
{

    // Klasa odpowiedzialna za tworzenie nowych kont 

    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly FinanceAppContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(FinanceAppContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }
        public void RegisterUser(RegisterUserDto dto)
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
        }
    }
}
