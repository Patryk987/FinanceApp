using AutoMapper;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class ShopListController : Controller
    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;
        private JwtHelper _jwt;

        public ShopListController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex, JwtHelper jwt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;
            _jwt = jwt;
        }

        [HttpGet("List")]
        public void GetList()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {

            }


        }

        [HttpPost("List")]
        public void AddList()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {

            }


        }

        [HttpGet("List/Product")]
        public void GetProduct()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {

            }


        }

        [HttpPost("List/Product")]
        public void AddProduct()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {

            }


        }

        [HttpDelete("List/Product")]
        public void DeleteProduct()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {

            }


        }
    }
}
