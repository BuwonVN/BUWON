using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.AssetHistory;
using BWERP.Models.Comment;
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

		public async Task<AssetView> GetAssetById(string assetid)
		{
			var result = await _httpClient.GetFromJsonAsync<AssetView>($"/api/assets/{assetid}");
			return result;
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

		public async Task<bool> UpdateAsset(string id, AssetUpdateDto request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/assets/{id}", request);
			return result.IsSuccessStatusCode;
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

		public async Task<PagedList<AssetHistoryView>> GetAssetHistory(AssetHistorySearch historySearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = historySearch.PageNumber.ToString()
			};

			string url = QueryHelpers.AddQueryString($"/api/assets/history", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<AssetHistoryView>>(url);
			return result;
		}

		public async Task<bool> CreateAssetHistory(AssetHistoryCreateDto request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/assets/history", request);
			return result.IsSuccessStatusCode;
		}
	}
}
