using BWERP.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Configurations
{
    public class AppRoleMenuConfiguration : IEntityTypeConfiguration<AppRoleMenu>
    {
        public void Configure(EntityTypeBuilder<AppRoleMenu> builder)
        {
            builder.ToTable("AppRoleMenus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.AppMenu).WithMany(x => x.AppRoleMenus).HasForeignKey(x => x.MenuId);
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppRoleMenus).HasForeignKey(x => x.RoleId);
        }
    }
}
