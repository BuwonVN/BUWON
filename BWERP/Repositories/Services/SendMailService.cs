using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;
using BWERP.Repositories.Interfaces;
using System.Net.Http;

namespace BWERP.Repositories.Services
{
	public class SendMailService : ISendMailService
	{
		private readonly HttpClient _httpClient;
		public SendMailService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://10.11.10.42:8080/");
			//_httpClient.BaseAddress = new Uri("https://localhost:7036");
		}
		public async Task<bool> SendEmailAsync(EmailDto emailDto)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/email", emailDto);
			return result.IsSuccessStatusCode;
		}
		public async Task<List<DailyReportView>> GetListDailyReport(DateTime reportdate)
		{
			string url = $"/api/DailyReports/search?CreatedDate={reportdate.ToString("yyyy-MM-dd")}";
			var result = await _httpClient.GetFromJsonAsync<List<DailyReportView>>(url);
			return result;
		}
	}
}
