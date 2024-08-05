using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly MainContext _mainContext;
		//
		private readonly IConfiguration _configuration;
		private const string Main_Database = "MainDBDatabase";
		public DepartmentRepository(MainContext mainContext,
			IConfiguration configuration)
		{
			_mainContext = mainContext;
			_configuration = configuration;
		}

		public async Task<List<Department>> GetDepartmentByUser(Guid userid)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", userid, DbType.Guid);

			IEnumerable<Department> dept;
			using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString(Main_Database)))
			{
				try
				{
					string readSql = @"select a.Id,a.Name from Departments a
					inner join DepartmentUser b on a.Id=b.DepartmentId where UserId=@Id order by Id";
					dept = await conn.QueryAsync<Department>(readSql, parameters);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return dept.ToList();
		}

		public async Task<List<Department>> GetDepartmentList()
		{
			return await _mainContext.Departments.ToListAsync();
		}
	}
}
