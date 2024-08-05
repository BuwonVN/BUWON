using BWERP.Api.Entities;
using BWERP.Api.Extensions;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Enums;
using BWERP.Models.Menu;
using BWERP.Models.Role;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleMenusController : ControllerBase
	{
		private readonly IRoleMenusRepository _roleMenusRepository;
		public RoleMenusController(IRoleMenusRepository roleMenusRepository)
		{
			_roleMenusRepository = roleMenusRepository;
		}
		[HttpGet]
		[Route("getmenu/{roleid}")]
		public async Task<IActionResult> GetMenuByRoleId(Guid roleid, [FromQuery] RoleMenuListSearch rolemenuSearch)
		{
			var pagedlist = await _roleMenusRepository.GetMenuByRoleId(roleid, rolemenuSearch);

			var taskDtos = pagedlist.Items.Select(x => new RoleMenusDto()
			{
				Id = x.Id,
				RoleId = x.RoleId,
				RoleName = x.AppRole.Name,
				MenuId = x.MenuId,
				MenuName = x.AppMenu.Name,
			});
			return Ok(new PagedList<RoleMenusDto>(taskDtos.ToList(),
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
		}
		[HttpGet("rolelist")]
		public async Task<IActionResult> GetListRole()
		{
			var result = await _roleMenusRepository.GetListRole();

			var menuDtos = result.Select(x => new RoleViewDto()
			{
				Id = x.Id,
				Name = x.Name
			});
			return Ok(menuDtos);
		}
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _roleMenusRepository.GetById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new RoleMenusDto()
			{
				Id = result.Id,
				RoleId = result.RoleId,
				MenuId = result.MenuId,
			});
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RoleMenuCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _roleMenusRepository.Create(new AppRoleMenu()
			{
				MenuId = request.MenuId,
				RoleId = request.RoleId,
			});
			return Ok();
		}		

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] RoleMenuUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var taskFromDb = await _roleMenusRepository.GetById(id);

			if (taskFromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			taskFromDb.RoleId = request.RoleId;
			taskFromDb.MenuId = request.MenuId;
			var taskResult = await _roleMenusRepository.Update(taskFromDb);
			return Ok();
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var task = await _roleMenusRepository.GetById(id);
			if (task == null) return NotFound($"{id} is not found");

			await _roleMenusRepository.Delete(task);
			return Ok();
		}
	}
}
