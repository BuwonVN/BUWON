using Blazored.Toast.Services;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Pages.Components;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Menus
{
	public partial class MenuList
	{
		private int DeleteId { set; get; }
		protected Confirmation DeleteConfirmation { set; get; }

		[Inject] private IMenuApiClient menuApiClient { get; set; }
		[Inject] IToastService toastService { set; get; }
		[Inject] NavigationManager NavigationManager { get; set; }
		private MenuListSearch menuListSearch = new MenuListSearch();
		private List<MenuViewRequest> menuViewRequests;

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await GetListMenu();			
		}
		private async Task GetListMenu()
		{
			try
			{
				var pagingResponse = await menuApiClient.GetListMenu(menuListSearch);
				menuViewRequests = pagingResponse.Items;
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
			menuViewRequests.Clear();
			await GetListMenu();
			if (menuViewRequests.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		//CONFIRM DELETE
		public void OnDeleteTask(int deleteId)
		{
			DeleteId = deleteId;
			DeleteConfirmation.Show();
		}
		public async Task OnConfirmDeleteTask(bool deleteConfirmed)
		{
			if (deleteConfirmed)
			{
				toastService.ShowInfo("Task has been deleted successfully.");
				var isSuccessed = await menuApiClient.DeleteMenu(DeleteId);
				if (isSuccessed)
				{
					await GetListMenu();
				}
			}
		}
		//PAGING
		private async Task SelectedPage(int page)
		{
			menuListSearch.PageNumber = page;
			await GetListMenu();
		}
		
	}
}
