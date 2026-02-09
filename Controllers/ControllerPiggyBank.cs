using Microsoft.AspNetCore.Mvc;
using PiggyBank.Interfaces;


namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("ThePiggyBank")]
  
    public class ControllerPiggyBank : ControllerBase
    {
       
        private readonly IPiggyBank _piggyBank;
        public ControllerPiggyBank( IPiggyBank piggyBank )
        {            
            _piggyBank = piggyBank;
        }
        

        [HttpPost("Add cash")]
        public string AddCash(string mail, decimal cash)
        {           
           var addedCash = _piggyBank.AddCash(mail,cash);
            return "you transferred - " + addedCash;
        }
        [HttpGet("Get Cash")]
        public decimal GetCash()
        {
            return _piggyBank.GetCash();
        }
        [HttpGet ("Show Cash")]
        public string ShowCash(string mail)
        {
            return _piggyBank.ShowCash(mail);
        }        
    }
}
