using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.Exppense;
using BWERP.Repositories.Interfaces;
using BWERP.Repositories.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Assets
{
	public partial class AssetCreate
	{
		private AssetCreateDto assetCreateDto = new AssetCreateDto();
		private List<AssetCategoryView> assetCategories = new List<AssetCategoryView>();
		private List<AssetStatus> assetStatuses = new List<AssetStatus>();
		private List<AssetView> assetViews = new List<AssetView>();
		//VARIALES
		private string username, newid;
		protected override async Task OnInitializedAsync()
		{
			assetCategories = await assetApiClient.GetCategory();
			assetStatuses = await assetApiClient.GetAssetStatus();
			//AUTHORIZE
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			username = authState.User.Identity.Name;

			assetCreateDto.CreatedUser = username;
			assetCreateDto.PurchaseDate = DateTime.Now;
		}
		private async Task SubmitTask(EditContext context)
		{
			await GetNewId();

			//VALIDATE DEPARTMENT
			if (assetCreateDto.CategoryId == 0)
			{
				toastService.ShowError($"Please select Category.");
				return;
			}
			if (assetCreateDto.StatusId == 0)
			{
				toastService.ShowError($"Please select Asset Status.");
				return;
			}
			assetCreateDto.Id = newid;
			assetCreateDto.CreatedDate = DateTime.Now;
			assetCreateDto.CreatedUser = username;
			var result = await assetApiClient.CreateAsset(assetCreateDto);
			if (result)
			{
				toastService.ShowSuccess($"Upload successfully.");
				NavigationManager.NavigateTo("/assets/assetlist");
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");

			}
		}
		private string GetCategoryCode() 
		{ 
			var categoryCode = assetCategories.FirstOrDefault(c => c.Id == assetCreateDto.CategoryId);
			// Return the code if the category is found, otherwise return an empty string
			return categoryCode?.Code ?? string.Empty;
		}
		private async Task GetNewId()
		{
			assetViews = await assetApiClient.GetAssetAll();
			var getid = assetViews.OrderByDescending(x => x.Id).Select(c => c.Id).FirstOrDefault();
			int numericPart = int.Parse(getid?.Substring(8) ?? "0");
			// Construct the new ID
			newid = GetCategoryCode() + DateTime.Now.ToString("yyMMdd") + (numericPart + 1).ToString("00");
		}
	}
}
