using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/cyclicalExpenses")]
    [Route("api/cyclicalExpenses")]
    [ApiController]

    public class CyclicalExpensesController : Controller
    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;
        private JwtHelper _jwt;

        public CyclicalExpensesController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex, JwtHelper jwt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;
            _jwt = jwt;
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<CyclicalExpensesModel>> GetCyclicalExpenses()
        {
            var tokenExist = HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);

            if (tokenExist)
            {

                var valid = _jwt.ValidateToken(headerValueToken);

                if (valid)
                {

                    var jwtUserId = _jwt.DecodeToken(headerValueToken);

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", jwtUserId);

                    var query = "select * from dbo.CyclicalExpenses where UserId = @UserId";

                    using (var sqlConnection = _dapperContex.Connect())
                    {
                        var queryResults = sqlConnection.Query(query, parameters).ToList();

                        if (queryResults == null)
                        {
                            return NotFound();
                        }

                        return Ok(queryResults);
                    }


                }

            }

            return Unauthorized();

        }

        [HttpPost("add")]
        public ActionResult<StatusModel> AddNewCyclicalExpenses([FromBody] CyclicalExpensesModel InputData)
        {

            var tokenExist = HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);

            if (tokenExist)
            {

                var valid = _jwt.ValidateToken(headerValueToken);

                if (valid)
                {

                    var jwtUserId = _jwt.DecodeToken(headerValueToken);

                    decimal AmountPln;

                    if (InputData.Currency == "PLN")
                    {
                        AmountPln = InputData.Amount;
                    }
                    else
                    {
                        AmountPln = ConvertCurrencyToPln.GetConvertPrice(InputData.Currency, InputData.Amount);
                    }

                    var parameters = new DynamicParameters();

                    parameters.Add("Name", InputData.Name);
                    parameters.Add("StartData", InputData.StartData);
                    parameters.Add("Periods", InputData.Periods);
                    parameters.Add("Amount", InputData.Amount);
                    parameters.Add("AmountPln", AmountPln);
                    parameters.Add("Currency", InputData.Currency);
                    parameters.Add("UserId", jwtUserId);
                    parameters.Add("Groups", InputData.Groups);

                    var query = "INSERT INTO dbo.CyclicalExpenses (Name, StartData, Periods, Amount, AmountPln, Currency, UserId, Groups) VALUES (@Name, @StartData, @Periods, @Amount, @AmountPln, @Currency, @UserId, @Groups)";

                    using (var sqlConnection = _dapperContex.Connect())
                    {
                        var queryResults = sqlConnection.Execute(query, parameters);

                        StatusModel status;

                        if (queryResults > 0)
                        {
                            status = new StatusModel { status = true };
                        }
                        else
                        {
                            status = new StatusModel { status = false };
                        }

                        return Ok(status);


                    }


                }

            }

            return Unauthorized();


        }

        [HttpDelete("delete")]
        public ActionResult<StatusModel> DeleteCyclicalExpenses(int Id)
        {

            var tokenExist = HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);

            if (tokenExist)
            {

                var valid = _jwt.ValidateToken(headerValueToken);

                if (valid)
                {

                    var jwtUserId = _jwt.DecodeToken(headerValueToken);

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", jwtUserId);
                    parameters.Add("Id", Id);

                    var query = "DELETE FROM dbo.CyclicalExpenses where UserId = @UserId AND Id = @Id";

                    using (var sqlConnection = _dapperContex.Connect())
                    {
                        var queryResults = sqlConnection.Execute(query, parameters);

                        StatusModel status;

                        if (queryResults > 0)
                        {
                            status = new StatusModel { status = true };
                        }
                        else
                        {
                            status = new StatusModel { status = false };
                        }

                        return Ok(status);

                    }


                }

            }

            return Unauthorized();

        }

        [HttpPut]
        public ActionResult<IEnumerable<Payment>> UpdateCyclicalExpenses()
        {

            return Ok();

        }

    }
}
