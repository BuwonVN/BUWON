using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.Menu;
using BWERP.Models.Role;
using BWERP.Models.SeedWork;
using BWERP.Models.User;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BWERP.Repositories.Services
{
	public class ExpenseApiClient : IExpenseApiClient
	{
		public HttpClient _httpClient;
		public ExpenseApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<ExpenseCategoryView>> GetCategory()
		{
			var result = await _httpClient.GetFromJsonAsync<List<ExpenseCategoryView>>($"/api/expenses/category");
			return result;
		}

		public async Task<ExpenseView> GetExpenseById(int id)
		{
			var result = await _httpClient.GetFromJsonAsync<ExpenseView>($"/api/expenses/{id}");
			return result;
		}
		public async Task<bool> CreateExpense(ExpenseCreateDto request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/expenses", request);
			return result.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateExpense(int id, ExpenseUpdateDto request)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/expenses/{id}", request);
			return result.IsSuccessStatusCode;
		}
		public Task<bool> DeleteExpense(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<PagedList<ExpenseView>> GetListExpense(ExpenseSearch expenseSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = expenseSearch.PageNumber.ToString()
			};

			if (expenseSearch.Year.HasValue)
				queryStringParam.Add("year", expenseSearch.Year.ToString());

			if (expenseSearch.Month.HasValue)
				queryStringParam.Add("month", expenseSearch.Month.ToString());

			if (!string.IsNullOrEmpty(expenseSearch.CreatedUser))
				queryStringParam.Add("createduser", expenseSearch.CreatedUser);

			if (expenseSearch.CategoryId.HasValue)
				queryStringParam.Add("categoryid", expenseSearch.CategoryId.ToString());

			string url = QueryHelpers.AddQueryString($"/api/expenses", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<PagedList<ExpenseView>>(url);
			return result;
		}

		public async Task<List<ExpenseView>> GetListExpenseNoPaging(ExpenseSearch expenseSearch)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = expenseSearch.PageNumber.ToString()
			};

			if (expenseSearch.Year.HasValue)
				queryStringParam.Add("year", expenseSearch.Year.ToString());

			if (expenseSearch.Month.HasValue)
				queryStringParam.Add("month", expenseSearch.Month.ToString());

			if (!string.IsNullOrEmpty(expenseSearch.CreatedUser))
				queryStringParam.Add("createduser", expenseSearch.CreatedUser);

			if (expenseSearch.CategoryId.HasValue)
				queryStringParam.Add("categoryid", expenseSearch.CategoryId.ToString());

			string url = QueryHelpers.AddQueryString($"/api/expenses/nopaging", queryStringParam);

			var result = await _httpClient.GetFromJsonAsync<List<ExpenseView>>(url);
			return result;
		}
	}
}
