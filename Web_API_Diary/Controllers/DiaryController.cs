using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_UI.BusinessLayer;
using Web_UI.DataAccessLayer;
using Web_UI.Models;

namespace Web_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        [HttpGet("get-diary/")]
        public Diarys GetDiary()
        {
            List<DiaryFakeData> diarys = new List<DiaryFakeData>();
            MyDiaryBusinessCode diaryBusinessCode = new MyDiaryBusinessCode();
            diarys = diaryBusinessCode.ConvertDiaryEntityToDTO();

            int diaryId = -1, userId = 1;
            string diary = "";
            DateTime date = DateTime.MinValue;
            DateTime updateDate = DateTime.MinValue;
            Diarys[] allDiarys = new Diarys[diarys.Count];
            foreach (DiaryFakeData item in diarys)
            {
                diaryId = item.diaryId;
                userId = item.userId;
                diary = item.diary;
                date = item.date;
                updateDate = item.updateDate;
                return new Diarys(diaryId, diary, date, updateDate, userId);
            }
            return new Diarys(diaryId,diary,date,updateDate,userId);
        }


        [HttpGet("get-all-diaries/{userId}")]
        public List<Diarys> GetAllDiaries()
        {
            List<DiaryFakeData> diaryDataList = new List<DiaryFakeData>();
            MyDiaryBusinessCode diaryBusinessCode = new MyDiaryBusinessCode();
            diaryDataList = diaryBusinessCode.ConvertDiaryEntityToDTO();

            List<Diarys> allDiaries = new List<Diarys>();

            foreach (DiaryFakeData item in diaryDataList)
            {
                int diaryId = item.diaryId;
                int userId = item.userId;
                string diary = item.diary;
                DateTime date = item.date;
                DateTime updateDate = item.updateDate;

                allDiaries.Add(new Diarys(diaryId, diary, date, updateDate, userId));
            }
            return allDiaries;
        }

        [HttpPost("add-diary")]
        public IActionResult AddDiary([FromBody] Diarys diary)
        {
            if (diary == null) return BadRequest("Invalid Data");

            MyDiaryBusinessCode businessCode = new MyDiaryBusinessCode();
            businessCode.AddDiary(diary);

            return Ok("Diary Added Successfully");
        }


        [HttpPut("update-diary/{diaryId}")]
        public IActionResult UpdateDiary(int diaryId, [FromBody] Diarys diary)
        {
            if (diary == null) return BadRequest("Invalid Data");

            MyDiaryBusinessCode businessCode = new MyDiaryBusinessCode();
            bool isUpdated = businessCode.UpdateDiary(diaryId, diary);

            if (isUpdated) return Ok("Diary Updated Successfully");
            else return NotFound("Diary not found");
        }


        [HttpDelete("delete-diary/{diaryId}")]
        public IActionResult DeleteDiary(int diaryId)
        {
            MyDiaryBusinessCode businessCode = new MyDiaryBusinessCode();
            bool isDeleted = businessCode.DeleteDiary(diaryId);

            if (isDeleted) return Ok("Diary Deleted Successfully");            
            else return NotFound("Diary not found");

        }

        [HttpDelete("delete-all-diarys{userId}")]
        public IActionResult DeleteAllDiary(int userId)
        {
            MyDiaryBusinessCode businessCode = new MyDiaryBusinessCode();
            bool isDeletedAll = businessCode.DeleteAllDiarys(userId);

            if (isDeletedAll) return Ok("All Diarys Deleted Succesfully");
            else return NotFound("Diary Not Found.");
        }    
    }
}
