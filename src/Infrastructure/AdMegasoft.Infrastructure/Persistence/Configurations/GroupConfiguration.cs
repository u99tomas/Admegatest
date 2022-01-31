using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdMegasoft.Infrastructure.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(g => g.Name)
                .IsUnique();

            builder.Property(g => g.Description)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
