using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Reposiroty.Entity.UserEntity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();

        // ✅ One User -> Many PiggyBank deposits
        builder.HasMany(u => u.PiggyBankDeposit)
               .WithOne(p => p.Depositor)          // navigation in PiggyBank
               .HasForeignKey(p => p.DepositorId)  // FK in PiggyBank
               .OnDelete(DeleteBehavior.Restrict);
    }
}
