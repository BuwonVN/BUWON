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
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity is not null && user.Identity.IsAuthenticated)
			{
				try
				{
					leftsidebar = await menuApiClient.GetMenuByUser(user.Identity.Name);

					// Avoid unnecessary redirect
					if (NavigationManager.Uri != NavigationManager.BaseUri)
					{
						NavigationManager.NavigateTo("/");
					}
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"Error fetching menu: {ex.Message}");
				}
			}
			else
			{
				NavigationManager.NavigateTo("/users/login");
			}
		}
	}
}
