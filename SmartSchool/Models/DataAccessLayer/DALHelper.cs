using System.Configuration;
using System.Data.SqlClient;

namespace SmartSchool.Models.DataAccessLayer
{
    static class DALHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}