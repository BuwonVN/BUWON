using BWERP.Models.Comment;
using BWERP.Models.Department;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Comment
{
	public partial class CommentCreate
	{
		private CommentCreateRequest commentCreate = new CommentCreateRequest();
		private List<CommentViewRequest> commentView;
		private List<DepartmentViewDto> departmentViewDtos;

		protected override async Task OnInitializedAsync()
		{
			departmentViewDtos = await departmentApiClient.GetDepartmentList();
			commentCreate.CreatedDate = DateTime.Now;
		}

		private async Task SubmitTask(EditContext context)
		{
			//VALIDATE DEPARTMENT
			//if (dailyrpt.DepartmentId == 0)
			//{
			//	toastService.ShowError($"Department can not be null. Please check again.!");
			//	return;
			//}
			//AUTHORIZE
			var authState = await AuthenticationStateProvider
				.GetAuthenticationStateAsync();
			//GET USERID
			var userid = authState.User.FindFirst(c => c.Type.Contains("UserId"))?.Value;
			commentCreate.CreatedBy = Guid.Parse(userid);
			commentCreate.UpdatedBy = Guid.Parse(userid);

			var result = await commentApi.CreateComment(commentCreate);
			if (result)
			{
				toastService.ShowSuccess($"Comment has been created successfully.");
				NavigationManager.NavigateTo("/comment/commentlist");
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");
			}
		}
	}
}
