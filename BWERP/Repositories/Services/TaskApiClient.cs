using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class TaskApiClient : ITaskApiClient
	{
		public HttpClient _httpClient;
		public TaskApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        public async Task<PagedList<TaskViewRequest>> GetListTask(TaskListSearch taskListSearch)
		{
			//string url = $"/api/tasks?name={taskListSearch.Name}&assignee={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}";
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = taskListSearch.PageNumber.ToString()
			};

			if (!string.IsNullOrEmpty(taskListSearch.Name))
				queryStringParam.Add("name", taskListSearch.Name);
			if (taskListSearch.AssigneeId.HasValue)
				queryStringParam.Add("assigneeId", taskListSearch.AssigneeId.ToString());
			if (taskListSearch.Priority.HasValue)
				queryStringParam.Add("priority", taskListSearch.Priority.ToString());

			string url = QueryHelpers.AddQueryString("/api/tasks", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<TaskViewRequest>>(url);
			return result;
		}

        public async Task<TaskViewRequest> GetTaskById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskViewRequest>($"/api/tasks/{id}");
            return result;
        }
        public async Task<bool> CreateTask(TaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/tasks", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/tasks/{id}");
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AssignTask(Guid id, TaskAssignRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}/assign", request);
            return result.IsSuccessStatusCode;
        }

		public async Task<PagedList<TaskViewRequest>> GetMyListTask(TaskListSearch taskListSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = taskListSearch.PageNumber.ToString()
			};

			if (!string.IsNullOrEmpty(taskListSearch.Name))
				queryStringParam.Add("name", taskListSearch.Name);
			if (taskListSearch.AssigneeId.HasValue)
				queryStringParam.Add("assigneeId", taskListSearch.AssigneeId.ToString());
			if (taskListSearch.Priority.HasValue)
				queryStringParam.Add("priority", taskListSearch.Priority.ToString());

			string url = QueryHelpers.AddQueryString("/api/tasks/me ", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<TaskViewRequest>>(url);
			return result;
		}
	}
}
