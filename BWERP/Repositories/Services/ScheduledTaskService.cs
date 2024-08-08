using Blazored.Toast.Services;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;
using BWERP.Repositories.Interfaces;

namespace BWERP.Repositories.Services
{
	public class ScheduledTaskService : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly IServiceProvider _serviceProvider;
		private readonly ISendMailService _sendMailService;
		private IToastService toastService { set; get; }
		private EmailDto emailDto = new EmailDto();
		private List<DailyReportView> dailyReportViews;

		public ScheduledTaskService(IServiceProvider serviceProvider,
			ISendMailService sendMailService)
		{
			_serviceProvider = serviceProvider;
			_sendMailService = sendMailService;
		}
		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
			return Task.CompletedTask;
		}
		private async void DoWork(object state)
		{
			var currentTime = DateTime.Now;
			var sendTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 9, 49, 0); // 4:00 PM
			if (currentTime >= sendTime && currentTime < sendTime.AddMinutes(1))
			{
				using (var scope = _serviceProvider.CreateScope())
				{
					var rptdate = DateTime.Now.Date;

					// Check if dailyReportApiClient is null
					if (_sendMailService == null)
						return;

					dailyReportViews = await _sendMailService.GetListDailyReport(rptdate);

					if (dailyReportViews.Count == 0)
					{
						return;
					}

					List<string> stringList = dailyReportViews.Select(s => s.HtmlBody).ToList();

					emailDto.ToAdress = string.Join(";", emailAddresses.ToAddresses);
					emailDto.CcAddress = string.Join(";", emailAddresses.CcAddresses);
					emailDto.Subject = $"{rptdate:yyyy-MM-dd} - 업무 공유";
					emailDto.Body = $"Dear all,<br>" +
									$"Please take a look at this below." +
									$"{string.Join("<br>", stringList)}";
									
					try
					{
						var result = await _sendMailService.SendEmailAsync(emailDto);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"An error occurred: {ex.Message}");
					}
				}
			}
		}
		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
		public class EmailAddresses
		{
			public List<string> ToAddresses { get; set; } = new List<string>();
			public List<string> CcAddresses { get; set; } = new List<string>();
		}

		private EmailAddresses emailAddresses = new EmailAddresses
		{
			ToAddresses = new List<string>
			{
				"jschoi@buwon.com;chha@buwon.com;hskim@buwon.com;jkkim@buwon.com;cbkim@buwon.com;cblee@buwon.com;skkyong@buwon.com;msyu@buwon.com;ihkim@buwon.com;sjkang@buwon.com;sjlee@buwon.com;mjrbergin@buwon.com;bthien@buwon.com;isyun@buwon.com;jhlim@buwon.com;hdkim@buwon.com"
				//"bthien@buwon.com"
            },
			CcAddresses = new List<string>
			{
				"wsjoo@buwon.com;wkjeon@buwon.com;kskim@buwon.com;sbhong@buwon.com;dschoi@buwon.com;secretary@buwon.com;nltmai@buwon.com"
			}
		};
	}
}
