using Blazored.Toast.Services;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Email;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BWERP.Pages.DepartmentDailyReport
{
	public partial class DailyReportDetail
	{
		//PARAM
		public string _username { get; set; }
		[Inject] private IDailyReportApiClient dailyReportApiClient { get; set; }
		[Inject] private IEmailApiClient emailApiClient { get; set; }
		[Inject] IToastService toastService { set; get; }
		[Inject] AuthenticationStateProvider AuthenticationStateProvide { get; set; }

		private List<DailyReportView> dailyReportViews;
		private DailyReportListSearch rptsearch = new DailyReportListSearch();
		private EmailDto emailDto = new EmailDto();

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			//AUTHORIZE
			var authState = await AuthenticationStateProvide
				.GetAuthenticationStateAsync();
			var user = authState.User;
			_username = user.Identity.Name;

			rptsearch.CreatedDate = DateTime.Now.Date;
			await GetListDailyReport();
		}
		private async Task GetListDailyReport()
		{
			try
			{
				dailyReportViews = await dailyReportApiClient.GetListDailyRptSearch(rptsearch);
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		//SEARCH REPORT
		private async Task SearchForm(EditContext editContext)
		{
			dailyReportViews.Clear();
			await GetListDailyReport();
			if (dailyReportViews.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}

		//SEND AN EMAIL
		private async Task SendEmail()
		{
			if (dailyReportViews.Count > 0)
			{
				DateTime? dateRpt = rptsearch.CreatedDate;

				List<string> stringList = dailyReportViews.Select(s => s.HtmlBody).ToList();

				if (dateRpt.HasValue)
				{
					emailDto.ToAdress = string.Join(";", emailAddresses.ToAddresses);
					emailDto.CcAddress = string.Join(";", emailAddresses.CcAddresses);
					emailDto.Subject = $"{dateRpt:yyyy-MM-dd} - 업무 공유";
					emailDto.Body = $"Dear all,<br>" +
									$"We would like to share with you the report on {dateRpt:yyyy-MM-dd}.<br>" +
									$"Please take a look at this below." +
									$"{string.Join("<br>", stringList)}";
				}
				else
				{
					toastService.ShowInfo("Invalid date. Please check Report date!");
					return;
				}

				try
				{
					var result = await emailApiClient.SendEmailAsync(emailDto);
					if (result)
					{
						toastService.ShowSuccess("Email has been sent successfully.");
					}
					else
					{
						toastService.ShowError("An error occurred in progress. Please contact the administrator.");
					}
				}
				catch (Exception ex)
				{
					toastService.ShowError($"An unexpected error occurred: {ex.Message}");
				}
			}
			else
			{
				toastService.ShowInfo("No data found.");
			}
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
			},
			CcAddresses = new List<string>
			{
				"wsjoo@buwon.com;wkjeon@buwon.com;kskim@buwon.com;sbhong@buwon.com;dschoi@buwon.com;secretary@buwon.com;nltmai@buwon.com"				
			}
		};
	}
}
