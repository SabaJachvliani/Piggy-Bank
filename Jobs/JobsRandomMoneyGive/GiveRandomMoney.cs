using Microsoft.EntityFrameworkCore;
using PiggyBank.Reposiroty.RepositoryInterface;

namespace PiggyBank.Jobs.JobsRandomMoneyGive
{
    public class GiveRandomMoney
    {

        public readonly IPiggyBankDbContext _db;
        public GiveRandomMoney(IPiggyBankDbContext db)
        {
            _db = db;

        }

        public Task Run()
        {                        
            var piggies = _db.PiggyBanks
                .Include(p => p.Depositor)          
                .Where(p => p.DebitTime == null && p.Depositor.DeleteTime == null)
                .ToList();                          

            var grouped = piggies
                .GroupBy(p => p.DepositorId)
                .Select(g => new
                {
                    User = g.First().Depositor,     
                    PiggyBanks = g.ToList()         
                })
                .ToList();
           
            if (grouped.Count == 0)
                return Task.CompletedTask;

            var randomIndex = Random.Shared.Next(grouped.Count);
            var winnerGroup = grouped[randomIndex];

            var winnerUser = winnerGroup.User;

            winnerUser.BankAccount += piggies.Sum(x => x.CashAmount);

            var time = DateTime.Now;

            foreach ( var g in piggies) 
            {
                g.DebitTime = time;
            }
            
            _db.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
