using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Configurations
{
	public class TaskConfiguration : IEntityTypeConfiguration<BWERP.Api.Entities.Task>
	{
		public void Configure(EntityTypeBuilder<BWERP.Api.Entities.Task> builder)
		{
			builder.ToTable("Tasks");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.Property(x => x.AssigneeId).IsRequired(false);

			builder.HasOne(x => x.Assignee).WithMany(x => x.Tasks).HasForeignKey(x => x.AssigneeId);

		}
	}
}
