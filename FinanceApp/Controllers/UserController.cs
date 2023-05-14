using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using FinanceApp.Models;
using FinanceApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Route("api/financeApp/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;


        public UserController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;

        }

        // GET: wszystkich użytkowników
        [HttpGet("Users")]
        public ActionResult<IEnumerable<User>> GetAllUser()
        {
            var sql = "SELECT * FROM Users";
            using (var sqlcon = _dapperContex.Connect())
            {
                var x = sqlcon.Query(sql).ToList();
                if (x == null)
                {
                    return NotFound();
                }

                return Ok(x);
            }
        }


        // GET: uzytkownika po loginie
        [HttpGet("{login}")]
        public ActionResult<User> GetByLogin([FromRoute] string login)
        {
            var sql = $"SELECT login FROM Users WHERE login = '{login}'";
            using (var sqlcon = _dapperContex.Connect())
            {
                var x = sqlcon.QuerySingleOrDefault(sql);
                if (x == null)
                {
                    return NotFound();
                }

                return Ok(x);
            }
        }
    }



}
