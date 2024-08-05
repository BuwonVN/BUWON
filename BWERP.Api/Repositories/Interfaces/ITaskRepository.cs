using BWERP.Models.Task;
using BWERP.Models.SeedWork;
using Microsoft.AspNetCore.Components.Web;
using Task = BWERP.Api.Entities.Task;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface ITaskRepository
	{
		Task<PagedList<Task>> GetAllTask(TaskListSearch taskListSearch);
		Task<PagedList<Task>> GetByUserId(Guid userid, TaskListSearch taskListSearch);
		Task<Task> GetTaskById(Guid id);
		Task<Task> Create(Task task);		
		Task<Task> Update(Task task);
		Task<Task> Delete(Task task);
	}
}
