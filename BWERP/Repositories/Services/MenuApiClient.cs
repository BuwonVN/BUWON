using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class MenuApiClient : IMenuApiClient
	{
		public HttpClient _httpClient;
		public MenuApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<PagedList<MenuViewRequest>> GetListMenu(MenuListSearch menuListSearch)
		{
			//string url = $"/api/tasks?name={taskListSearch.Name}&assignee={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}";
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = menuListSearch.PageNumber.ToString()
			};

			if (!string.IsNullOrEmpty(menuListSearch.Name))
				queryStringParam.Add("name", menuListSearch.Name);
			

			string url = QueryHelpers.AddQueryString("/api/menus", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<MenuViewRequest>>(url);
			return result;
		}

		public async Task<MenuViewRequest> GetMenuById(int id)
		{
			var result = await _httpClient.GetFromJsonAsync<MenuViewRequest>($"/api/menus/{id}");
			return result;
		}
		public async Task<bool> CreateMenu(MenuCreateRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/menus", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteMenu(int id)
		{
			var result = await _httpClient.DeleteAsync($"/api/menus/{id}");
			return result.IsSuccessStatusCode;
		}		

		public async Task<bool> UpdateMenu(int id, MenuUpdateRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/menus/{id}", request);
			return result.IsSuccessStatusCode;
		}
		                                                                                                                                                                                                                           
		public async Task<List<ParentMenuDto>> GetParentMenu()
		{
			var result = await _httpClient.GetFromJsonAsync<List<ParentMenuDto>>($"/api/menus/parent");
			return result;
		}

		public async Task<List<MenuViewRequest>> GetMenuByUser(string username)
		{
			var result = await _httpClient.GetFromJsonAsync<List<MenuViewRequest>>($"/api/menus/getbyuser/{username}");
			return result;
		}
	}
}
