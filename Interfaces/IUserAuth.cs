using PiggyBank.Models.DTO;

namespace PiggyBank.Interfaces
{
    public interface IUserAuTh
    {        
        public string UserRegistraition(UserRegistrationModel user);
        public bool LogIn(string mail, string password);
        public bool LogOut(string mail);
        public void ChangePassword(string mail, string newPassword);
        public void DeleteUser(string mail);
    }
}
