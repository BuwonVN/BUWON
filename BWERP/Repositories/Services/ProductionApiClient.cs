using BWERP.Models.Production;
using BWERP.Models.WeelkyReport;
using BWERP.Repositories.Interfaces;
using System;

namespace BWERP.Repositories.Services
{
	public class ProductionApiClient : IProductionApiClient
	{
		public HttpClient _httpClient;
		public ProductionApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<ProductionViewDto>> GetProductionStatus_Dashboard(string dept, DateTime scandate)
		{
			string url = $"/api/Productions/dashboardprod?dept={dept}&scandate={scandate.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<ProductionViewDto>>(url);
			return result;
		}
	}
}
