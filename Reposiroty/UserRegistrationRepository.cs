using Microsoft.AspNetCore.Identity;
using PiggyBank.Interfaces;
using PiggyBank.Models;
using PiggyBank.Models.DTO;
using PiggyBank.Reposiroty.Entity.UserEntity;
using PiggyBank.Reposiroty.RepositoryInterface;

namespace PiggyBank.Reposiroty
{
    public class UserRegistrationRepository : IUserAuTh
    {
        public readonly IPasswordHasher<User> _passwordHasher;

        

        public readonly IPiggyBankDbContext _db; 
        public UserRegistrationRepository(IPiggyBankDbContext db, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }
        
        public int UserId = 1;
        
        public bool LogIn(string mail, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == mail && x.DeleteTime == null);

            if(user == null)
            {
                throw new Exception(" ther is now such account");
            }
           
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("wrong password");

            user.IsActive = true;
            user.ActivationTime = DateTime.Now;
            

            _db.SaveChanges();

            return true;
        }

        public bool LogOut(string mail)
        {            
            var user = _db.Users.FirstOrDefault(x => x.Email == mail);

            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            user.ActivationTime = null;

            _db.SaveChanges();

            return true;            
        } 
        
        public string UserRegistraition(UserRegistrationModel user)
        {
            var isUserExist = _db.Users.FirstOrDefault(x => x.Email == user.Email && x.DeleteTime == null);

            if (isUserExist != null)
            {
                throw new Exception("user allready exist");
            }

            User currentUser = new User
            { Name = user.Name,
              Email = user.Email,               
              RegistrationTime = DateTime.Now
            };

            var hashPassword = _passwordHasher.HashPassword(currentUser, user.Password);
            currentUser.Password = hashPassword;

            _db.Users.Add(currentUser);

            _db.SaveChanges();

             return " registration was saccesfull ";            
        }

        public void ChangePassword(string mail, string newPassword)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == mail && x.DeleteTime == null );

            if (user == null || user.IsActive == false)
            {
                throw new Exception(" please logg in ");
            }

            var hashPassword = _passwordHasher.HashPassword(user, newPassword);
            user.Password = hashPassword;
            
            _db.SaveChanges();           
        }

        public void DeleteUser(string mail)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == mail);

            if (user == null || user.IsActive == false)
            {
                throw new Exception(" please logg in ");
            }

            user.IsActive = false;
            user.DeleteTime = DateTime.Now;

            _db.SaveChanges();         
        }
    }
}
