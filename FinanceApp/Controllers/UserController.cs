using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Route("api/financeApp/User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;
        

        public UserController(FinanceAppContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // GET: wszystkich użytkowników
        [HttpGet("Users")]
        public ActionResult<IEnumerable<User>> GetAllUser()
        {

            var users = _dbContext
                .Users
                .ToList();

            if (users == null) { return NotFound(); }
            return Ok(users);
        }


        // GET: uzytkownika po loginie
        [HttpGet("User{login}")]
        public ActionResult<User> GetByLogin([FromRoute] string login)
        {
            var user = _dbContext.Users.FirstOrDefault(
                x => x.Login.ToLower() == login.ToLower());
            if (user == null) { return NotFound(); }
            return Ok(user);
        }
        [HttpGet("User-1")]
        public ActionResult<IEnumerable<User>> dapper()
        {
            using(var  sqlcon = new SqlConnection("Data Source=ADRIAN\\SQLEXPRESS;Initial Catalog=FinanceApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                var sql = "select d.name,prod.Name towar, pos.price from Documents d\r\njoin DocumentPos pos on d.id = pos.idDoc\r\njoin products prod on prod.id=pos.idProd";
                var x = sqlcon.Query<User>(sql).ToList();
                if (x == null) { return NotFound(); }
                return Ok(x);
            }
            
        }
    }
}
