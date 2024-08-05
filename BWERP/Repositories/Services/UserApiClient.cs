using BWERP.Models.Comment;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Models.User;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class UserApiClient : IUserApiClient
	{
		public HttpClient _httpClient;
		public UserApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<AssigneeDto>> GetAssignees()
		{
			var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>($"/api/users");
			return result;
		}
		public async Task<PagedList<UserViewRequest>> GetListUser(UserListSearch userListSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = userListSearch.PageNumber.ToString()
			};

			string url = QueryHelpers.AddQueryString($"/api/users/all", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<UserViewRequest>>(url);
			return result;
		}

		public async Task<UserViewRequest> GetUserById(string id)
		{
			var result = await _httpClient.GetFromJsonAsync<UserViewRequest>($"/api/users/{id}");
			return result;
		}
		public async Task<bool> CreateUser(UserCreateRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/users", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteUser(Guid id)
		{
			var result = await _httpClient.DeleteAsync($"/api/users/{id}");
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateUser(Guid id, UserUpdateRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/users/{id}", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> ChangePassword(Guid id, UserChangePassword request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/users/changepassword/{id}", request);
			return result.IsSuccessStatusCode;
		}
	}
}
