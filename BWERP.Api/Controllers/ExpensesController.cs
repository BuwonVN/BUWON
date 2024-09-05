using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Exppense;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpensesController : ControllerBase
	{
		private readonly IExpenseRepository _expenseRepository;
		public ExpensesController(IExpenseRepository expenseRepository)
		{
			_expenseRepository = expenseRepository;
		}

		[HttpGet("category")]
		public async Task<IActionResult> GetExpenseCategory()
		{
			var result = await _expenseRepository.GetExpenseCategory();
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetListExpense([FromQuery] ExpenseSearch expenseSearch)
		{
			var pagedlist = await _expenseRepository.GetListExpense(expenseSearch);
			var expenseDto = pagedlist.Items.ToList();

			return Ok(new PagedList<ExpenseView>(expenseDto,
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
		}
		[HttpGet("nopaging")]
		public async Task<IActionResult> GetListExpenseNoPaging([FromQuery] ExpenseSearch expenseSearch)
		{
			var data = await _expenseRepository.GetListExpenseNoPaging(expenseSearch);
			return Ok(data);
		}
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _expenseRepository.GetExpenseById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new ExpenseView()
			{
				Id = result.Id,
				DepartmentId = result.DepartmentId,
				CategoryId= result.CategoryId,
				Description = result.Description,
				CreatedDate = result.CreatedDate,
				CreatedUser = result.CreatedUser,
				Amount = result.Amount,
				Note = result.Note
			});
		}

		//CREATE DATA
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ExpenseCreateDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _expenseRepository.Create(new Expense()
			{
				DepartmentId = request.DepartmentId,
				//Description = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
				CategoryId = request.CategoryId,
				Description = request.Description,
				CreatedDate = request.CreatedDate,
				CreatedUser = request.CreatedUser,
				Amount = request.Amount,
				Note = request.Note
			});
			return Ok();
		}
		//UPDATE DATA
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ExpenseUpdateDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var fromDb = await _expenseRepository.GetExpenseById(id);

			if (fromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			fromDb.CategoryId = request.CategoryId;
			fromDb.Description = request.Description;
			fromDb.CreatedDate = request.CreatedDate;
			fromDb.Amount = request.Amount;
			fromDb.Note = request.Note;

			var result = await _expenseRepository.Update(fromDb);

			return Ok();
		}
	}
}
