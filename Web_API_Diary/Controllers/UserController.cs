using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_UI.BusinessLayer;
using Web_UI.DataAccessLayer;

namespace Web_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("get-user/{userId}")]

        public Users GetUser()
        {
            List<UsersFakeData> users = new List<UsersFakeData>();
            MyUserBusinessCode myBusinessCode = new MyUserBusinessCode();
            users = myBusinessCode.ConvertUserEntityToDTO();
            
            int userId = -1;
            string userName = "", password = "", name = "", surname = "", securityQuestion = "", securityAnswer = "";
            DateTime dateOfRegister = DateTime.MinValue;

            foreach (UsersFakeData item in users)
            {
                userId = item.userId;
                userName = item.userName;
                password = item.password;
                name = item.name;
                surname = item.surname;
                securityQuestion = item.securityQuestion;
                securityAnswer = item.securityAnswer;
                dateOfRegister = item.dateOfRegister;
            }
            return new Users(userId, userName, password, name, surname, securityQuestion, securityAnswer, dateOfRegister);
        }

        [HttpPost("add-user")]

        public IActionResult AddUser([FromBody] Users user)
        {
            if (user == null)  return BadRequest("Invalid Data"); 

            MyUserBusinessCode businessCode = new MyUserBusinessCode();
            businessCode.AddUser(user);

            return Ok("User Added Succesfully");
        }

        [HttpPut("update-user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Data");
            }

            MyUserBusinessCode businessCode = new MyUserBusinessCode();
            bool isUpdated = businessCode.UpdateUser(userId, user);

            if (isUpdated) return Ok("User Updated Successfully");
            else return NotFound("User not found");          
        }

        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            MyUserBusinessCode businessCode = new MyUserBusinessCode();
            bool isDeleted = businessCode.DeleteUser(userId);

            if (isDeleted)
            {
                return Ok("User Deleted Successfully");
            }
            else
            {
                return NotFound("User not found");
            }
        }

    }
}
