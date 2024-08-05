using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Role;
using BWERP.Models.Task;
using BWERP.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IRoleRepository _roleRepository;
		public RolesController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		//Get Permission list by Role Id
		[HttpGet]
		[Route("permission")]
		public async Task<IActionResult> GetRolePermission(Guid roleid)
		{
			var result = await _roleRepository.GetRolePermission(roleid);
			if (result == null)
			{
				return NotFound($"{roleid} is not found");
			}
			return Ok(result);
		}
        //Get List Role
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleRepository.GetListRole();
            return Ok(result);
        }
    }
}
