using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using FinanceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/payments")]
    [Route("api/payments")]
    [ApiController]

    public class PaymentsController : Controller

    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;
        private JwtHelper _jwt;

        public PaymentsController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex, JwtHelper jwt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;
            _jwt = jwt;
        }
        [HttpGet("All")]
        public ActionResult<IEnumerable<Payment>> GetAll()
        {
            var sql = "select * from dbo.Payments where typeOfPayments = 1";
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
        [HttpGet("AllSavings")]
        public ActionResult<IEnumerable<Payment>> GetAllSavings()
        {
            var sql = "select * from dbo.Payments where typeOfPayments = 2";
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

        [HttpGet("Balance")]
        public ActionResult<IEnumerable<Payment>> GetBalance()
        {
            var sql = "select sum([amountPLN]) from dbo.Payments where typeOfPayments = 1";
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
        [HttpGet("SavingsBalance")]
        public ActionResult<IEnumerable<Payment>> GetSavingsBalance()
        {

            var sql = "select sum([amountPLN]) from dbo.Payments where typeOfPayments = 2";
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

        // POST: Payments/Create

        [HttpPost("insert")]
        public ActionResult InsertPaymants([FromBody] Payment payment)
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);
            Console.WriteLine(valid);

            // var decode = _jwt.DecodeToken(headerValueToken);
            // Console.WriteLine(decode);
            // var sql = "INSERT INTO [dbo].[Payments]([name],[amountPLN],[typeOfPayments],[UserId],[amountWal],[waluta],[paymentsDate])" +
            // "Values (@name,@amountPLN,@typeOfPayments,@UserId,@amountWal,@waluta,@paymentsDate)";

            var sql = "INSERT INTO [dbo].[Payments]([name],[amountPLN],[typeOfPayments],[UserId],[amountWal],[waluta])" +
                      "Values (@name,@amountPLN,@typeOfPayments,@UserId,@amountWal,@waluta)";

            var parameters = new DynamicParameters();
            parameters.Add("name", payment.Name);
            parameters.Add("amountPLN", payment.AmountPln);
            parameters.Add("typeOfPayments", payment.TypeOfPayments);
            parameters.Add("UserId", payment.UserId);
            parameters.Add("amountWal", payment.AmountWal);
            parameters.Add("waluta", payment.Waluta);
            //parameters.Add("paymentsDate", DateTime.Now);

            using (var sqlcon = _dapperContex.Connect())
            {
                var exec = sqlcon.Execute(sql, parameters);
                return Ok(exec);
                // Console.WriteLine(exec);
                // return Created($"api/financeApp/payment/{payment.Name}", null);
            }

        }

        // GET: Payments/Delete/5
        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            var sql = "DELETE FROM [dbo].[Payments] WHERE ID = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var sqlcon = _dapperContex.Connect())
            {
                var exec = sqlcon.Execute(sql);
                {
                    var affectedRows = sqlcon.Execute(sql);
                    return Ok($"Affected Rows: {affectedRows}");
                }
            }
        }

    }
}
