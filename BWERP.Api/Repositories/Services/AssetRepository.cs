using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.AssetHistory;
using BWERP.Models.Comment;
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
				string insertSql = @"INSERT INTO Assets(Id, Name, SerialNo, CategoryId, Location, StatusId, Description, PurchaseDate, PurchasePrice, AssignedTo, CreatedDate, CreatedUser) 
                             VALUES(@Id, @Name, @SerialNo, @CategoryId, @Location, @StatusId, @Description, @PurchaseDate, @PurchasePrice, @AssignedTo, @CreatedDate, @CreatedUser)";

				await sqlconMain.ExecuteAsync(insertSql, new
				{
					asset.Id,
					asset.Name,
					asset.SerialNo,
					asset.CategoryId,
					asset.Location,
					asset.StatusId,
					asset.Description,
					asset.PurchaseDate,
					asset.PurchasePrice,
					asset.AssignedTo,
					asset.CreatedDate,
					asset.CreatedUser
				});
			}
			catch (Exception ex)
			{
				//This preserves the original stack trace.
				throw;
			}

			return asset;
		}

		public async Task<AssetHistory> CreateHistory(AssetHistory asset)
		{
			try
			{
				string insertSql = @"INSERT INTO AssetHistory(AssetId, Description, Date, Event)
                             VALUES(@AssetId, @Description, @Date, @Event)";

				await sqlconMain.ExecuteAsync(insertSql, new
				{
					asset.Id,
					asset.AssetId,
					asset.Description,
					asset.Date,
					asset.Event
				});
			}
			catch (Exception ex)
			{
				//This preserves the original stack trace.
				throw;
			}

			return asset;
		}

		public async Task<Asset> GetLatestId()
		{
			try
			{
				var query = @"SELECT TOP 1 Id FROM Assets 
                      ORDER BY SUBSTRING(Id, 7, 2) DESC,
                               SUBSTRING(Id, 5, 2) DESC,
                               SUBSTRING(Id, 3, 2) DESC,
                               CAST(SUBSTRING(Id, 9, 2) AS INT) DESC";
				var data = await sqlconMain.QueryAsync<Asset>(query);
				return data.FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Asset>  GetAssetById(string id)
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
		//GET HISTORY
		public async Task<PagedList<AssetHistoryView>> GetAssetHistory(int pageNumber, int pageSize)
		{
			try
			{
				var query = @"select Id, AssetId, Description, Date, Event from AssetHistory order by Date desc";

				var data = await sqlconMain.QueryAsync<AssetHistoryView>(query);

				var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
				var totalCount = data.Count();

				return new PagedList<AssetHistoryView>(pagedData, totalCount, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<List<AssetStatus>> GetAssetStatus()
		{
			try
			{
				var query = "select Id, Name from AssetStatus";
				var data = await sqlconMain.QueryAsync<AssetStatus>(query);
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
				string updateSql = @"UPDATE Assets SET Name=@Name,SerialNo=@SerialNo, Location=@Location, StatusId=@StatusId, Description=@Description, PurchasePrice=@PurchasePrice, PurchaseDate=@PurchaseDate, AssignedTo=@AssignedTo
                             WHERE Id=@Id";

				await sqlconMain.ExecuteAsync(updateSql, new
				{
					asset.Id,
					asset.Name,
					asset.SerialNo,
					asset.Location,
					asset.StatusId,
					asset.Description,
					asset.PurchasePrice,
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
