using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_UI.BusinessLayer;
using Web_UI.DataAccessLayer;

namespace BusinessLayer
{
    public class MyDiaryBusinessCode
    {
        public List<Diarys> GetDiaryData()
        {
            List<Diarys> diarys = new List<Diarys>();
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            string query = "SELECT * FROM Diarys";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            SqlDataReader reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Diarys diary = new Diarys(
                    (int)reader["DiaryId"],
                    reader["Diary"].ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["UpdateDate"],
                    (int)reader["UserId"]
                    );
                diarys.Add(diary);
            }
            return diarys;
        }

        public List<DiaryFakeData> ConvertDiaryEntityToDTO()
        {
            List<Diarys> diarys = GetDiaryData();
            List<DiaryFakeData> diaryFakeDatas = new List<DiaryFakeData>();

            foreach (Diarys item in diarys)
            {
                DiaryFakeData data = new DiaryFakeData();
                data.diary = item.diary;
                data.diaryId = item.diaryId;
                data.userId = item.userId;
                data.updateDate = item.updateDate;
                data.date = item.date;
                diaryFakeDatas.Add(data);
            }
            return diaryFakeDatas;
        }

        public void AddDiary(Diarys diary)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection sqlConnection = dbConnection.OpenConnection();

            string query = "INSERT INTO Diarys (Diary, Date, UpdateDate, UserId)" +
                           "VALUES (@Diary, @Date, @UpdateDate, @UserId)";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@Diary", diary.diary);
            sqlCommand.Parameters.AddWithValue("@Date", diary.date);
            sqlCommand.Parameters.AddWithValue("@UpdateDate", diary.updateDate);
            sqlCommand.Parameters.AddWithValue("@UserId", diary.userId);

            sqlCommand.ExecuteNonQuery();
        }

        public bool UpdateDiary(int diaryId,Diarys diary)
        {
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            string query = "UPDATE Diarys SET Diary = @Diary, Date = @Date, " +
                           "UpdateDate = @UpdateDate WHERE DiaryId = @DiaryId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);


            sqlCommand.Parameters.AddWithValue("@Diary", diary.diary);
            sqlCommand.Parameters.AddWithValue("@Date", diary.date);
            sqlCommand.Parameters.AddWithValue("@UpdateDate", diary.updateDate);
            sqlCommand.Parameters.AddWithValue("@DiaryId", diaryId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected > 0;

        }

        public bool DeleteDiary(int diaryId)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection connection = dbConnection.OpenConnection();

            string query = "DELETE FROM Diarys WHERE DiaryId = @DiaryId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@DiaryId", diaryId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
