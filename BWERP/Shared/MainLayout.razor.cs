using BWERP.Models.Menu;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System;

namespace BWERP.Shared
{
	public partial class MainLayout
	{
		[Inject] private IMenuApiClient menuApiClient { get; set; }

		private List<MenuViewRequest> leftsidebar;

		//Use JSRuntime to call the initializeSidePanel function after the component has rendered.
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await JS.InvokeVoidAsync("initializeSidePanel");
			}
		}

		protected override async Task OnInitializedAsync()
		{
			//AUTHORIZE
			var authState = await AuthenticationStateProvider
				.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity is not null && user.Identity.IsAuthenticated)
			{
				NavigationManager.NavigateTo("/");
				leftsidebar = await menuApiClient.GetMenuByUser(user.Identity.Name);
				//foreach (var icon in leftsidebar)
				//{
				//    var newIcon = icon.Icon.Trim('"');
				//}
			}
			else
			{
				NavigationManager.NavigateTo("/users/login");
			}
		}
	}
}
