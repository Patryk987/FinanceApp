using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceApp.Controllers
{

    //create table[FinanceApp].[dbo].[ShoppingList] (Id int, ListName nvarchar(255), UserId int, PRIMARY KEY(Id))
    //create table[FinanceApp].[dbo].[ProductList] (Id int, ProductName nvarchar(255), Status BIT, ListId int, UserId int, PRIMARY KEY(Id))

    [Microsoft.AspNetCore.Components.Route("api/ShopingList")]
    [Route("api/ShopingList")]
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
        public ActionResult<IEnumerable<ShoppingListModel>> GetList()
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);

                var sql = "select * from [dbo].[ShoppingList] where UserId = @UserId";
                using (var sqlcon = _dapperContex.Connect())
                {
                    var sqlResults = sqlcon.Query(sql, parameters).ToList();
                    if (sqlResults == null)
                    {
                        return NoContent();
                    }

                    return Ok(sqlResults);
                }
            }

            return NotFound();

        }

        [HttpPost("List")]

        public ActionResult<StatusModel> AddList([FromBody] ShoppingListModel inputData)
        {

            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);
                parameters.Add("ListName", inputData.ListName);
                var sql = "INSERT INTO [dbo].[ShoppingList] (ListName, UserId) VALUES (@ListName, @UserId)";

                using (var sqlcon = _dapperContex.Connect())
                {
                    var exec = sqlcon.Execute(sql, parameters);

                    var status = new StatusModel { status = true };
                    return Ok(status);
                }

            }
            else
            {
                var status = new StatusModel { status = false };
                return Ok(status);
            }


        }

        [HttpPost("List/Product")]
        public ActionResult<StatusModel> AddProduct([FromBody] ShoppingListElementModel inputData)
        {

            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);
                parameters.Add("ProductName", inputData.ProductName);
                parameters.Add("ListId", inputData.ListId);

                var sql = "INSERT INTO [dbo].[ProductList] (ProductName, ListId, UserId) VALUES (@ProductName, @ListId, @UserId)";

                using (var sqlcon = _dapperContex.Connect())
                {
                    var exec = sqlcon.Execute(sql, parameters);

                    var status = new StatusModel { status = true };
                    return Ok(status);
                }

            }
            else
            {
                var status = new StatusModel { status = false };
                return Ok(status);
            }


        }

        [HttpPost("List/delete")]
        public ActionResult<StatusModel> DeleteList([FromBody] ShoppingListModel inputData)
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);
                parameters.Add("ListId", inputData.Id);

                var sql = "DELETE [dbo].[ShoppingList] where UserId = @UserId AND Id = @ListId";

                using (var sqlcon = _dapperContex.Connect())
                {
                    var sqlResults = sqlcon.Execute(sql, parameters);
                    if(sqlResults > 0)
                    {

                        return Ok(new StatusModel { status = true });
                    } else
                    {
                        return Ok(new StatusModel { status = false });

                    }

                }
            }

            return Ok(new StatusModel { status = false });


        }

        [HttpGet("List/Product")]
        public ActionResult<ShoppingListElementModel> GetProducts(int ListId)
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);
                parameters.Add("ListId", ListId);

                var sql = "select * from [dbo].[ProductList] where UserId = @UserId AND ListId = @ListId";

                using (var sqlcon = _dapperContex.Connect())
                {
                    var sqlResults = sqlcon.Query(sql, parameters).ToList();

                    if (sqlResults == null)
                    {
                        return Ok();
                    }

                    return Ok(sqlResults);

                }

            }

            return NoContent();
        }

        [HttpPut("List/Product")]
        public ActionResult<StatusModel> PutProducts([FromBody] ShoppingListElementModel inputData)
        {
            HttpContext.Request.Headers.TryGetValue("token", out var headerValueToken);
            var valid = _jwt.ValidateToken(headerValueToken);

            if (valid)
            {
                var decode = _jwt.DecodeToken(headerValueToken);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", decode);
                parameters.Add("ListId", inputData.ListId);
                parameters.Add("Id", inputData.Id);
                parameters.Add("Status", inputData.Status);

                var sql = "UPDATE [dbo].[ProductList] SET Status = @Status WHERE UserId = @UserId AND ListId = @ListId AND Id = @Id";

                using (var sqlcon = _dapperContex.Connect())
                {
                    var sqlResults = sqlcon.Query(sql, parameters).ToList();

                    if (sqlResults == null)
                    {
                        return Ok(new StatusModel { status = false});
                    }

                    return Ok(new StatusModel { status = true });

                }

            }

            return Ok(new StatusModel { status = false});
        }
    }
}
