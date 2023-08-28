using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Web_UI.DataAccessLayer
{
    public class DbConnection
    {
        public SqlConnection OpenConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=DiaryV7;Trusted_Connection=True;");
            sqlConnection.Open();
            return sqlConnection;
        }      
        public SqlCommand CreateCommand(string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, OpenConnection());
            return sqlCommand;
        } 
    }
}

