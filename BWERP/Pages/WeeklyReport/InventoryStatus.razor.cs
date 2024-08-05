using Blazored.Toast.Services;
using BWERP.Models.Comment;
using BWERP.Models.WeelkyReport;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;

namespace BWERP.Pages.WeeklyReport
{
	public partial class InventoryStatus
	{
		public int commentid { get; set; }
		private double sumBeginQty { get; set; }
		private double sumEndQty { get; set; }
		private double sumInQty { get; set; }
		private double sumOutQty { get; set; }
		//INJECTION
		[Inject] private ICommentApiClient commentApi { get; set; }
		[Inject] private IWeeklyReportApiClient weeklyreportApi { get; set; }
		[Inject] IToastService toastService { set; get; }

		private DateRangeSearch daterangeSearch = new DateRangeSearch();
		private List<InventorySummaryDto> inventorySummaryDtos;
		private CommentViewRequest commentView { set; get; }

		DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

		[CascadingParameter]
		private Error? _error { get; set; }
		protected override async Task OnInitializedAsync()
		{
			daterangeSearch.FromDate = startOfMonth;
			daterangeSearch.ToDate = DateTime.Now;
			try
			{
				await GetInventoryStatus();
				commentView = await commentApi.GetCommentByDeptId(11);
				string pattern = @"<thead(?![^>]*\bclass\b)";
				string replacement = "<thead class=\"table-grey\"";
				commentView.Content = Regex.Replace(commentView.Content, pattern, replacement);
			}
			catch (Exception ex)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private async Task SearchForm(EditContext editContext) 
		{
			await GetInventoryStatus();
			if (inventorySummaryDtos.Count() <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private async Task GetInventoryStatus()
		{
			try
			{
				inventorySummaryDtos = await weeklyreportApi.GetInventoryReportByRangeDate(daterangeSearch);
				//inventorySummaryDtos = inventorySummaryDtos.OrderByDescending(s => s.ItmsGrpNam).ToList();
				sumBeginQty = inventorySummaryDtos.Sum(s => s.BeginQty);
				sumEndQty = inventorySummaryDtos.Sum(s => s.EndQty);
				sumInQty = inventorySummaryDtos.Sum(s => s.InQty);
				sumOutQty = inventorySummaryDtos.Sum(s => s.OutQty);
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
	}
}
