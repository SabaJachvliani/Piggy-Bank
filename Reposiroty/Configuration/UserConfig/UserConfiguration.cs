using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Reposiroty.Entity.UserEntity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("Users", t => t.ExcludeFromMigrations());

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasMany(u => u.PiggyBankDeposit)
               .WithOne(p => p.Depositor)          
               .HasForeignKey(p => p.DepositorId)  
               .OnDelete(DeleteBehavior.Restrict);
    }
}
