using BWERP.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Configurations
{
    public class AppPermissionConfiguration : IEntityTypeConfiguration<AppPermission>
    {
        public void Configure(EntityTypeBuilder<AppPermission> builder)
        {
            builder.ToTable("AppPermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
