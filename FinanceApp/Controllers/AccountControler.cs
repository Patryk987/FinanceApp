using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{

    //Rejestrowanie użytkownika poprzez:
    //klasa RegisterUserDto ---- Co ma być zarejestrowane
    //Klasa RegisterUserDtoValidators  --- Walidacja wprowadzonych danych

    [Microsoft.AspNetCore.Components.Route("api/account")]

    [Route("api/account")]
    [ApiController]
    public class AccountControler : ControllerBase
    {

        //Rejestrowanie użytkownika poprzez:
        //klasa RegisterUserDto ---- Co ma być zarejestrowane
        //Klasa RegisterUserDtoValidators  --- Walidacja wprowadzonych danych
        //***************************************************************************************************
        private readonly IAccountService _accountService;

        public AccountControler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUer([FromBody] RegisterUserDto dto)
        {
            string data = _accountService.RegisterUser(dto);
            return Ok(data);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }


    }
}
