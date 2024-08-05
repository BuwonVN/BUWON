using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;
using BWERP.Repositories.Interfaces;

namespace BWERP.Repositories.Services
{
	public class EmailApiClient : IEmailApiClient
	{
		public HttpClient _httpClient;
		public EmailApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<bool> SendEmailAsync(EmailDto emailDto)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/email", emailDto);
			return result.IsSuccessStatusCode;
		}
	}
}
