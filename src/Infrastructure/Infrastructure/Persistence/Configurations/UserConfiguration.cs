using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.AccountName)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasIndex(u => u.AccountName)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired();
        }
    }
}
