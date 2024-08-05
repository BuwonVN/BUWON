using Blazored.Toast.Services;
using BWERP.Models.Menu;
using BWERP.Models.Role;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using BWERP.Repositories.Interfaces;
using BWERP.Repositories.Services;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.RoleMenus
{
	public partial class RoleMenusList
	{
		[Inject] private IRoleMenuApiClient roleMenuApiClient { get; set; }
		[Inject] private IToastService toastService { get; set; }
		private RoleMenuListSearch listSearch = new RoleMenuListSearch();
		private List<RoleMenusDto> roleMenusDtos;
		private List<RoleViewDto> roleViewDtos;
		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			roleViewDtos = await roleMenuApiClient.GetListRole();

			listSearch.RoleId = roleViewDtos.Select(x => x.Id).FirstOrDefault();
			await GetListMenu();
		}

		private async Task GetListMenu()
		{ 
			try
			{
				var pagingResponse = await roleMenuApiClient.GetListMenu(listSearch.RoleId, listSearch);
				roleMenusDtos = pagingResponse.Items;
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
			roleMenusDtos.Clear();
			await GetListMenu();
			if (roleMenusDtos.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
		private async Task SelectedPage(int page)
		{
			listSearch.PageNumber = page;
			await GetListMenu();
		}
		
		//GET ROLE ID WHEN SELECTED
	    async void HandleSelectedIndexChanged(ChangeEventArgs e)
		{
			listSearch.RoleId = Guid.Parse(e.Value.ToString());
			
			// Do something with the new selected value
			await GetListMenu();
		}
	}
}
