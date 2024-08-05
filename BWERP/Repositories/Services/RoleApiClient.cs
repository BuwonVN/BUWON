using BWERP.Models.Role;
using BWERP.Models.Task;
using BWERP.Repositories.Interfaces;

namespace BWERP.Repositories.Services
{
    public class RoleApiClient : IRoleApiClient
    {
        public HttpClient _httpClient;
        public RoleApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AssignRole(Guid id, RoleAssignRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/roles/{id}/assign", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<List<RoleViewDto>> GetListRole()
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoleViewDto>>($"/api/roles");
            return result;
        }
    }
}
