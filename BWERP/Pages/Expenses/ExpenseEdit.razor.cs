using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Expenses
{
	public partial class ExpenseEdit
	{
		[Parameter]
		public int expenseid { get; set; }

		private ExpenseUpdateDto expenseUpdate = new ExpenseUpdateDto();
		private List<ExpenseCategoryView> expenseCategory = new List<ExpenseCategoryView>();

		protected override async Task OnInitializedAsync()
		{
			expenseCategory = await expenseApiClient.GetCategory();

			var fromDb = await expenseApiClient.GetExpenseById(expenseid);
			expenseUpdate.CategoryId = fromDb.CategoryId;
			expenseUpdate.Description = fromDb.Description;
			expenseUpdate.CreatedDate = fromDb.CreatedDate;
			expenseUpdate.Amount = fromDb.Amount;
			expenseUpdate.Note = fromDb.Note;
		}

		private async Task SubmitTask(EditContext context)
		{
			var result = await expenseApiClient.UpdateExpense(expenseid, expenseUpdate);
			if (result)
			{
				toastService.ShowSuccess($"Expense has been Updated successfully.");
				NavigationManager.NavigateTo("/expenses/expenselist");
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");

			}
		}
	}
}
