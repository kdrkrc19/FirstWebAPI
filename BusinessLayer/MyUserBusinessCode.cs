using System.Data.SqlClient;
using Web_UI.DataAccessLayer;

namespace Web_UI.BusinessLayer
{
    public class MyUserBusinessCode
    {
        public List<Users> GetUserData()
        {
            List<Users> users = new List<Users>();
            DbConnection dbConnection = new DbConnection();
            SqlConnection sqlConnection = dbConnection.OpenConnection();
            string query = "SELECT * FROM Users";
            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            SqlDataReader reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Users user = new Users(
                    (int)reader["UserId"],
                    reader["UserName"].ToString(),
                    reader["Password"].ToString(),
                    reader["Name"].ToString(),
                    reader["Surname"].ToString(),
                    reader["SecurityQuestion"].ToString(),
                    reader["SecurityAnswer"].ToString(),
                    (DateTime)reader["DateOfRegister"]
                    );
                users.Add(user);
            }
            return users;
        }

        public List<UsersFakeData> ConvertUserEntityToDTO()
        {
            List<Users> users = GetUserData();
            List<UsersFakeData> usersFakeDatas = new List<UsersFakeData>();

            foreach (Users item in users)
            {
                UsersFakeData usersFakeData = new UsersFakeData();
                usersFakeData.userId = item.userId;
                usersFakeData.userName = item.userName;
                usersFakeData.password = item.password;
                usersFakeData.name = item.name;
                usersFakeData.surname = item.surname;
                usersFakeData.securityQuestion = item.securityQuestion;
                usersFakeData.securityAnswer = item.securityAnswer;
                usersFakeData.dateOfRegister = item.dateOfRegister;
                usersFakeDatas.Add(usersFakeData);
            }
            return usersFakeDatas;
        }

        public void AddUser(Users user)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection sqlConnection = dbConnection.OpenConnection();

            string query = "INSERT INTO Users (UserName, Password, Name, Surname, SecurityQuestion, SecurityAnswer, DateOfRegister) " +
                           "VALUES (@UserName, @Password, @Name, @Surname, @SecurityQuestion, @SecurityAnswer, @DateOfRegister)";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserName", user.userName);
            sqlCommand.Parameters.AddWithValue("@Password", user.password);
            sqlCommand.Parameters.AddWithValue("@Name", user.name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.surname);
            sqlCommand.Parameters.AddWithValue("@SecurityQuestion", user.securityQuestion);
            sqlCommand.Parameters.AddWithValue("@SecurityAnswer", user.securityAnswer);
            sqlCommand.Parameters.AddWithValue("@DateOfRegister", user.dateOfRegister);

            sqlCommand.ExecuteNonQuery();

        }

        public bool UpdateUser(int userId,Users user)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection connection = dbConnection.OpenConnection();

            string query = "UPDATE Users SET " +
                   "UserName = @UserName, " +
                   "Password = @Password, " +
                   "Name = @Name, " +
                   "Surname = @Surname, " +
                   "SecurityQuestion = @SecurityQuestion, " +
                   "SecurityAnswer = @SecurityAnswer, " +
                   "DateOfRegister = @DateOfRegister " +
                   "WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);

            sqlCommand.Parameters.AddWithValue("@UserName", user.userName);
            sqlCommand.Parameters.AddWithValue("@Password", user.password);
            sqlCommand.Parameters.AddWithValue("@Name", user.name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.surname);
            sqlCommand.Parameters.AddWithValue("@SecurityQuestion", user.securityQuestion);
            sqlCommand.Parameters.AddWithValue("@SecurityAnswer", user.securityAnswer);
            sqlCommand.Parameters.AddWithValue("@DateOfRegister", user.dateOfRegister);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool DeleteUser(int userId)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection connection = dbConnection.OpenConnection();

            string query = "DELETE FROM Users WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}