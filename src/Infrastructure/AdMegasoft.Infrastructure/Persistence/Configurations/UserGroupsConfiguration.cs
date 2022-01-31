using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdMegasoft.Infrastructure.Persistence.Configurations
{
    public class UserGroupsConfiguration : IEntityTypeConfiguration<UserGroups>
    {
        public void Configure(EntityTypeBuilder<UserGroups> builder)
        {
            builder.Property(ug => ug.UserId)
                .IsRequired();

            builder.Property(ug => ug.GroupId)
                .IsRequired();
        }
    }
}
