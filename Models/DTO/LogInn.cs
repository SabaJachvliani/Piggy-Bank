using FluentValidation;

namespace PiggyBank.Models.DTO
{
    public class LogInn 
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
