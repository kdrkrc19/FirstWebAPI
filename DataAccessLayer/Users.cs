namespace Web_UI.DataAccessLayer
{
    public class Users
    {
        public int userId {get;set;}
        public string userName { get;set;}
        public string password { get;set;}
        public string name { get;set;}
        public string surname { get;set;}
        public string securityQuestion { get;set;}
        public string securityAnswer { get;set;}
        public DateTime dateOfRegister { get;set;}

        public Users(int userId, string userName, string password, string name, string surname, string securityQuestion, string securityAnswer, DateTime dateOfRegister)
        {
            this.userId = userId;
            this.userName = userName;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.securityQuestion = securityQuestion;
            this.securityAnswer = securityAnswer;
            this.dateOfRegister = dateOfRegister;
        }

    }
}
