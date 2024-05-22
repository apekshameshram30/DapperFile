using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperExecution.Contex
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {   
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connStr");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        
    }
}
