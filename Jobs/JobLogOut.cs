using PiggyBank.Reposiroty.RepositoryInterface;

namespace Jobs.JobClass.LogOut
{
    public class JobLogOut
    {
        public readonly IPiggyBankDbContext _db;
        public JobLogOut(IPiggyBankDbContext db)
        {
            _db = db;
        }

        public Task Run()
        {
            var cutoff = DateTime.Now.AddMinutes(-10);

            var usersToLogout = _db.Users
                .Where(u => u.IsActive
                            && u.ActivationTime != null
                            && u.ActivationTime <= cutoff)
                .ToList();   // ✅ here

            foreach (var u in usersToLogout)
            {
                u.IsActive = false;
                u.ActivationTime = null;
            }

            _db.SaveChanges(); // if you have it
            return Task.CompletedTask;
        }
    }
}

