using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentRepository _comment;
		public CommentsController(ICommentRepository comment)
		{
			_comment = comment;
		}
		//Get list comment
		[HttpGet]
		public async Task<IActionResult> GetAllComment(int pageNumber = 1, int pageSize = 10)
		{
			var commentQuery = await _comment.GetListComment(pageNumber, pageSize);
			return Ok(commentQuery);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _comment.GetCommentById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new CommentViewRequest()
			{
				Content = result.Content,
				DepartmentId = result.DepartmentId,
				FuncName = result.Department.Name,
				CreatedBy = result.CreatedBy,
				Id = result.Id,
				CreatedDate = result.CreatedDate,
				UpdatedDate = result.UpdatedDate, 
				UpdatedBy = result.UpdatedBy
			});
		}

		[HttpGet]
		[Route("department/{id}")]		
		public async Task<IActionResult> GetByDepartmentId(int id)
		{
			var result = await _comment.GetCommentByDeptId(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new CommentViewRequest()
			{
				Content = result.Content,
				//Function = result.Function,
				//FuncId= result.FuncId,
				CreatedBy = result.CreatedBy,
				Id = result.Id,
				CreatedDate = result.CreatedDate,
				UpdatedDate = result.UpdatedDate,
				UpdatedBy = result.UpdatedBy
			});
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _comment.Create(new Comment()
			{
				Content = request.Content.Replace("<table>", @"<table class=""table table-bordered"">"),
				DepartmentId = request.DepartmentId,
				CreatedDate = DateTime.Now,
				CreatedBy = request.CreatedBy,
				UpdatedDate = DateTime.Now,
				UpdatedBy= request.UpdatedBy
			});
			return Ok();
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] CommentEditRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var taskFromDb = await _comment.GetCommentById(id);

			if (taskFromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			taskFromDb.Content = request.Content.Replace("<table>", @"<table class=""table table-bordered"">");
			taskFromDb.UpdatedBy = request.UpdatedBy;
			taskFromDb.UpdatedDate = DateTime.Now;
			
			var taskResult = await _comment.Update(taskFromDb);
			return Ok();
		}
	}
}
