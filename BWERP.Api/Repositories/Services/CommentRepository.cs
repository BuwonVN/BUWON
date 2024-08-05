using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Comment;
using BWERP.Models.Enums;
using BWERP.Models.SeedWork;
using BWERP.Models.WeelkyReport;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using System.Data;

namespace BWERP.Api.Repositories.Services
{
	public class CommentRepository : ICommentRepository
	{
		private readonly MainContext _mainContext;
		//TEST ADO.NET
		private readonly IConfiguration _configuration;
		private const string Main_Database = "MainDBDatabase";
		//private const string SELECT_BUG = "select * from bugs";
		//
		public CommentRepository(MainContext mainContext,
								IConfiguration configuration)
		{
			_mainContext = mainContext;
			_configuration = configuration;
		}
		public async Task<Comment> Create(Comment cmt)
		{
			await _mainContext.Comments.AddAsync(cmt);
			await _mainContext.SaveChangesAsync();
			return cmt;
		}

		public async Task<Comment> Delete(Comment cmt)
		{
			_mainContext.Comments.Remove(cmt);
			await _mainContext.SaveChangesAsync();
			return cmt;
		}

		public async Task<Comment> GetCommentByDeptId(int id)
		{
			//var query = from m in _mainContext.Comments.Where(x => x.Function == (Functions)id)
			//			select m;
			var query = from m in _mainContext.Comments.Where(x => x.DepartmentId == id)
						select m;
			return await query.OrderByDescending(x => x.Id).Skip(0).Take(1).FirstOrDefaultAsync();
		}

		public async Task<Comment> GetCommentById(int id)
		{
			//return await _mainContext.Comments.FindAsync(id);
			var query = _mainContext.Comments
				.Include(x => x.Department).AsQueryable();
			query = query.Where(t => t.Id == id);
			return query.FirstOrDefault();
		}

		public async Task<PagedList<CommentViewRequest>> GetListComment(int pageNumber, int pageSize)
		{
			IEnumerable<CommentViewRequest> result;
			using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString(Main_Database)))
			{
				try
				{
					db.Open();
					var query = @"select a.Id,DepartmentId, b.Name FuncName, Content,d.UserName as CreatedUser, a.CreatedDate, UpdatedBy, UpdatedDate, c.UserName UpdatedUser
					from Comments a inner join Departments b on a.DepartmentId = b.Id left join AppUsers c on a.UpdatedBy=c.Id
					left join AppUsers d on a.CreatedBy=d.Id
					order by a.UpdatedDate desc";
					var data = await db.QueryAsync<CommentViewRequest>(query);

					var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
					var totalCount = data.Count();

					return new PagedList<CommentViewRequest>(pagedData, totalCount, pageNumber, pageSize);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public async Task<Comment> Update(Comment cmt)
		{
			_mainContext.Comments.Update(cmt);
			await _mainContext.SaveChangesAsync();
			return cmt;
		}
	}
}
