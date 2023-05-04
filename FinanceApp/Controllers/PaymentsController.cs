using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Route("api/financeApp")]
    [ApiController]
    public class PaymentsController : Controller

    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;

        public PaymentsController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;
        }
        [HttpGet("All")]
        public ActionResult<IEnumerable<Payment>> GetAll()
        {
            var sql = " select * from dbo.Payments where typeOfPayments = 1";
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
            var sql = " select * from dbo.Payments where typeOfPayments = 2";
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
            var sql = " select sum([amountPLN]) from dbo.Payments where typeOfPayments = 1";
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
        [HttpGet("Savings Balance")]
        public ActionResult<IEnumerable<Payment>> GetSavingsBalance()
        {
            var sql = " select sum([amountPLN]) from dbo.Payments where typeOfPayments = 2";
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

        [HttpPost]
        public ActionResult InsertPaymants([FromBody] Payment payment)
        {
            var sql = "INSERT INTO [dbo].[[Payments]]([name],[amountPLN],[typeOfPayments],[UserId],[amountWal],[waluta],[paymentsDate])" +
                      "Values (@name,@amountPLN,@typeOfPayments,@UserId,@amountWal,@waluta,@paymentsDate)";

            var parameters = new DynamicParameters();
            parameters.Add("name", payment.Name);
            parameters.Add("amountPLN", payment.AmountPln);
            parameters.Add("typeOfPayments", payment.TypeOfPayments);
            parameters.Add("UserId", payment.UserId);
            parameters.Add("amountWal", payment.AmountWal);
            parameters.Add("waluta", payment.Waluta);
            parameters.Add("paymentsDate", DateTime.Now);

            using (var sqlcon = _dapperContex.Connect())
            {
                var exec = sqlcon.Execute(sql, parameters);
                return Created($"api/financeApp/payment/{payment.Name}", null);
            }

        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int id)
        {
            var sql = "DELETE FROM [dbo].[Payments] WHERE ID =@id";

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
