using Microsoft.EntityFrameworkCore;
using PiggyBank.Reposiroty.Entity.Logger;
using PiggyBank.Reposiroty.Entity.PiggyBankEntity;
using PiggyBank.Reposiroty.Entity.UserEntity;

namespace PiggyBank.Reposiroty.RepositoryInterface
{
    public interface IPiggyBankDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<PiggyBankClass> PiggyBanks { get; }
        public DbSet<ErrorLog> ErrorLogs { get; }
        int SaveChanges();
    }
}
