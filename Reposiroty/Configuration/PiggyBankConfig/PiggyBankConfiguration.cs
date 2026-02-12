using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Reposiroty.Entity.PiggyBankEntity;


namespace PiggyBank.Reposiroty.Configuration.PiggyBankConfig
{
    public class PiggyBankConfiguration : IEntityTypeConfiguration<PiggyBankClass>
    {
        public void Configure(EntityTypeBuilder<PiggyBankClass> builder)
        {
            builder.ToTable("PiggyBank", t => t.ExcludeFromMigrations());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.CashAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.DepositTime)
                   .IsRequired();

            builder.Property(x => x.DepositorId)
                   .IsRequired();            
        }
    }
}

