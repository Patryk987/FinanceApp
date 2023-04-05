using FinanceApp.Entities;
using FluentValidation;

namespace FinanceApp.Models.Validators
{

    //Walidatory do zarejestrowania użytkownika
    public class RegisterUserDtoValidators : AbstractValidator<RegisterUserDto>
    {

        public RegisterUserDtoValidators(FinanceAppContext dbContext)
        {
            RuleFor(x=>x.Login)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Surname)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
           

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(e => e.Password);

            RuleFor(x => x.Login)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Login == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }

                });
        }
    }
}
