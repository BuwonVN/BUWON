using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class AssetRepository : IAssetRepository
	{
		private readonly IConfiguration _configuration;
		private readonly string _dbMain;
		public AssetRepository(IConfiguration configuration)
        {
			_configuration = configuration;
			_dbMain = _configuration.GetConnectionString("MainDBDatabase");
		}
		private IDbConnection sqlconMain => new SqlConnection(_dbMain);
		public async Task<Asset> Create(Asset asset)
		{
			try
			{
				string insertSql = @"INSERT INTO Assets(Id, Name, CategoryId, Location, StatusId, Description, PurchaseDate, PurchasePrice, AssignedTo) 
                             VALUES(@Id, @Name, @CategoryId, @Location, @StatusId, @Description, @PurchaseDate, @PurchasePrice, @AssignedTo)";

				await sqlconMain.ExecuteAsync(insertSql, new
				{
					asset.Id,
					asset.Name,
					asset.CategoryId,
					asset.Location,
					asset.StatusId,
					asset.Description,
					asset.PurchaseDate,
					asset.PurChasePrice,
					asset.AssignedTo
				});
			}
			catch (Exception ex)
			{
				//This preserves the original stack trace.
				throw;
			}

			return asset;
		}

		public async Task<Asset> GetAssetById(string id)
		{
			try
			{
				string query = "SELECT * FROM Assets WHERE Id = @Id";
				return await sqlconMain.QueryFirstOrDefaultAsync<Asset>(query, new { Id = id });
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<AssetCategoryView>> GetAssetCategory()
		{
			try
			{
				var query = "select Id, Code, Name from AssetCategories";
				var data = await sqlconMain.QueryAsync<AssetCategoryView>(query);
				return data.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch)
		{
			try
			{
				var parameters = new
				{
					assetSearch.Name,
					assetSearch.Location,
					CategoryId = (object)assetSearch.CategoryId ?? DBNull.Value,
					StatusId = (object)assetSearch.StatusId ?? DBNull.Value
				};
				var data = await sqlconMain.QueryAsync<AssetView>("spAssetGetAll", parameters, commandType: CommandType.StoredProcedure);

				var pagedData = data.Skip((assetSearch.PageNumber - 1) * assetSearch.PageSize).Take(assetSearch.PageSize).ToList();
				var totalCount = data.Count();

				return new PagedList<AssetView>(pagedData, totalCount, assetSearch.PageNumber, assetSearch.PageSize);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Asset> Update(Asset asset)
		{
			try
			{
				string updateSql = @"UPDATE Assets SET Name=@Name, CategoryId=@CategoryId, Location=@Location, StatusId=@StatusId, Description=@Description, PurchasePrice=@PurchasePrice, PurchaseDate=@PurchaseDate, AssignedTo=@AssignedTo
                             WHERE Id=@Id";

				await sqlconMain.ExecuteAsync(updateSql, new
				{
					asset.Id,
					asset.Name,
					asset.CategoryId,
					asset.Location,
					asset.StatusId,
					asset.Description,
					asset.PurChasePrice,
					asset.PurchaseDate,
					asset.AssignedTo
				});
			}
			catch (Exception ex)
			{
				throw;
			}

			return asset;
		}
	}
}
