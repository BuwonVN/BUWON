using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.SeedWork;

namespace BWERP.Repositories.Interfaces
{
	public interface IAssetApiClient
	{
		Task<List<AssetCategoryView>> GetCategory();
		Task<List<AssetStatus>> GetAssetStatus();
		Task<List<AssetView>> GetAssetAll();
		Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch);
		Task<AssetView> GetAssetById(int id);
		Task<bool> CreateAsset(AssetCreateDto request);
		Task<bool> UpdateAsset(int id, AssetUpdateDto request);
		Task<bool> DeleteAsset(int id);
	}
}
