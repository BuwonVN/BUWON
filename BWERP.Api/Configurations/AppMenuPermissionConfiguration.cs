using BWERP.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Configurations
{
    public class AppMenuPermissionConfiguration : IEntityTypeConfiguration<AppMenuPermission>
    {
        public void Configure(EntityTypeBuilder<AppMenuPermission> builder)
        {
            builder.ToTable("AppMenuPermissions");
            builder.HasKey(x => x.RoleMenuId);
            builder.HasKey(x => x.PermissionId);

            builder.HasOne(x => x.AppPermission).WithMany(x => x.MenuPermissions).HasForeignKey(x => x.PermissionId);
            builder.HasOne(x => x.AppRoleMenu).WithMany(x => x.MenuPermissions).HasForeignKey(x => x.RoleMenuId);
        }
    }
}
