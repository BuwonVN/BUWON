using BWERP.Models.User;

namespace BWERP.Repositories.Interfaces
{
	public interface IAuthenService
	{
		Task<LoginResponse> Login(LoginRequest loginRequest);
		Task Logout(); 
	}
}
