using Microsoft.EntityFrameworkCore;
using PiggyBank.Reposiroty.Entity.Logger;
using PiggyBank.Reposiroty.Entity.PiggyBankEntity;
using PiggyBank.Reposiroty.Entity.UserEntity;
using PiggyBank.Reposiroty.RepositoryInterface;

namespace PiggyBank.Reposiroty.Database
{
    public class PiggyBankDbContext : DbContext, IPiggyBankDbContext
    {
        public PiggyBankDbContext(DbContextOptions<PiggyBankDbContext> options) : base(options) { }
        public DbSet<ErrorLog> ErrorLogs => Set<ErrorLog>();

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<PiggyBankClass> PiggyBanks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PiggyBankDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
