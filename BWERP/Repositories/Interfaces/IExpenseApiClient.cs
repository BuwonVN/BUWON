using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.Menu;
using BWERP.Models.Role;
using BWERP.Models.SeedWork;

namespace BWERP.Repositories.Interfaces
{
	public interface IExpenseApiClient
	{
		Task<List<ExpenseCategoryView>> GetCategory();
		Task<PagedList<ExpenseView>> GetListExpense(ExpenseSearch expenseSearch);
		Task<ExpenseView> GetExpenseById(int id);
		Task<bool> CreateExpense(ExpenseCreateDto request);
		Task<bool> UpdateExpense(int id, ExpenseUpdateDto request);
		Task<bool> DeleteExpense(int id);
	}
}
