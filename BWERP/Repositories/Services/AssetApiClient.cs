using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class AssetApiClient : IAssetApiClient
	{
		public HttpClient _httpClient;
		public AssetApiClient(HttpClient httpClient)
        {
			_httpClient = httpClient;

		}
        public async Task<bool> CreateAsset(AssetCreateDto request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/assets", request);
			return result.IsSuccessStatusCode;
		}

		public Task<bool> DeleteAsset(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<AssetCategoryView>> GetCategory()
		{
			var result = await _httpClient.GetFromJsonAsync<List<AssetCategoryView>>($"/api/assets/category");
			return result;
		}

		public Task<AssetView> GetAssetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<PagedList<AssetView>> GetListAsset(AssetSearch assetSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = assetSearch.PageNumber.ToString()
			};
			
			if (!string.IsNullOrEmpty(assetSearch.Name))
				queryStringParam.Add("name", assetSearch.Name);

			if (!string.IsNullOrEmpty(assetSearch.Location))
				queryStringParam.Add("location", assetSearch.Location);

			if (assetSearch.CategoryId.HasValue)
				queryStringParam.Add("categoryid", assetSearch.CategoryId.ToString());

			if (assetSearch.StatusId.HasValue)
				queryStringParam.Add("statusid", assetSearch.StatusId.ToString());

			string url = QueryHelpers.AddQueryString($"/api/assets", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<AssetView>>(url);
			return result;
		}

		public Task<bool> UpdateAsset(int id, AssetUpdateDto request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<AssetStatus>> GetAssetStatus()
		{
			var result = await _httpClient.GetFromJsonAsync<List<AssetStatus>>($"/api/assets/status");
			return result;
		}

		public async Task<List<AssetView>> GetAssetAll()
		{
			var result = await _httpClient.GetFromJsonAsync<List<AssetView>>($"/api/assets/all");
			return result;
		}
	}
}
