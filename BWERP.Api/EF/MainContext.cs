using BWERP.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BWERP.Api.Configurations;
using Task = BWERP.Api.Entities.Task;
using BWERP.Api.Extensions;

namespace BWERP.Api.EF
{
	public class MainContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		public MainContext(DbContextOptions<MainContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //Identity Database
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new DailyReportConfiguration());
			modelBuilder.ApplyConfiguration(new CommentConfiguration());

			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AppMenuConfiguration());
            modelBuilder.ApplyConfiguration(new AppMenuPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleMenuConfiguration());

			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

			//Data Seeding  
			modelBuilder.Seed();
		}

		public DbSet<Comment> Comments { get; set; }						
		public DbSet<DailyReport> DailyReports { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<AppMenu> Menus { get; set; }
        public DbSet<AppRoleMenu> AppRoleMenus { get; set; }
    }
}
