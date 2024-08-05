using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;

namespace BWERP.Repositories.Interfaces
{
	public interface ICommentApiClient
	{
		Task<PagedList<CommentViewRequest>> GetListComment(CommentSearch commentSearch);
		Task<CommentViewRequest> GetCommentById(int id);
		Task<CommentViewRequest> GetCommentByDeptId(int id);
		Task<bool> CreateComment(CommentCreateRequest request);
		Task<bool> UpdateComment(int id, CommentEditRequest request);
		Task<bool> DeleteComment(int id);
	}
}
