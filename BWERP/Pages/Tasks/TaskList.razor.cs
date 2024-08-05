using Blazored.Toast.Services;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Pages.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Repositories.Services;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BWERP.Pages.Tasks
{
	public partial class TaskList
	{
		private Guid DeleteId { set; get; }

		protected Confirmation DeleteConfirmation { set; get; }
        protected TaskAssign AssignTaskDialog { set; get; }

        [Inject] private ITaskApiClient taskApiClient { get; set; }
		[Inject] private IUserApiClient _userApiClient { get; set; }
		[Inject] IToastService toastService { set; get; }

		private List<TaskViewRequest> taskViewRequests;
		private List<AssigneeDto> assigneeDtos;

		private TaskListSearch taskListSearch = new TaskListSearch();

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await GetListTask();
			//Get Assignees
			assigneeDtos = await _userApiClient.GetAssignees();
		}
		private async Task GetListTask()
		{
			try
			{
				var pagingResponse = await taskApiClient.GetListTask(taskListSearch);
				taskViewRequests = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
                //throw new InvalidOperationException("Current count is over five!");
            }
            catch (Exception ex)
			{
                _error.ProcessError(ex);
			}
		}

		private async Task SearchForm(EditContext editContext)
		{
			taskViewRequests.Clear();
			await GetListTask();
			if (taskViewRequests.Count <= 0)
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
				toastService.ShowInfo("Task has been deleted successfully.");
                var isSuccessed = await taskApiClient.DeleteTask(DeleteId);
				if (isSuccessed)
				{
					await GetListTask();
				}
			}
		}
		//ASSIGN TASK
        public void OpenAssignPopup(Guid id)
        {
            AssignTaskDialog.Show(id);
        }

        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
            {
                await GetListTask();
            }
        }

        private async Task SelectedPage(int page)
        {
            taskListSearch.PageNumber = page;
            await GetListTask();
        }
    }
}
