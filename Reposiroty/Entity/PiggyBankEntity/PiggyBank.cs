
using PiggyBank.Reposiroty.Entity.UserEntity;

namespace PiggyBank.Reposiroty.Entity.PiggyBankEntity
{
    public class PiggyBankClass
    {
        public int Id { get; set; }
        public int DepositorId { get; set; }        
        public decimal CashAmount { get; set; }
        public DateTime? DebitTime { get; set; } 
        public DateTime DepositTime { get; set; } 
        public User Depositor { get; set; } = null!;
    }
}
