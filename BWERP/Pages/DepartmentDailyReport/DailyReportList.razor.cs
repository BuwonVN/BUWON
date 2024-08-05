using Blazored.Toast.Services;
using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace BWERP.Pages.DepartmentDailyReport
{
	public partial class DailyReportList
	{
		private string userid { get; set; }	
		[Inject] private IDailyReportApiClient dailyReportApiClient { get; set; }
		[Inject] IToastService toastService { set; get; }
		[Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		private List<DailyReportView> dailyReportViews;
		private DailyReportListSearch dailyreportSearch = new DailyReportListSearch();
		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			//AUTHORIZE
			var authState = await AuthenticationStateProvider
				.GetAuthenticationStateAsync();
			//GET USERID
			userid = authState.User.FindFirst(c => c.Type.Contains("UserId"))?.Value;

			await GetListDailyReport();
		}
		private async Task GetListDailyReport()
		{
			try
			{
				dailyreportSearch.UserId = userid;

				var pagingResponse = await dailyReportApiClient.GetListDailyReport(dailyreportSearch);
				dailyReportViews = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		//PAGING
		private async Task SelectedPage(int page)
		{
			dailyreportSearch.PageNumber = page;
			await GetListDailyReport();
		}
	}
}
