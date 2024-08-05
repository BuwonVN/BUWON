using BWERP.Models.SeedWork;
using BWERP.Models.Task;

namespace BWERP.Repositories.Interfaces
{
	public interface ITaskApiClient
	{
		Task<PagedList<TaskViewRequest>> GetListTask(TaskListSearch taskListSearch);
		Task<PagedList<TaskViewRequest>> GetMyListTask(TaskListSearch taskListSearch);
		Task<TaskViewRequest> GetTaskById(string id);
		Task<bool> CreateTask(TaskCreateRequest request);
        Task<bool> UpdateTask(Guid id, TaskUpdateRequest request);

        Task<bool> AssignTask(Guid id, TaskAssignRequest request);

        Task<bool> DeleteTask(Guid id);
    }
}
