using Blazored.Toast.Services;
using BWERP.Models.Comment;
using BWERP.Models.WeelkyReport;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;

namespace BWERP.Pages.WeeklyReport
{
	public partial class ProductionStatus
	{
		[Parameter]
		public int commentid { get; set; }
		//MBCP variables
		private int isumPlan_W, isumPlan_M, isumProQty_M, isumProQty;
		private double dProdRate_W, dProdRate_M;

		//INJECTION
		[Inject] private ICommentApiClient commentApi { get; set; }
		[Inject] private IWeeklyReportApiClient weeklyreportApi { get; set; }
		[Inject] IToastService toastService { set; get; }

		private CommentViewRequest ViewMBCP { set; get; }
		private CommentViewRequest ViewIP { set; get; }
		private CommentViewRequest ViewOS { set; get; }

		private List<MBCPProductionDto> mbcpProductionDtos;
		private List<MBCPProductionDto> viewmbcpProd;

		private List<MBCPMonthlyProdDto> mbcpMonthlyProdDtos;
		private List<OSProductionDto> oSProductionDtos;
		private List<OSProductionDto> viewosProd;

		private List<IPOSProductionDto> _ipProdDtos;
		private List<IPOSProductionDto> viewipProd;

		private List<IPOSMonthlyProdDto> _iposMonthlyProdDtos;

		private List<OSMonthlyProdDto> oSMonthlyProdDtos;

		private List<ViewPlanningDto> viewPlanningDtos;

		private BarConfig _IPbarConfig;
		private BarConfig _OSbarConfig;
		private LineConfig _MBCPlineConfig;

		private DateRangeSearch daterangeSearch = new DateRangeSearch();

		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			(DateTime previousMonday, DateTime previousSaturday) = GetPreviousWeekRange();
			daterangeSearch.FromDate = previousMonday;
			daterangeSearch.ToDate = previousSaturday;

			try
			{
				ViewMBCP = await commentApi.GetCommentByDeptId(3);
				string mbcp_pattern = @"<thead(?![^>]*\bclass\b)";
				string mbcp_replacement = "<thead class=\"table-grey\"";
				ViewMBCP.Content = Regex.Replace(ViewMBCP.Content, mbcp_pattern, mbcp_replacement);

				ViewIP = await commentApi.GetCommentByDeptId(4);
				string ippattern = @"<thead(?![^>]*\bclass\b)";
				string ipreplacement = "<thead class=\"table-grey\"";
				ViewIP.Content = Regex.Replace(ViewIP.Content, ippattern, ipreplacement);

				ViewOS = await commentApi.GetCommentByDeptId(5);
				string pattern = @"<thead(?![^>]*\bclass\b)";
				string replacement = "<thead class=\"table-grey\"";
				ViewOS.Content = Regex.Replace(ViewOS.Content, pattern, replacement);

				await GetData();
				await GetMonthlyProd();
				
			}
			catch (Exception ex)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private async Task SearchForm(EditContext editContext)
		{
			await GetData();
			//await GetMonthlyProd();

			if (_ipProdDtos.Count() <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
			if (oSProductionDtos.Count() <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		public static (DateTime, DateTime) GetPreviousWeekRange()
		{
			DateTime now = DateTime.Now;
			int daysToSubtract = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7 + 7;
			DateTime previousMonday = now.AddDays(-daysToSubtract);
			DateTime previousSaturday = previousMonday.AddDays(5);
			return (previousMonday.Date, previousSaturday.Date);
		}
		private async Task GetMonthlyProd()
		{
			try
			{
				mbcpMonthlyProdDtos = await weeklyreportApi.Get_MBCP_MonthlyProd(daterangeSearch);
				LoadMBCPLineChart(mbcpMonthlyProdDtos);

				_iposMonthlyProdDtos = await weeklyreportApi.GetIPMonthlyProductions(daterangeSearch);
				LoadIPBarChart(_iposMonthlyProdDtos);

				oSMonthlyProdDtos = await weeklyreportApi.GetOSMonthlyProductions(daterangeSearch);
				LoadOSBarChart(oSMonthlyProdDtos);
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		
		//Get data
		private async Task GetData()
		{
			try
			{
				viewPlanningDtos = await weeklyreportApi.GetPlanningQty(daterangeSearch);

				//Get MBCP Production
				mbcpProductionDtos = await weeklyreportApi.Get_MBCP_ProductionByRangeDate(daterangeSearch);
				var mbcpdata = from a in mbcpProductionDtos
							   join b in viewPlanningDtos on a.CustomerName equals b.Dept into prodplan
							   from b in prodplan.DefaultIfEmpty()
							   select new MBCPProductionDto
							   {
								   CustomerName = a.CustomerName,
								   ProQty = a.ProQty,
								   ProQty_M = a.ProQty_M,
								   PlanQty_W = b != null ? b.PlanQty_W : 0,
								   PlanQty_M = b != null ? b.PlanQty_M : 0,
								   ProductionRate_W = b != null && b.PlanQty_W != 0 ? (double)a.ProQty / b.PlanQty_W : 0,
								   ProductionRate_M = b != null && b.PlanQty_M != 0 ? (double)a.ProQty_M / b.PlanQty_M : 0

							   };
				viewmbcpProd = mbcpdata.ToList();

				isumPlan_W = mbcpdata.Sum(s => s.PlanQty_W);
				isumPlan_M = mbcpdata.Sum(s => s.PlanQty_M);
				isumProQty = mbcpdata.Sum(s => s.ProQty);
				isumProQty_M = mbcpdata.Sum(s => s.ProQty_M);
				dProdRate_W = (double)isumProQty / isumPlan_W;
				dProdRate_M = (double)isumProQty_M / isumPlan_M;

				//Get IP Production
				_ipProdDtos = await weeklyreportApi.GetIPProduction(daterangeSearch);
				var ipdata = from a in _ipProdDtos
							 join b in viewPlanningDtos on a.Dept equals b.Dept
							 select new IPOSProductionDto
							 {
								 Dept = a.Dept,
								 ProQty = a.ProQty,
								 ProQty_M = a.ProQty_M,
								 PlanQty = b.PlanQty_W,
								 PlanQty_M = b.PlanQty_M,
								 DefectQty = a.DefectQty,
								 DefectQty_M= a.DefectQty_M,
								 DefectRate = a.DefectRate,
								 DefectRate_M = a.DefectRate_M,
								 ProductionRate = b.PlanQty_W != 0 ? (double)a.ProQty / b.PlanQty_W : 0,
								 ProductionRate_M = b.PlanQty_M != 0 ? (double)a.ProQty_M / b.PlanQty_M : 0

							 };
				viewipProd = ipdata.ToList();
				//Get OS
				oSProductionDtos = await weeklyreportApi.GetOSProduction(daterangeSearch);
				var osdata = from a in oSProductionDtos
							 join b in viewPlanningDtos on a.Dept equals b.Dept
							 select new OSProductionDto
							 {
								 Dept = a.Dept,
								 ProQty = a.ProQty,
								 ProQty_M = a.ProQty_M,
								 PlanQty = b.PlanQty_W,
								 PlanQty_M = b.PlanQty_M,
								 DefectQty = a.DefectQty,
								 DefectQty_M = a.DefectQty_M,
								 DefectRate = a.DefectRate,
								 DefectRate_M = a.DefectRate_M,
								 ProductionRate = b.PlanQty_W != 0 ? (double)a.ProQty / b.PlanQty_W : 0,
								 ProductionRate_M = b.PlanQty_M != 0 ? (double)a.ProQty_M / b.PlanQty_M : 0

							 };
				viewosProd = osdata.ToList();
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}

		private void LoadIPBarChart(List<IPOSMonthlyProdDto> monthlyProd)
		{
			// Initialize _barConfig with BarOptions and BarData

			_IPbarConfig = new BarConfig
			{
				Options = new BarOptions
				{
					Responsive = true,
					Title = new OptionsTitle
					{
						Display = true,
						Text = "IP Production in 2024"
					}
				}
			};

			// Define the array of month names
			foreach (string months in new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Otc", "Nov", "Dec" })
			{
				_IPbarConfig.Data.Labels.Add(months);
			}
			// Define the first dataset values and background colors
			//int[] dataValues1 = { 12, 20, 9, 25, 12, 20, 9, 25, 12, 20, 9, 25 };
			var dataValues1 = monthlyProd.Select(s => s.ProQty).ToArray();
			string[] backgroundColors1 = Enumerable.Repeat(ColorUtil.ColorHexString(75, 192, 192), dataValues1.Length).ToArray();

			// Create the first BarDataset with the data values and background colors
			BarDataset<int> dataset1 = new BarDataset<int>(dataValues1)
			{
				BackgroundColor = backgroundColors1,
				Label = "Production"
			};

			// Define the second dataset values and background colors
			var dataValues2 = monthlyProd.Select(s => s.DefectQty).ToArray();
			string[] backgroundColors2 = Enumerable.Repeat(ColorUtil.ColorHexString(255, 99, 132), dataValues2.Length).ToArray();

			// Create the second BarDataset with the data values and background colors
			BarDataset<int> dataset2 = new BarDataset<int>(dataValues2)
			{
				BackgroundColor = backgroundColors2,
				Label = "Defect"
			};

			// Add both datasets to the Data property
			_IPbarConfig.Data.Datasets.Add(dataset1);
			_IPbarConfig.Data.Datasets.Add(dataset2);
		}

		private void LoadOSBarChart(List<OSMonthlyProdDto> _osmonthly)
		{
			// Initialize _barConfig with BarOptions and BarData
			_OSbarConfig = new BarConfig
			{
				Options = new BarOptions
				{
					Responsive = true,
					Title = new OptionsTitle
					{
						Display = true,
						Text = "OS Production in 2024"
					}
				}
			};

			// Define the array of month names
			foreach (string months in new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Otc", "Nov", "Dec" })
			{
				_OSbarConfig.Data.Labels.Add(months);
			}

			// Define the first dataset values and background colors
			int[] dataValues1 = _osmonthly.Select(s => s.ProQty).ToArray();
			string[] backgroundColors1 = Enumerable.Repeat(ColorUtil.ColorHexString(75, 192, 192), dataValues1.Length).ToArray();

			// Create the first BarDataset with the data values and background colors
			BarDataset<int> dataset1 = new BarDataset<int>(dataValues1)
			{
				BackgroundColor = backgroundColors1,
				Label = "Production"
			};

			// Define the second dataset values and background colors
			var dataValues2 = _osmonthly.Select(s => s.DefectQty).ToArray();
			string[] backgroundColors2 = Enumerable.Repeat(ColorUtil.ColorHexString(255, 99, 132), dataValues2.Length).ToArray();

			// Create the second BarDataset with the data values and background colors
			BarDataset<int> dataset2 = new BarDataset<int>(dataValues2)
			{
				BackgroundColor = backgroundColors2,
				Label = "Defect"
			};

			// Add both datasets to the Data property
			_OSbarConfig.Data.Datasets.Add(dataset1);
			_OSbarConfig.Data.Datasets.Add(dataset2);
		}

		private void LoadMBCPLineChart(List<MBCPMonthlyProdDto> _mbcpMonthlyProd)
		{
			_MBCPlineConfig = new LineConfig
			{
				Options = new LineOptions
				{
					Responsive = true,
					Title = new OptionsTitle
					{
						Display = true,
						Text = "MBCP Production in 2024"
					}
				}
			};

			// Define the array of month names
			foreach (string months in new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Otc", "Nov", "Dec" })
			{
				_MBCPlineConfig.Data.Labels.Add(months);
			}
			var dataValues1 = _mbcpMonthlyProd.Select(s => s.MB_ProQty).ToArray();
			var dataValues2 = _mbcpMonthlyProd.Select(s => s.CP_ProQty).ToArray();

			LineDataset<int> dataset1 = new LineDataset<int>(dataValues1)
			{
				Label = "Master Batch",
				BackgroundColor = "rgba(75, 192, 192, 0.2)",
				BorderColor = "rgba(75, 192, 192, 1)",
				BorderWidth = 1
			};

			LineDataset<int> dataset2 = new LineDataset<int>(dataValues2)
			{
				Label = "Compound",
				BackgroundColor = "rgba(255, 99, 132, 0.2)",
				BorderColor = "rgba(255, 99, 132, 1)",
				BorderWidth = 1
			};

			_MBCPlineConfig.Data.Datasets.Add(dataset1);
			_MBCPlineConfig.Data.Datasets.Add(dataset2);
		}
	}
}
