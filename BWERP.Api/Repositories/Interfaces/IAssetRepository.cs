using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.AssetHistory;
using BWERP.Models.Comment;
using BWERP.Models.SeedWork;

namespace BWERP.Api.Repositories.Interfaces
{
    public interface IAssetRepository
    {
		Task<List<AssetCategoryView>> GetAssetCategory();
		Task<List<AssetStatus>> GetAssetStatus();
		Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch);
		Task<List<AssetView>> GetAssetAll();
		Task<Asset> GetAssetById(string id);
		Task<Asset> Create(Asset asset);
		Task<Asset> Update(Asset asset);
		//ASSET HISTORY
		Task<PagedList<AssetHistoryView>> GetAssetHistory(int pageNumber, int pageSize);
		Task<AssetHistory> CreateHistory(AssetHistory asset);
	}
}
