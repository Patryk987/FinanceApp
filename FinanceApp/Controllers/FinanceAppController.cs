using FinanceApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Route("api/financeApp")]
    [ApiController]
    public class FinanceAppController : ControllerBase
    {

        private readonly FinanceAppContext _dbContext;
        public FinanceAppController(FinanceAppContext dbContext) 
        {
            _dbContext  = dbContext;
        }
        

        // GET: api/<FinanceAppController>
        [HttpGet(Name = "user")]
        public ActionResult <IEnumerable<Users>> GetAllUser()
        {

            var users = _dbContext.User.ToList() ;

            return Ok(users);
        }

        // GET api/<FinanceAppController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FinanceAppController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FinanceAppController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FinanceAppController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
