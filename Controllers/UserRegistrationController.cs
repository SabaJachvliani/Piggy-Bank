using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PiggyBank.Interfaces;
using PiggyBank.Models.DTO;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("UserRegistration")]
    public class UserRegistrationController : ControllerBase
    {
        public readonly IUserAuTh _usersRepository;

        public UserRegistrationController(IUserAuTh usersRepository) => _usersRepository = usersRepository;

        [HttpPost("Registration")]
        [EnableRateLimiting("3-per-minute")]
        public IActionResult UserRegistraition(UserRegistrationModel user)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = _usersRepository.UserRegistraition(user);
            return Ok(result);
        }

        [HttpGet("Log inn")]
        public IActionResult LogIn(string mail, string password)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var result = _usersRepository.LogIn(mail, password);
            return Ok(result);
        }

        [HttpGet("log out")]
        public IActionResult LogOut(string mail)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            bool result = _usersRepository.LogOut(mail);
            return Ok(result);
        }

        [HttpPost("Change password")]
        public IActionResult ChangePassword(string mail, string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
             _usersRepository.ChangePassword(mail, newPassword);

            return Ok(" Password changed ");
        }

        [HttpDelete("delete user")]
        public IActionResult Delete(string mail)
        {
            _usersRepository.DeleteUser(mail);

            return Ok("user was deleted");
        }
    }
}
