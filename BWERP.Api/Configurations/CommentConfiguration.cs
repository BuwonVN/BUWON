using BWERP.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWERP.Api.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comments");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn();

			builder.Property(x => x.Content).IsRequired();
			builder.Property(x => x.CreatedBy).IsRequired();
			builder.Property(x => x.UpdatedBy).IsRequired();
			builder.Property(x => x.CreatedDate).IsRequired();
			builder.Property(x => x.UpdatedDate	).IsRequired();
			builder.HasOne(x => x.Department).WithMany(x => x.Comments).HasForeignKey(x => x.DepartmentId);
		}
	}
}
