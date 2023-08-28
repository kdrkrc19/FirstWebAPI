namespace Web_UI.DataAccessLayer
{
    public class Diarys
    {
        public int diaryId { get; set; }
        public string diary { get; set; }
        public DateTime date { get; set; }
        public DateTime updateDate { get; set; }
        public int userId { get; set; }

        public Diarys(int diaryId, string diary, DateTime date, DateTime updateDate, int userId) 
        {
            this.diaryId = diaryId;
            this.diary = diary;
            this.date = date;
            this.updateDate = updateDate;
            this.userId = userId;
        }
    }
}
