using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.AssetHistory;
using BWERP.Models.Comment;
using BWERP.Models.SeedWork;

namespace BWERP.Repositories.Interfaces
{
	public interface IAssetApiClient
	{
		Task<List<AssetCategoryView>> GetCategory();
		Task<List<AssetStatus>> GetAssetStatus();
		Task<AssetView> GetLatestId();
		Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch);
		Task<AssetView> GetAssetById(string assetid);
		Task<bool> CreateAsset(AssetCreateDto request);
		Task<bool> UpdateAsset(string id, AssetUpdateDto request);
		Task<bool> DeleteAsset(int id);

		//ASSET HISTORY
		Task<PagedList<AssetHistoryView>> GetAssetHistory(AssetHistorySearch historySearch);
		Task<bool> CreateAssetHistory(AssetHistoryCreateDto request);
	}
}
