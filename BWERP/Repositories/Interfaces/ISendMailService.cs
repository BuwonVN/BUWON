using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;

namespace BWERP.Repositories.Interfaces
{
	public interface ISendMailService
	{
		Task<bool> SendEmailAsync(EmailDto emailDto);
		Task<List<DailyReportView>> GetListDailyReport(DateTime reportdate);
	}
}
