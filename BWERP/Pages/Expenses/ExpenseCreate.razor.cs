using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Expenses
{
	public partial class ExpenseCreate
	{
		private ExpenseCreateDto expenseCreateDto = new ExpenseCreateDto();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();

		protected override async Task OnInitializedAsync()
		{
			expenseCategory = await expenseApiClient.GetCategory();
			expenseCreateDto.CreatedDate= DateTime.Now;
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
