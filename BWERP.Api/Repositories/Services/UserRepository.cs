using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Comment;
using BWERP.Models.Role;
using BWERP.Models.SeedWork;
using BWERP.Models.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class UserRepository : IUserRepository
	{
		private readonly MainContext _mainContext;
		//TEST ADO.NET
		private readonly IConfiguration _configuration;
		private readonly string _dbMain;
		public UserRepository(MainContext mainContext,
							 IConfiguration configuration)
		{
			_mainContext = mainContext;
			_configuration = configuration;
			_dbMain = _configuration.GetConnectionString("MainDBDatabase");
		}
		private IDbConnection sqlconMain => new SqlConnection(_dbMain);

		public async Task<PagedList<UserViewRequest>> GetListUser(int pageNumber, int pageSize)
		{
			try
			{
				var query = "Select * from AppUsers order by LastLogin desc";
				var data = await sqlconMain.QueryAsync<UserViewRequest>(query);

				var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
				var totalCount = data.Count();

				return new PagedList<UserViewRequest>(pagedData, totalCount, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<AppUser> Create(AppUser user)
		{
			await _mainContext.Users.AddAsync(user);
			await _mainContext.SaveChangesAsync();
			return user;
		}

		public async Task<AppUser> Delete(AppUser user)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", user.Id, DbType.Guid);

			try
			{
				string deleteSql = "DELETE FROM AppUsers WHERE Id = @Id";
				await sqlconMain.ExecuteAsync(deleteSql, parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return user;
		}
		public async Task<AppUser> GetUserById(Guid id)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Guid);

			AppUser employee = new AppUser();

			try
			{
				string uSql = "Select * from AppUsers where Id = @Id";
				employee = await sqlconMain.QueryFirstOrDefaultAsync<AppUser>(uSql, parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return employee;
		}

		public async Task<AppUser> Update(AppUser user)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", user.Id, DbType.Guid);
			parameters.Add("FirstName", user.FirstName, DbType.String);
			parameters.Add("LastName", user.LastName, DbType.String);
			parameters.Add("isActive", user.isActive, DbType.Boolean);
			parameters.Add("LastLogin", user.LastLogin, DbType.DateTime);
            parameters.Add("RoleId", user.RoleId, DbType.Guid);

            try
			{
				string updateSql = "UPDATE AppUsers SET FirstName = @FirstName, LastName = @LastName, isActive = @isActive, LastLogin=@LastLogin WHERE Id = @Id";
				await sqlconMain.ExecuteAsync(updateSql, parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}

		public async Task<AppUser> ChangePassword(AppUser user)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", user.Id, DbType.Guid);
			parameters.Add("Password", user.PasswordHash, DbType.String);
			parameters.Add("SecurityStamp", user.SecurityStamp, DbType.String);

			try
			{
				string updateSql = "UPDATE AppUsers SET PasswordHash = @Password, SecurityStamp=@SecurityStamp WHERE Id = @Id";
				await sqlconMain.ExecuteAsync(updateSql, parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}

		public async Task<IEnumerable<UserRoleDto>> GetUserRole(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<AppUser>> GetListAssignee()
		{
			try
			{
				string sql = "Select * from AppUsers";
				var data = await sqlconMain.QueryAsync<AppUser>(sql);
				return data.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
