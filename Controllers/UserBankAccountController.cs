using Microsoft.AspNetCore.Mvc;
using PiggyBank.Interfaces;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("UserBankAccount")]
    public class UserBankAccountController : ControllerBase
    {
        private readonly IUserBankAccount _userBankAccount;
        public UserBankAccountController(IUserBankAccount userBankAccount)
        {
            _userBankAccount = userBankAccount;
        }

        [HttpPost(" Add cash ")]
        public string BanckAccoubtAdd(string mail, decimal amount)
        {
            var bankAccountAmount = _userBankAccount.BanckAccoubtAdd(mail, amount);
            return $"you add money:{amount} whole amount:{bankAccountAmount} "; 
        }
        [HttpPost("Money transfer")]

        public string TransferMoneyToUser(string userMail, string myMail, decimal amount)
        {
            var (transferdMoney, userName) = _userBankAccount.TransferMoneyToUser(userMail, myMail, amount);
            return "you transfered -" + transferdMoney + " to -" + userName ;
        }
        [HttpGet("Show cash")]
        public string ShowCash(string userMail)
        {
            var result = _userBankAccount.ShowCash(userMail);
            return "Your corrent amount - " + result;
        }





    }

}
