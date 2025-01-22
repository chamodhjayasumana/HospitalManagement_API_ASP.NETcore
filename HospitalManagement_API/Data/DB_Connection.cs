using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HospitalManagement_API.DataAccess
{
    public class DB_Connection
    {
        private readonly IConfiguration _configuration;

        public DB_Connection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("HospitalManagementConnection");
            return new SqlConnection(connectionString);
        }
    }
}
