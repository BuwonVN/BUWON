using Blazored.Toast.Services;
using BWERP.Models.Comment;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Models.User;
using BWERP.Pages.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Users
{
	public partial class UserList
    {
		private Guid DeleteId { set; get; }

		protected Confirmation DeleteConfirmation { set; get; }
		protected RoleAssign AssignUserDialog { set; get; }

		[Inject] private IUserApiClient _userApiClient { get; set; }
        [Inject] IToastService toastService { set; get; }
		private UserListSearch _searchUser = new UserListSearch();
		private List<UserViewRequest> userViewRequests;
        public MetaData MetaData { get; set; } = new MetaData();
        [CascadingParameter]
        private Error? _error { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetListUsers();
        }
		private async Task GetListUsers()
		{
			try
			{
				var pagingResponse = await _userApiClient.GetListUser(_searchUser);
				userViewRequests = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		//PAGING
		private async Task SelectedPage(int page)
		{
			_searchUser.PageNumber = page;
			await GetListUsers();
		}
		//SEARCH USER
		private async Task SearchForm(EditContext editContext)
		{
			userViewRequests.Clear();
			await GetListUsers();
			if (userViewRequests.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}

		//CONFIRM DELETE
		public void OnDeleteTask(Guid deleteId)
		{
			DeleteId = deleteId;
			DeleteConfirmation.Show();
		}
		public async Task OnConfirmDeleteTask(bool deleteConfirmed)
		{
			if (deleteConfirmed)
			{
				toastService.ShowInfo("User has been deleted successfully.");
				var isSuccessed = await _userApiClient.DeleteUser(DeleteId);
				if (isSuccessed)
				{
					await GetListUsers();
				}
			}
		}
		//ASSIGN ROLE
		public void OpenAssignPopup(Guid id)
		{
			AssignUserDialog.Show(id);
		}

		public async Task AssignTaskSuccess(bool result)
		{
			if (result)
			{
				await GetListUsers();
			}
		}
	}
}
