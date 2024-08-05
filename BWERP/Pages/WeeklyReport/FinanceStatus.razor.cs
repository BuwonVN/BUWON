using Blazored.Toast.Services;
using BWERP.Models.Comment;
using BWERP.Models.WeelkyReport;
using BWERP.Pages.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.LineChart;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;

namespace BWERP.Pages.WeeklyReport
{
	public partial class FinanceStatus
	{
		[Parameter]
		public int commentid { get; set; }
		public int year { get; set; }
		public int month { get; set; }
		public double iTotalAmount, iInterest;
		public double dRate { get; set; }
		private int isumbegin, isumincrease, isumdecrease, isumend;
		private bool isDivVisible = false;
		//INJECTION
		[Inject] private ICommentApiClient commentApi { get; set; }
		[Inject] private IWeeklyReportApiClient weeklyreportApi { get; set; }
		[Inject] IToastService toastService { set; get; }

		private List<OutstandingReceivableDto> _outstandingDtos;
		private List<OutstandingReceivableDto> _getfirstrow;
		private List<OutstandingReceivableDetailDto> _outstandingdetailDto;

		private CommentViewRequest commentView { set; get; }

		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			year = DateTime.Now.Year;
			month = DateTime.Now.Month;
			try
			{
				await GetData();
				commentView = await commentApi.GetCommentByDeptId(8);

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
			var searchModel = (SearchByMonth.SearchModel)editContext.Model;
			year= searchModel.Year;
			month = searchModel.Month;

			await GetData();
			if (_outstandingDtos.Count() <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private void ToggleDivVisibility()
		{
			isDivVisible = !isDivVisible;
		}

		private async Task GetData()
		{
			try
			{
				_outstandingDtos = await weeklyreportApi.GetOutstandingReceivable(year, month);
				_getfirstrow = _outstandingDtos.Skip(0).Take(1).ToList();
				iTotalAmount = _outstandingDtos.Where(s => s.Department == "To").Select(s => s.BeginAmount).FirstOrDefault();
				dRate = _outstandingDtos.Where(s => s.Department == "Ra").Select(s => s.BeginAmount).FirstOrDefault();
				dRate = Math.Round(dRate * 100, 2);

				iInterest = _outstandingDtos.Where(s => s.Department == "In").Select(s => s.BeginAmount).FirstOrDefault();

				//Get Outstanding Receivable Detail
				_outstandingdetailDto = await weeklyreportApi.GetOutstandingReceivable_Detail(year, month);
				isumbegin = _outstandingdetailDto.Sum(s => s.BeginAmount);
				isumincrease = _outstandingdetailDto.Sum(s => s.IncreaseAmount);
				isumdecrease = _outstandingdetailDto.Sum(s => s.DecreaseAmount);
				isumend = _outstandingdetailDto.Sum(s => s.EndAmount);
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		
	}
}
