using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.SeedWork;

namespace BWERP.Api.Repositories.Interfaces
{
    public interface IAssetRepository
    {
		Task<List<AssetCategoryView>> GetAssetCategory();
		Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch);
		Task<Asset> GetAssetById(int id);
		Task<Asset> Create(Asset asset);
		Task<Asset> Update(Asset asset);
	}
}
