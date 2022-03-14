using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.CompanyName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(c => c.CompanyName)
                .IsUnique();

            builder.Property(c => c.Denomination)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
