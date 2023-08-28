namespace Web_UI.Models
{
    public class UserModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }
        public DateTime? dateOfRegister { get; set; }
    }
}
