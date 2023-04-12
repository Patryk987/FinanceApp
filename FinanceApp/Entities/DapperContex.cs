
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
namespace FinanceApp.Entities
{
    public class DapperContex
    {
        string contex = File.ReadAllText("C:\\DbContext.txt");
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContex(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = contex;
        }

        public IDbConnection Connect()
            => new SqlConnection(_connectionString);
    }
}
