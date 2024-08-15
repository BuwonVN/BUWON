using BWERP.Api.Entities;
using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
using BWERP.Models.User;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IExpenseRepository
	{
		Task<List<ExpenseCategoryView>> GetExpenseCategory();
		Task<PagedList<ExpenseView>> GetListExpense(ExpenseSearch expenseSearch);
		Task<Expense> GetExpenseById(int id);
		Task<Expense> Create(Expense user);
		Task<Expense> Update(Expense user);
	}
}
