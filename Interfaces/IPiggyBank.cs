namespace PiggyBank.Interfaces
{
    public interface IPiggyBank
    {
        public decimal AddCash(string mail, decimal cash);
        public string ShowCash(string mail);
        public decimal GetCash();       
    }
}
