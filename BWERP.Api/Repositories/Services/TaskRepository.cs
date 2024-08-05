using BWERP.Api.EF;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Enums;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.EntityFrameworkCore;
using Task = BWERP.Api.Entities.Task;

namespace BWERP.Api.Repositories.Services
{
	public class TaskRepository : ITaskRepository
	{
		private readonly MainContext _mainContext;
		public TaskRepository(MainContext mainContext)
		{
			_mainContext = mainContext;
		}

		public async Task<PagedList<Task>> GetAllTask(TaskListSearch taskListSearch)
		{
			var query = _mainContext.Tasks
				.Include(x => x.Assignee).AsQueryable();

			if (!string.IsNullOrEmpty(taskListSearch.Name))
				query = query.Where(t => t.Name.Contains(taskListSearch.Name));

			if (taskListSearch.AssigneeId.HasValue)
				query = query.Where(t => t.AssigneeId == taskListSearch.AssigneeId.Value);

			if (taskListSearch.Priority.HasValue)
				query = query.Where(t => t.Priority == taskListSearch.Priority.Value);

			var count = await query.CountAsync();

			var data = await query.OrderByDescending(x => x.CreatedDate)
				.Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
				.Take(taskListSearch.PageSize)
				.ToListAsync();
			return new PagedList<Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
		}

		public async Task<PagedList<Task>> GetByUserId(Guid userid, TaskListSearch taskListSearch)
		{
			var query = _mainContext.Tasks
				.Where(x=> x.AssigneeId == userid)
				.Include(x => x.Assignee).AsQueryable();

			if (!string.IsNullOrEmpty(taskListSearch.Name))
				query = query.Where(t => t.Name.Contains(taskListSearch.Name));

			if (taskListSearch.AssigneeId.HasValue)
				query = query.Where(t => t.AssigneeId == taskListSearch.AssigneeId.Value);

			if (taskListSearch.Priority.HasValue)
				query = query.Where(t => t.Priority == taskListSearch.Priority.Value);

			var count = await query.CountAsync();

			var data = await query.OrderByDescending(x => x.CreatedDate)
				.Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
				.Take(taskListSearch.PageSize)
				.ToListAsync();
			return new PagedList<Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
		}

		public async Task<Task> GetTaskById(Guid id)
		{
			return await _mainContext.Tasks.FindAsync(id);
		}
        public async Task<Task> Create(Task task)
        {
            await _mainContext.Tasks.AddAsync(task);
            await _mainContext.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Delete(Task task)
        {
            _mainContext.Tasks.Remove(task);
            await _mainContext.SaveChangesAsync();
            return task;
        }
        public async Task<Task> Update(Task task)
		{
            _mainContext.Tasks.Update(task);
             await _mainContext.SaveChangesAsync();
			return task;
        }
	}
}
