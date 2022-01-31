using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdMegasoft.Infrastructure.Persistence.Configurations
{
    public class GroupRolesConfiguration : IEntityTypeConfiguration<GroupRoles>
    {
        public void Configure(EntityTypeBuilder<GroupRoles> builder)
        {
            builder.Property(gr => gr.GroupId)
                .IsRequired();

            builder.Property(gr => gr.RoleId)
                .IsRequired();
        }
    }
}
