using BWERP.Api.Extensions;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Enums;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

	public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //api/tasks?name=?
        [HttpGet]
        public async Task<IActionResult> GetAllTask([FromQuery] TaskListSearch taskListSearch)
        {
            var pagedlist = await _taskRepository.GetAllTask(taskListSearch);

            var taskDtos = pagedlist.Items.Select(x => new TaskViewRequest()
            {
                Id = x.Id,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                Assignee = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A",
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Status = x.Status
            }).OrderByDescending(x => x.CreatedDate);
            return Ok(new PagedList<TaskViewRequest>(taskDtos.ToList(),
                pagedlist.MetaData.TotalCount,
                pagedlist.MetaData.CurrentPage,
                pagedlist.MetaData.PageSize));
        }

		[HttpGet("me")]
		public async Task<IActionResult> GetTaskByUserId([FromQuery] TaskListSearch taskListSearch)
		{
            var userid = User.GetUserId();
			var pagedlist = await _taskRepository.GetByUserId(Guid.Parse(userid),taskListSearch);

			var taskDtos = pagedlist.Items.Select(x => new TaskViewRequest()
			{
				Id = x.Id,
				Name = x.Name,
				AssigneeId = x.AssigneeId,
				Assignee = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A",
				CreatedDate = x.CreatedDate,
				Priority = x.Priority,
				Status = x.Status
			}).OrderByDescending(x => x.CreatedDate);
			return Ok(new PagedList<TaskViewRequest>(taskDtos.ToList(),
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
		}

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskRepository.Create(new Entities.Task()
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.Now,
            });
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskRepository.GetTaskById(id);
            if (result == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(new TaskViewRequest()
            {
                Name = result.Name,
                Status = result.Status,
                Id = result.Id,
                AssigneeId = result.AssigneeId,
                Priority = result.Priority,
                CreatedDate = result.CreatedDate
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetTaskById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;
            taskFromDb.Status = request.Status;
            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null) return NotFound($"{id} is not found");

            await _taskRepository.Delete(task);
            return Ok();
        }

        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid id, [FromBody] TaskAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetTaskById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.AssigneeId = request.UserId.Value == Guid.Empty ? null : request.UserId.Value;

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskViewRequest()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }
    }
}
