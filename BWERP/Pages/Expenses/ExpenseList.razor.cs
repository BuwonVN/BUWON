using Blazored.Toast.Services;
using BWERP.Models.Exppense;
using BWERP.Models.User;
using Microsoft.AspNetCore.Components.Forms;
using static BWERP.Pages.Components.SearchByMonth;
using System.Globalization;
using BWERP.Models.ExpenseCategory;
using Microsoft.AspNetCore.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Repositories.Services;
using BWERP.Shared;

namespace BWERP.Pages.Expenses
{
    public partial class ExpenseList
    {
        private ExpenseSearch expenseSearch = new ExpenseSearch();
        private List<ExpenseView> expenseView = new List<ExpenseView>();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();


		private List<int> years = new List<int>();
		private Dictionary<int, string> months = new Dictionary<int, string>();

		[Inject] private IExpenseApiClient expenseApiClient { get; set; }

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }
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
				var pagingResponse = await expenseApiClient.GetListExpense(expenseSearch);
				expenseView = pagingResponse.Items;
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
			expenseSearch.PageNumber = page;
			await GetListExpense();
		}
	}
}
