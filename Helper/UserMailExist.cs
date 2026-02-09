using PiggyBank.Reposiroty.Entity.UserEntity;

namespace PiggyBank.Helper
{
    public static class UserMailExist
    {
        public static bool MailExistChek(string mail, List<User> users)
        {
            var user = users.FirstOrDefault(x => x.Email == mail);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
