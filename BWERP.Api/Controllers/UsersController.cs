using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Role;
using BWERP.Models.Task;
using BWERP.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly SignInManager<AppUser> _signInManager;
		public UsersController(IUserRepository userRepository,
			SignInManager<AppUser> signInManager) 
		{ 
			_userRepository = userRepository;
			_signInManager = signInManager;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userRepository.GetListAssignee();
			var assignees = users.Select(x => new AssigneeDto()
			{
				Id = x.Id,
				Fullname = x.FirstName + " " + x.LastName
			});

			return Ok(assignees);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllUser(int pageNumber = 1, int pageSize = 10)
		{
			var users = await _userRepository.GetListUser(pageNumber, pageSize);
			return Ok(users);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var hasher = new PasswordHasher<AppUser>();
			var result = await _userRepository.Create(new AppUser()
			{
				UserName = request.UserName,
				NormalizedUserName = request.UserName.ToUpper(),
				Email = request.Email,
				NormalizedEmail = request.Email.ToUpper(),
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "123456"),
				SecurityStamp = Guid.NewGuid().ToString(),
				FirstName = request.FirstName,
				LastName = request.LastName,
				isActive = true,
				CreatedDate = DateTime.Now,
				LastLogin = DateTime.Now
			});
			return Ok();
		}
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _userRepository.GetUserById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new UserViewRequest()
			{
				FirstName = result.FirstName,
				LastName = result.LastName,
				Username = result.UserName,
				Email = result.Email,
				Password = result.PasswordHash,
				isActive = result.isActive,
				CreatedDate = result.CreatedDate
			});
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var taskFromDb = await _userRepository.GetUserById(id);

			if (taskFromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			taskFromDb.FirstName = request.FirstName;
			taskFromDb.LastName = request.LastName;
			taskFromDb.isActive = request.isActive;
			taskFromDb.LastLogin = request.LastLogin;
			var taskResult = await _userRepository.Update(taskFromDb);

			return Ok();
		}

		[HttpPut("changepassword/{id}")]
		//[Route("{id}")]
		public async Task<IActionResult> ChangePassword(Guid id, [FromBody] UserChangePassword request)
		{
			var hasher = new PasswordHasher<AppUser>();
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			// GET USER INFO
			var userFromDb = await _userRepository.GetUserById(id);
			if (userFromDb == null)
				return NotFound($"{id} is not found");

			// VALIDATE INPUT DATA
			var verificationResult = hasher.VerifyHashedPassword(userFromDb, userFromDb.PasswordHash, request.OldPassword);
			if (verificationResult == PasswordVerificationResult.Failed)
				return BadRequest("Current password is incorrect.");

			if (request.NewPassword != request.ConfirmPassword)
				return BadRequest("Password does not match.");

			// UPDATE PASSWORD
			userFromDb.PasswordHash = hasher.HashPassword(userFromDb, request.NewPassword);
			userFromDb.SecurityStamp = Guid.NewGuid().ToString();
			var result = await _userRepository.ChangePassword(userFromDb);

			return Ok();
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var task = await _userRepository.GetUserById(id);
			if (task == null) return NotFound($"{id} is not found");

			await _userRepository.Delete(task);
			return Ok();
		}
        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignRole(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			//Check request and roleId are not null
            if (request == null || request.RoleId == null)
            {
                return BadRequest("Invalid request");
            }

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound($"{id} is not found");
            }

            user.RoleId = request.RoleId.Value == Guid.Empty ? null : request.RoleId.Value;

            var result = await _userRepository.Update(user);

            return Ok(new UserViewRequest()
            {
               Username = result.UserName,
                FirstName = result.FirstName,
                Id = result.Id,
				Email= result.Email,
				isActive = result.isActive,
                lastLogin = result.LastLogin,
                LastName = result.LastName,
                CreatedDate = result.CreatedDate
            });
        }
    }
}
