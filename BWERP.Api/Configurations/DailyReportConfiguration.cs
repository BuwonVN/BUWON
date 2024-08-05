using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BWERP.Api.Entities;

namespace BWERP.Api.Configurations
{
    public class DailyReportConfiguration : IEntityTypeConfiguration<DailyReport>
    {
        public void Configure(EntityTypeBuilder<DailyReport> builder)
        {
            builder.ToTable("DailyReports");
            builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn();
			builder.Property(x => x.TodayTask).IsRequired();
            builder.Property(x => x.TomorrowTask).IsRequired(false);

            builder.HasOne(x => x.AppUser).WithMany(x => x.DailyReports).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Department).WithMany(x => x.DailyReports).HasForeignKey(x => x.DepartmentId);
        }
    }
}
