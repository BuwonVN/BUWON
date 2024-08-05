using BWERP.Models.Department;
using BWERP.Models.Menu;
using BWERP.Repositories.Interfaces;
using System.Net.Http;

namespace BWERP.Repositories.Services
{
	public class DepartmentApiClient : IDepartmentApiClient
	{
		public HttpClient _httpClient;
		public DepartmentApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<DepartmentViewDto>> GetDepartmentByUser(string id)
		{
			var result = await _httpClient.GetFromJsonAsync<List<DepartmentViewDto>>($"/api/Departments/user/{id}");
			return result;
		}

		public async Task<List<DepartmentViewDto>> GetDepartmentList()
		{
			var result = await _httpClient.GetFromJsonAsync<List<DepartmentViewDto>>($"api/departments");
			return result;
		}
	}
}
