using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Reposiroty.Entity.Logger;

namespace PiggyBank.Reposiroty.Configuration.ErrorConfiguration
{
    public sealed class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> e)
        {
            e.ToTable("ErrorLogs");

            e.HasKey(x => x.Id);

            e.Property(x => x.TraceId).HasMaxLength(100);
            e.Property(x => x.Method).HasMaxLength(16);
            e.Property(x => x.Path).HasMaxLength(500);
            e.Property(x => x.RemoteIp).HasMaxLength(60);
            e.Property(x => x.ExceptionType).HasMaxLength(300);
            e.Property(x => x.Message).HasMaxLength(2000);

            // optional: make some fields required
            e.Property(x => x.TraceId).IsRequired();
            e.Property(x => x.Method).IsRequired();
            e.Property(x => x.Path).IsRequired();
            e.Property(x => x.ExceptionType).IsRequired();
            e.Property(x => x.Message).IsRequired();
        }
    }
}

