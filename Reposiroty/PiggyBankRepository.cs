using PiggyBank.Helper;
using PiggyBank.Interfaces;
using PiggyBank.Models;
using PiggyBank.Reposiroty.Entity.PiggyBankEntity;
using PiggyBank.Reposiroty.Entity.UserEntity;
using PiggyBank.Reposiroty.RepositoryInterface;
namespace PiggyBank.Reposiroty
{
    public class PiggyBankRepository : IPiggyBank
    {
        public readonly IPiggyBankDbContext _db;
        public PiggyBankRepository(IPiggyBankDbContext db )
        {
            _db = db;            
        }

        public decimal AddCash(string mail, decimal cash)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == mail);
            
            if (user == null || user.IsActive == false)
            {
                throw new Exception(" please logg in ");
            }
            if (cash <= 0)
            {
                throw new Exception("enter the money");
            }
            if (user.BankAccount < cash)
            {
                throw new Exception("you dont have enugh money");
            }
            user.BankAccount -= cash;

            PiggyBankClass piggyBank = new PiggyBankClass();

            piggyBank.CashAmount += cash;
            piggyBank.DepositorId = user.Id;
            piggyBank.DepositTime = DateTime.Now;
            piggyBank.DebitTime = null;

            _db.PiggyBanks.Add(piggyBank);
            
            _db.SaveChanges();
            return cash;
        }

        public decimal GetCash()
        {
            return _db.PiggyBanks
             
              .Sum(x => x.CashAmount);

        }

        public string ShowCash(string mail)
        {
            var userList = _db.Users.ToList();

            bool IsActive = AtorisationCheck.IsUserActive(mail, userList);

            if (IsActive == false)
            {
                throw new Exception("you are not logged in ");
            }

            PiggyBankClass piggyBank = new PiggyBankClass();

            string allCash = piggyBank.CashAmount.ToString();

            return allCash + " - all amount of money ";
        }

       
    }
}
