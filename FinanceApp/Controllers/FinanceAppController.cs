using AutoMapper;
using FinanceApp.Entities;
using FinanceApp.Models;
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
        private readonly IMapper _mapper;

        public FinanceAppController(FinanceAppContext dbContext, IMapper mapper) 
        {
            _dbContext  = dbContext;
            _mapper = mapper;
        }
        // GET: wszystkich użytkowników
        [HttpGet("Users")]
        public ActionResult <IEnumerable<User>> GetAllUser()
        {

            var users = _dbContext
                .Users
                .ToList() ;

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
        //Get: wszystkich dokumentów
        [HttpGet( "Document")]
        public ActionResult<IEnumerable<DocumentDTO>> GetAllDocuments()
        {
            var documents = _dbContext
                .Documents
               // .Include(r => r.DocumentPos)
                .ToList();

            //var documentDto = _mapper.Map<List<DocumentDTO>>(documents);

            if (documents == null) { return NotFound(); }
            return Ok(documents);
        }

        [HttpGet("DocumentPos")]
        public ActionResult<IEnumerable<DocumentPo>> GetAllDocumentsPos()
        {
            var position = _dbContext.DocumentPos
                
                .ToList();

            if (position == null) { return NotFound(); }
            return Ok(position);
        }

        [HttpGet("Shops")]
        public ActionResult<IEnumerable<Shop>> GetAllShops()
        {
            var shops = _dbContext.Shops.ToList();

            if (shops == null) { return NotFound(); }
            return Ok(shops);
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
