using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Models.User;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;

namespace BWERP.Repositories.Services
{
	public class DailyReportApiClient : IDailyReportApiClient
	{
		public HttpClient _httpClient;
		public DailyReportApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<bool> CreateDailyReport(DailyReportCreateRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/DailyReports", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteDailyReport(int id)
		{
			var result = await _httpClient.DeleteAsync($"/api/DailyReports/{id}");
			return result.IsSuccessStatusCode;
		}

		public async Task<PagedList<DailyReportView>> GetListDailyReport(DailyReportListSearch dailyreportSearch)
		{
			//var result = await _httpClient.GetFromJsonAsync<List<DailyReportView>>($"/api/DailyReports?userid={userid}");
			//return result;
			//
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = dailyreportSearch.PageNumber.ToString()
			};

			queryStringParam.Add("userid", dailyreportSearch.UserId);
			string url = QueryHelpers.AddQueryString($"/api/DailyReports", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<DailyReportView>>(url);
			return result;
		}

		public async Task<List<DailyReportView>> GetListDailyRptSearch(DailyReportListSearch dailyrptsearch)
		{
			string url = $"/api/DailyReports/search?CreatedDate={dailyrptsearch.CreatedDate.Value.ToString("yyyy-MM-dd")}";
			
			var result = await _httpClient.GetFromJsonAsync<List<DailyReportView>>(url);
			return result;
		}

		public async Task<DailyReportView> GetReportById(int id)
		{
			var result = await _httpClient.GetFromJsonAsync<DailyReportView>($"/api/DailyReports/{id}");
			return result;
		}

		public async Task<bool> UpdateDailyReport(int id, DailyReportUpdateRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/DailyReports/{id}", request);
			return result.IsSuccessStatusCode;
		}
	}
}
