using BWERP.Api.Entities;
using BWERP.Api.Extensions;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Enums;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        public MenusController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenu([FromQuery] MenuListSearch menuListSearch)
        {
            var pagedlist = await _menuRepository.GetAllMenu(menuListSearch);

            var menuDtos = pagedlist.Items.Select(x => new MenuViewRequest()
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                Icon = x.Icon,
                //Assignee = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A",
                Url = x.Url,
                SortOrder = x.SortOrder,
                isEnable = x.isEnable
            }).OrderBy(x => x.SortOrder);
            return Ok(new PagedList<MenuViewRequest>(menuDtos.ToList(),
                pagedlist.MetaData.TotalCount,
                pagedlist.MetaData.CurrentPage,
                pagedlist.MetaData.PageSize));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _menuRepository.GetMenuById(id);
            if (result == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(new MenuViewRequest()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                ParentId = result.ParentId,
                SortOrder = result.SortOrder,
                Url = result.Url,
                Icon = result.Icon
        });
        }
		[HttpGet("parent")]
		public async Task<IActionResult> GetParentMenu()
		{
			var result = await _menuRepository.GetParentMenu();

            var menuDtos = result.Select(x => new ParentMenuDto()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Ok(menuDtos);
		}
        [HttpGet]
        [Route("getbyuser/{username}")]
        public async Task<IActionResult> GetMenuByUser(string username)
        {
            var result = await _menuRepository.GetMenuByUser(username);            
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _menuRepository.Create(new AppMenu()
            {
                Name = request.Name,
                //Description = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Description = request.Description,
                ParentId = request.ParentId == null ? 0 : request.ParentId,
                Url = request.Url,
                SortOrder = request.SortOrder,
                Icon= request.Icon,
                IconPath = request.IconPath,
                isEnable = 1
            });
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _menuRepository.GetMenuById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Description = request.Description;
            taskFromDb.ParentId = request.ParentId == null ? 0 : request.ParentId;
            taskFromDb.Url = request.Url;
            taskFromDb.SortOrder = request.SortOrder;
            taskFromDb.Icon = request.Icon;

            var taskResult = await _menuRepository.Update(taskFromDb);

            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var task = await _menuRepository.GetMenuById(id);
            if (task == null) return NotFound($"{id} is not found");

            await _menuRepository.Delete(task);
            return Ok();
        }
    }
}
