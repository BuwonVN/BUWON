using BWERP.Models.Email;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IMailService
	{
		void SendEmailAsync(EmailDto emailDto);
	}
}
