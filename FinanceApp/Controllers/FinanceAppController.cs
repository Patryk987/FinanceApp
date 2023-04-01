using FinanceApp.Entities;
using Microsoft.AspNetCore.Components.Web;
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
        // GET: wszystkich użytkowników
        [HttpGet("Users")]
        public ActionResult <IEnumerable<Users>> GetAllUser()
        {
<<<<<<< Updated upstream

            var users = _dbContext.User.ToList() ;
=======
            var users = _dbContext.Users.ToList() ;
>>>>>>> Stashed changes

            if (users == null) { return NotFound(); }
            return Ok(users);
        }


        // GET: uzytkownika po loginie
        [HttpGet("User{login}")]
        public ActionResult<Users> GetByLogin([FromRoute] string login)
        {            
            var user = _dbContext.Users.FirstOrDefault(
                x => x.Login.ToLower() == login.ToLower());
            if (user == null) { return NotFound(); }
            return Ok(user);
        }
        //Get: wszystkich dokumentów
        [HttpGet( "Document")]
        public ActionResult<IEnumerable<Document>> GetAllDocuments()
        {
            var documents = _dbContext.Documents.ToList();

            if (documents == null) { return NotFound(); }
            return Ok(documents);
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
