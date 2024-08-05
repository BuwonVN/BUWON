using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;

namespace BWERP.Repositories.Interfaces
{
	public interface IEmailApiClient
	{
		Task<bool> SendEmailAsync(EmailDto emailDto);
	}
}
