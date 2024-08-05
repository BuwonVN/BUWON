using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Production;
using BWERP.Models.WeelkyReport;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class ProductionRepository : IProductionRepository
	{
		private readonly IConfiguration _configuration;
		private readonly string _constrMES;

		public ProductionRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			_constrMES = configuration.GetConnectionString("MESDatabase");
		}
		private IDbConnection sqlconMES => new SqlConnection(_constrMES);

		public async Task<IEnumerable<ProductionViewDto>> GetProductionStatus_Dashboard(string Dept, DateTime scanDate)
		{
			using (var db = sqlconMES)
			{
				var parameters = new { Dept, scanDate };
				return await db.QueryAsync<ProductionViewDto>("spProductionStatus_Dashboard", parameters, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
