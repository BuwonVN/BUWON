using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.ExpenseCategory;
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

		public Task<Asset> GetAssetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<AssetCategoryView>> GetAssetCategory()
		{
			try
			{
				var query = "select ID, Name from AssetCategories";
				var data = await sqlconMain.QueryAsync<AssetCategoryView>(query);
				return data.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch)
		{
			throw new NotImplementedException();
		}

		public async Task<Asset> Update(Asset asset)
		{
			try
			{
				string updateSql = @"UPDATE Expenses SET Name=@Name, CategoryId=@CategoryId, Location=@Location, StatusId=@StatusId, Description=@Description, PurchasePrice=@PurchasePrice, PurchaseDate=@PurchaseDate, AssignedTo=@AssignedTo
                             WHERE Id=@Id";

				await sqlconMain.ExecuteAsync(updateSql, new
				{
					asset.Id,
					asset.CategoryId,
					asset.Name,
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
				throw;
			}

			return asset;
		}
	}
}
