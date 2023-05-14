using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Route("api/financeApp/statement")]
    [ApiController]
    public class StatementsController : Controller
    {


        private readonly DapperContex _dapperContex;
            private readonly FinanceAppContext _dbContext;
            private readonly IMapper _mapper;

            public StatementsController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _dapperContex = DapperContex;
            }
        public ActionResult<IEnumerable<ProductGroup>> AllGroup()
        {
            
            {
                var sql = $"select name from ProductGroup";
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
        }

        public ActionResult<Product>  GetStatementsGroupFrom([FromRoute] int days)
            {
            var sql = $"select POSITION.price,gr.name groupname " +
            $"from DocumentPos POSITION " +
            $"JOIN products prod on prod.id=POSITION.idProd " +
            $"JOIN ProductGroup gr on gr.idGroup = prod.idGroup " +
            $"where POSITION.date BETWEEN GETDATE() and GETDATE()-{days} "+
            $"GROUP by gr.name,POSITION.price";
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
        public ActionResult<Document> GetDocumentFrom([FromRoute] int days)
        {
            var sql = $"SELECT Name,amount,[Desc],DataDokumentu" +
                        $"FROM[FinanceApp].[dbo].[Documents] " +
                        $"where DataDokumentu BETWEEN GETDATE() and GETDATE()-{days} ";
               
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
    }
    }

