using PiggyBank.Models;
using PiggyBank.Reposiroty.Entity.UserEntity;

namespace PiggyBank.Helper
{
    public static class AtorisationCheck
    {
        public static bool IsUserActive(string mail, List<User> users)
        {
            var user = users.FirstOrDefault(x => x.Email == mail );            
            if (user != null && user.IsActive == true)
            {
                return true;
            }
            return false;
        }
    }
}
