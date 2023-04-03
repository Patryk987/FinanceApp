
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
namespace FinanceApp.Entities
{
    public class DapperContex
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContex(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection Connect()
            => new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
    }
}
