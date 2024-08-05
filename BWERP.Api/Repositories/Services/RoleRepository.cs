using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Role;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IConfiguration _configuration;
		private readonly string _dbMain;
		public RoleRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			_dbMain = _configuration.GetConnectionString("MainDBDatabase");
		}

		private IDbConnection sqlconMain => new SqlConnection(_dbMain);

        public async Task<List<RoleViewDto>> GetListRole()
        {
			try
			{
				var query = "select ID, Name from AppRoles";
				var data = await sqlconMain.QueryAsync<RoleViewDto>(query);
				return data.ToList();
            }
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public async Task<List<RolePermissionDtos>> GetRolePermission(Guid roleId)
		{
			IEnumerable<RolePermissionDtos> rolePermissions;

			var parameters = new DynamicParameters();
			parameters.Add("roleId", roleId);

			try
			{
				string sql = "select * from AppRolePermissions where RoleId=@roleId";
				rolePermissions = await sqlconMain.QueryAsync<RolePermissionDtos>(sql, parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return rolePermissions.ToList();
		}
	}
}
