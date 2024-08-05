using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace BWERP.Repositories.Services
{
	public class CommentApiClient : ICommentApiClient
	{
		public HttpClient _httpClient;
		public CommentApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<bool> CreateComment(CommentCreateRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/comments", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteComment(int id)
		{
			var result = await _httpClient.DeleteAsync($"/api/comments/{id}");
			return result.IsSuccessStatusCode;
		}

		public async Task<CommentViewRequest> GetCommentByDeptId(int id)
		{
			var result = await _httpClient.GetFromJsonAsync<CommentViewRequest>($"/api/comments/department/{id}");
			return result;
		}

		public async Task<CommentViewRequest> GetCommentById(int id)
		{
			var result = await _httpClient.GetFromJsonAsync<CommentViewRequest>($"/api/comments/{id}");
			return result;
		}

		public async Task<PagedList<CommentViewRequest>> GetListComment(CommentSearch commentSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = commentSearch.PageNumber.ToString()
			};

			string url = QueryHelpers.AddQueryString($"/api/comments", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<CommentViewRequest>>(url);
			return result;
		}
 
		public async Task<bool> UpdateComment(int id, CommentEditRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/comments/{id}", request);
			return result.IsSuccessStatusCode;
		}
	}
}
