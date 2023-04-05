using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/account")]

    [Route("api/account")]
    [ApiController]
    public class AccountControler : ControllerBase
    {
       private readonly IAccountService _accountService;

       public AccountControler(IAccountService accountService)
       {
           _accountService = accountService;
       }

       [HttpPost("register")]
       public ActionResult RegisterUer([FromBody] RegisterUserDto dto)
       {
           _accountService.RegisterUser(dto);
           return Ok();
       }
    }
}
