namespace PiggyBank.Interfaces
{
    public interface IUserBankAccount
    {
        public decimal BanckAccoubtAdd(string mail, decimal amount);
        public (decimal,string) TransferMoneyToUser(string userMail, string myMail, decimal amount);

        public decimal ShowCash(string userMail);
    }
}
