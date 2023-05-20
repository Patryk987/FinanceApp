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
                var x = sqlcon.QuerySingleOrDefault(sql).ToList();
                if (x == null)
                {
                    return NotFound();
                }

                return Ok(x);
            }
        }

        //To do wywalenia nie potrzebne, jest rejestrowanie użytkowników w Controllers jako AccountControler
        //Adrian Sprawdź i oceń 
        //*************************************************************************************************************************
        ////dodawanie uzytkownika
        //[HttpPost]
        //public ActionResult CreateUser([FromBody] User user)
        //{
        //    var sql = "INSERT INTO [dbo].[users]([Login],[Name],[Surname],[IdGroup],[password],[CreateDate])" +
        //              "Values (@Login,@Name,@Surname,@IdGroup,@password,@CreateDate)";

        //    var parameters = new DynamicParameters();
        //    parameters.Add("Login", user.Login);
        //    parameters.Add("password", user.Password);
        //    parameters.Add("Name", user.Name);
        //    parameters.Add("Surname", user.Surname);
        //    parameters.Add("IdGroup", user.IdGroup);
        //    parameters.Add("CreateDate", DateTime.Now);

        //    using (var sqlcon = _dapperContex.Connect())
        //    {
        //        var exec = sqlcon.Execute(sql, parameters);
        //        return Created($"api/financeApp/User/{user.Login}", null);
        //    }

        //}
        //*********************************************************************************************************************
    }



}
