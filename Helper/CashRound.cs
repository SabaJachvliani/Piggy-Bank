namespace PiggyBank.Helper
{
    public static class CashRound
    {
        public static decimal MathRound(decimal cash)
        {
            return Math.Round(cash, 2);
        }
    }

}
