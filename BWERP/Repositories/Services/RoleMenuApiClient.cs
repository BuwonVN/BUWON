using BWERP.Models.Role;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class RoleMenuApiClient : IRoleMenuApiClient
	{
		public HttpClient _httpClient;
		public RoleMenuApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<PagedList<RoleMenusDto>> GetListMenu(Guid roleid,RoleMenuListSearch listSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = listSearch.PageNumber.ToString()
			};

			//queryStringParam.Add("roleid", listSearch.RoleId.ToString());


			string url = QueryHelpers.AddQueryString($"/api/RoleMenus/getmenu/{roleid}", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<RoleMenusDto>>(url);
			return result;
		}

		public async Task<List<RoleViewDto>> GetListRole()
		{
			var result = await _httpClient.GetFromJsonAsync<List<RoleViewDto>>($"/api/RoleMenus/rolelist");
			return result;
		}
	}
}
