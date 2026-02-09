using PiggyBank.Reposiroty.Entity.PiggyBankEntity;

namespace PiggyBank.Reposiroty.Entity.UserEntity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActivationTime { get; set; }
        public decimal BankAccount { get; set; }
        public DateTime? RegistrationTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public List<PiggyBankClass> PiggyBankDeposit { get; set; } = new();
        
    }
}
