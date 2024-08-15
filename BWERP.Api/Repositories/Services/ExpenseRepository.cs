using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using BWERP.Models.Role;
using BWERP.Models.SeedWork;
using BWERP.Models.User;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class ExpenseRepository : IExpenseRepository
	{
		private readonly IConfiguration _configuration;
		private readonly string _dbMain;
		public ExpenseRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			_dbMain = _configuration.GetConnectionString("MainDBDatabase");
		}
		private IDbConnection sqlconMain => new SqlConnection(_dbMain);

		public async Task<Expense> Create(Expense exp)
		{
			try
			{
				string insertSql = @"INSERT INTO Expenses(DepartmentId, CategoryId, Description, CreatedDate, CreatedUser, Amount, Note) 
                             VALUES(@DepartmentId, @CategoryId, @Description, @CreatedDate, @CreatedUser, @Amount, @Note)";

				await sqlconMain.ExecuteAsync(insertSql, new
				{
					exp.DepartmentId,
					exp.CategoryId,
					exp.Description,
					exp.CreatedDate,
					exp.CreatedUser,
					exp.Amount,
					exp.Note
				});
			}
			catch (Exception ex)
			{
				//This preserves the original stack trace.
				throw;
			}

			return exp;
		}

		public async Task<Expense> GetExpenseById(int id)
		{
			try
			{
				string query = "SELECT * FROM Expenses WHERE Id = @Id";
				return await sqlconMain.QueryFirstOrDefaultAsync<Expense>(query, new { Id = id });
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<ExpenseCategoryView>> GetExpenseCategory()
		{
			try
			{
				var query = "select ID, Name from ExpenseCategory";
				var data = await sqlconMain.QueryAsync<ExpenseCategoryView>(query);
				return data.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<PagedList<ExpenseView>> GetListExpense(ExpenseSearch expenseSearch)
		{
			try
			{
				var parameters = new 
				{ 
					expenseSearch.Year, 
					expenseSearch.Month, 
					expenseSearch.CreatedUser,
					CategoryId = (object)expenseSearch.CategoryId ?? DBNull.Value
				}; 
				var data = await sqlconMain.QueryAsync<ExpenseView>("spExpenseGetAll", parameters, commandType: CommandType.StoredProcedure);

				var pagedData = data.Skip((expenseSearch.PageNumber - 1) * expenseSearch.PageSize).Take(expenseSearch.PageSize).ToList();
				var totalCount = data.Count();

				return new PagedList<ExpenseView>(pagedData, totalCount, expenseSearch.PageNumber, expenseSearch.PageSize);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Expense> Update(Expense exp)
		{
			try
			{
				string updateSql = @"UPDATE Expenses SET CategoryId=@CategoryId, Description=@Description, CreatedDate=@CreatedDate, Amount=@Amount, Note=@Note 
                             WHERE Id=@Id";

				await sqlconMain.ExecuteAsync(updateSql, new
				{
					exp.Id,
					exp.CategoryId,
					exp.Description,
					exp.CreatedDate,
					exp.Amount,
					exp.Note
				});
			}
			catch (Exception ex)
			{
				throw;
			}

			return exp;
		}
	}
}
