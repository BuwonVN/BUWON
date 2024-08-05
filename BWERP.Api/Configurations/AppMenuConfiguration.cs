using BWERP.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Configurations
{
    public class AppMenuConfiguration : IEntityTypeConfiguration<AppMenu>
    {
        public void Configure(EntityTypeBuilder<AppMenu> builder)
        {
            builder.ToTable("AppMenus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(50);                      
            builder.Property(x => x.isEnable).IsRequired().HasMaxLength(50);           
        }
    }
}
