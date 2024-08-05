using BWERP.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Extensions
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
			var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
			modelBuilder.Entity<AppRole>().HasData(new AppRole
			{
				Id = roleId,
				Name = "admin",
				NormalizedName = "admin",
				Description = "Administrator"
			});

			var hasher = new PasswordHasher<AppUser>();
			modelBuilder.Entity<AppUser>().HasData(new AppUser
			{
				Id = adminId,
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "orib@a2-cloud.com",
				NormalizedEmail = "ORIB@A2-CLOUD.COM",
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "abc@123"),
				SecurityStamp = Guid.NewGuid().ToString(),
				FirstName = "Hien",
				LastName = "Bui",
			});

			modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = roleId,
				UserId = adminId
			});

            modelBuilder.Entity<AppPermission>().HasData(
               new AppPermission() { Id = 1, Name = "View" },
               new AppPermission() { Id = 2, Name = "Create" },
               new AppPermission() { Id = 3, Name = "Update" },
               new AppPermission() { Id = 4, Name = "Delete" }
               );
        }
	}
}
