using BWERP.Api.Entities;
using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface ICommentRepository
	{
		Task<PagedList<CommentViewRequest>> GetListComment(int pageNumber, int pageSize);
		Task<Comment> GetCommentById(int id);
		Task<Comment> GetCommentByDeptId(int id);
		Task<Comment> Create(Comment cmt);
		Task<Comment> Update(Comment cmt);
		Task<Comment> Delete(Comment cmt);
	}
}
