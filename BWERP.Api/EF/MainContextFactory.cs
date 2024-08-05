using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.EF
{
	public class MainContextFactory : IDesignTimeDbContextFactory<MainContext>
	{
		public MainContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("MainDBDatabase");

			var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new MainContext(optionsBuilder.Options);
		}
	}
}
