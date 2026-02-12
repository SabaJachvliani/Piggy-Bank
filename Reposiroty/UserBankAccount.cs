using PiggyBank.Interfaces;
using PiggyBank.Reposiroty.RepositoryInterface;

namespace PiggyBank.Reposiroty
{
    public class UserBankAccount : IUserBankAccount
    {
        public readonly IPiggyBankDbContext _db;
        public UserBankAccount(IPiggyBankDbContext db)
        {
            _db = db;
        }
        public decimal BanckAccoubtAdd(string mail, decimal amount)
        {
            if (amount <= 0.0m)
            {
                throw new ArgumentException("enter the money");
            }
             var user = _db.Users.FirstOrDefault(x => x.Email == mail);

            if (user == null || user.IsActive == false)
            {
                throw new Exception(" please logg in ");
            }
            
            user.BankAccount += amount;
            _db.SaveChanges();
            return user.BankAccount;
        }

        public decimal ShowCash(string userMail)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == userMail);
            if (user == null && user.IsActive == false)
            {
                throw new Exception("user does not exist");
            }

            _db.SaveChanges();
            return user.BankAccount;
        }

        public (decimal,string) TransferMoneyToUser(string userMail, string myMail, decimal amount)
        {            
            var users = _db.Users.Where(x => x.Email == userMail || x.Email == myMail).ToList();

            if (users.Count != 2)
            {
                throw new Exception("there is no user");
            }

            var user = users.FirstOrDefault(x => x.Email == userMail);
            var me = users.FirstOrDefault(x => x.Email == myMail);

            if (me.IsActive == false)
            {
                throw new Exception("please logg in");
            }                       
            if(amount <= 0.0m)
            {
                throw new Exception("enter the money");
            }
            if(me.BankAccount < amount)
            {
                throw new Exception("you don't have enough money");
            }

            me.BankAccount -= amount;
            user.BankAccount += amount;
            _db.SaveChanges();
            return (amount, user.Name);
        }
    }
}
