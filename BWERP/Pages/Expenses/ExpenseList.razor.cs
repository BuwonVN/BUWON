using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;

namespace BWERP.Pages.Expenses
{
	public partial class ExpenseList
    {
        private ExpenseSearch expenseSearch = new ExpenseSearch();
        private List<ExpenseView> expenseView = new List<ExpenseView>();
		private List<ExpenseView> expenseNoPaging = new List<ExpenseView>();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();

		private List<int> years = new List<int>();
		private Dictionary<int, string> months = new Dictionary<int, string>();

		[Inject] private IExpenseApiClient expenseApiClient { get; set; }

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }
		//DECLARE VARIABLES
		private double sPayment, categorySubtotal;
		private string username;
		protected override async Task OnInitializedAsync()
		{
			// Populate years
			int endYear = DateTime.Now.Year;

			for (int year = endYear - 15; year <= endYear; year++)
			{
				years.Add(year);
			}

			// Populate months
			for (int month = 1; month <= 12; month++)
			{
				months.Add(month, DateTimeFormatInfo.CurrentInfo.GetMonthName(month));
			}

			// Initialize default selections
			expenseSearch.Year = DateTime.Now.Year;
			expenseSearch.Month = DateTime.Now.Month;

			expenseCategory = await expenseApiClient.GetCategory();

			//AUTHORIZE
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			username = authState.User.Identity.Name;

			expenseSearch.CreatedUser = username;
			await GetListExpense();
		}
		//SEARCH DATA
		private async Task SearchForm(EditContext editContext)
        {
			expenseView.Clear();
			await GetListExpense();
			if (expenseView.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private async Task GetListExpense()
		{
			try
			{
				//GET EXPENSES WITHOUT PAGING
				expenseNoPaging = await expenseApiClient.GetListExpenseNoPaging(expenseSearch);

				//GET EXPENSES WITH PAGING
				var pagingResponse = await expenseApiClient.GetListExpense(expenseSearch);
				expenseView = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;

				sPayment = expenseView.Select(s => s.TotalAmount).FirstOrDefault();
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		//PAGING
		private async Task SelectedPage(int page)
		{
			expenseSearch.PageNumber = page;
			await GetListExpense();
		}
		//private async Task ExportToExcel()
		//{
		//	// Call the JavaScript function to export the data
		//	await JSRuntime.InvokeVoidAsync("exportDataToExcel", expenseView, "expenses.xlsx");
		//}
		private async Task ExportToExcel()
		{
			//Get DATA
			await GetListExpense(); 

			// Replace with LicenseContext.Commercial if applicable
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Expenses");

				// Add Headers
				worksheet.Cells[1, 1].Value = "Category";
				worksheet.Cells[1, 2].Value = "Description";
				worksheet.Cells[1, 3].Value = "Created date";
				worksheet.Cells[1, 4].Value = "Amount";
				worksheet.Cells[1, 5].Value = "Remark";
				worksheet.Cells[1, 6].Value = "Created by";

				// Apply styles to header
				using (var range = worksheet.Cells[1, 1, 1, 6])
				{
					range.Style.Font.Bold = true;
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
				}

				int row = 2;
				double grandTotal = 0;
				var groupedExpenses = expenseNoPaging.GroupBy(x => x.CategoryName);

				foreach (var group in groupedExpenses)
				{
					double categorySubtotal = 0;
					int groupStartRow = row; // Track the start row for the current category group

					foreach (var item in group)
					{
						worksheet.Cells[row, 1].Value = item.CategoryName;
						worksheet.Cells[row, 2].Value = item.Description;
						worksheet.Cells[row, 3].Value = item.CreatedDate.ToString("yyyy-MM-dd");
						worksheet.Cells[row, 4].Value = item.Amount;
						worksheet.Cells[row, 4].Style.Numberformat.Format = "#,##0";
						worksheet.Cells[row, 5].Value = item.Note;
						worksheet.Cells[row, 6].Value = item.CreatedUser;

						categorySubtotal += item.Amount;
						row++;
					}
					// Merge Category cells for this group
					if (group.Count() > 1) // Only merge if there's more than one row in the group
					{
						worksheet.Cells[groupStartRow, 1, row - 1, 1].Merge = true;
						worksheet.Cells[groupStartRow, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
					}

					// Add Subtotal row
					worksheet.Cells[row, 1, row, 3].Merge = true;
					worksheet.Cells[row, 1].Value = $"Subtotal for {group.Key}";
					worksheet.Cells[row, 1].Style.Font.Bold = true;

					worksheet.Cells[row, 4].Value = categorySubtotal;
					worksheet.Cells[row, 4].Style.Numberformat.Format = "#,##0";
					worksheet.Cells[row, 4].Style.Font.Bold = true;

					// Apply background color to the subtotal row
					using (var range = worksheet.Cells[row, 1, row, 6])
					{
						range.Style.Fill.PatternType = ExcelFillStyle.Solid;
						range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
					}

					grandTotal += categorySubtotal;
					row++;
				}
				// Add Total row at the end
				worksheet.Cells[row, 1, row, 3].Merge = true;
				worksheet.Cells[row, 1].Value = "Total";
				worksheet.Cells[row, 1].Style.Font.Bold = true;
				worksheet.Cells[row, 4].Value = grandTotal;
				worksheet.Cells[row, 4].Style.Numberformat.Format = "#,##0";
				worksheet.Cells[row, 4].Style.Font.Bold = true;

				// Apply background color to the Total row
				using (var range = worksheet.Cells[row, 1, row, 6])
				{
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
				}

				worksheet.Cells.AutoFitColumns();

				var stream = new MemoryStream();
				package.SaveAs(stream);

				string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

				var fileName = $"expense_{timestamp}.xlsx"; ;
				var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

				stream.Position = 0;

				// Navigate to the download endpoint with file data
				await JSRuntime.InvokeVoidAsync("downloadFile", stream.ToArray(), fileName, contentType);
			}
		}
	}
}
