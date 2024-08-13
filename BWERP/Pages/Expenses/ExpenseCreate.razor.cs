using Blazored.Toast.Services;
using BWERP.Models.Exppense;
using BWERP.Repositories.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Models.ExpenseCategory;

namespace BWERP.Pages.Expenses
{
	public partial class ExpenseCreate
	{
		private ExpenseCreateDto expenseCreateDto = new ExpenseCreateDto();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();

		[Inject] private IExpenseApiClient expenseApiClient { get; set; }
		protected override async Task OnInitializedAsync()
		{
			expenseCategory = await expenseApiClient.GetCategory();
			expenseCreateDto.CreatedDate= DateTime.Now;
		}
		private async Task SubmitTask(EditContext context)
		{
			expenseCreateDto.CreatedUser = "admin";
			var result = await expenseApiClient.CreateExpense(expenseCreateDto);
			if (result)
			{
				toastService.ShowSuccess($"Upload successfully.");
				NavigationManager.NavigateTo("/expenses/expenselist");
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");

			}
		}
	}
}
