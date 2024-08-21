using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Expenses
{
	public partial class ExpenseCreate
	{
		private ExpenseCreateDto expenseCreateDto = new ExpenseCreateDto();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();
		//VARIALES
		private string username;

		protected override async Task OnInitializedAsync()
		{
			expenseCategory = await expenseApiClient.GetCategory();
			expenseCreateDto.CreatedDate= DateTime.Now;

			//AUTHORIZE
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			username = authState.User.Identity.Name;

			expenseCreateDto.CreatedUser = username;
		}
		private async Task SubmitTask(EditContext context)
		{
			//VALIDATE DEPARTMENT
			if (expenseCreateDto.CategoryId == 0)
			{
				toastService.ShowError($"Please select Category.");
				return;
			}

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
