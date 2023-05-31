using AutoMapper;
using Dapper;
using FinanceApp.Entities;
using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace FinanceApp.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/products")]
    [Route("api/products")]
    [ApiController]
    public class ScanProductController : Controller
    {
        private readonly DapperContex _dapperContex;
        private readonly FinanceAppContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private JwtHelper _jwt;

        public ScanProductController(FinanceAppContext dbContext, IMapper mapper, DapperContex DapperContex, JwtHelper jwt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dapperContex = DapperContex;
            _jwt = jwt;
            _httpClient = new HttpClient();
        }

        // GET information aboute scaned product
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> CheckProduct(string ean)
        {
            var results = this.ProductInfo(ean);

            return Ok(results);
        }

        [HttpPost]
        public ActionResult<StatusModel> SavePrice([FromBody] ProductPriceModel productPrice)
        {

            var parameters = new DynamicParameters();
            parameters.Add("barcode", productPrice.Barcode);
            parameters.Add("price", productPrice.Price);
            parameters.Add("date", DateTime.Now);

            var sql = "INSERT INTO dbo.ProductPrice (barcode, price, date) VALUES (@barcode, @price, @date)";
            using (var sqlcon = _dapperContex.Connect())
            {

                var data = sqlcon.Query(sql, parameters).FirstOrDefault();

            }

            var result = new StatusModel { status = true };

            return Ok(result);
        }

        [HttpPost("addProduct")]
        public ActionResult<StatusModel> AddNewProduct([FromBody] ProductDTO product)
        {

            // create table [FinanceApp].[dbo].[ProductPrice] (id int, barcode nvarchar(255), price money, date date, CONSTRAINT PKProductPrice PRIMARY KEY (id))


            this.SaveProduct(product.Barcode, product.Name);

            var result = new StatusModel { status = true };

            return Ok(result);
        }


        private bool SaveProduct(string ean, string name, string imageUrl = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("barcode", ean);
            parameters.Add("name", name);
            parameters.Add("imageUrl", imageUrl);

            var sql = "INSERT INTO dbo.products (Name, barcode, imageURL) VALUES (@name, @barcode, @imageUrl)";
            using (var sqlcon = _dapperContex.Connect())
            {

                sqlcon.Query(sql, parameters);

            }

            return false;
        }

        private ProductDTO ProductInfo(string ean)
        {

            ProductDTO results;

            var parameters = new DynamicParameters();
            parameters.Add("barcode", ean);

            var sql = "SELECT * FROM dbo.products WHERE barcode = @barcode";
            using (var sqlcon = _dapperContex.Connect())
            {

                var data = sqlcon.Query(sql, parameters).FirstOrDefault();
                if (data == null)
                {

                    OpenFoodModel product = this.GetProductFromOpenFood(ean).Result;
                    if(product == null)
                    {

                        results = new ProductDTO { Barcode = ean, Name = "" };

                    } else
                    {
                        results = new ProductDTO { Barcode = product.Code, Name = product.Name, ImageUrl = product.Image };
                        this.SaveProduct(product.Code, product.Name, product.Image);
                    }
                    

                } else
                {

                    results = new ProductDTO { Barcode = data.barcode, Name = data.Name, ImageUrl = data.imageURL };

                }
                    

            }


            return results;

        }

        private async Task<OpenFoodModel> GetProductFromOpenFood(string ean)
        {
            try
            {
                string apiUrl = "https://world.openfoodfacts.org/api/v0/product/" + ean + ".json";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonData);

                    OpenFoodModel productData = new OpenFoodModel
                    {
                        Image = data.product.image_front_url,
                        Name = data.product.product_name,
                        Code = ean,
                    };

                    return productData;
                }
                else
                {
                    Console.WriteLine("Błąd podczas pobierania danych z API. Kod odpowiedzi: " + response.StatusCode);

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);

                return null;
            }
        }

    }
}
