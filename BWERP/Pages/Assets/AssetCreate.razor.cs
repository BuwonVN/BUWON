using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
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
		private AssetView assetView = new AssetView();
		//VARIALES
		private string username, newAssetId;
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
			assetCreateDto.Id = newAssetId;
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
			var latestid = await assetApiClient.GetLatestId();
			// Current date in ddMMyy format
			string currentDate = DateTime.Now.ToString("yyMMdd");

			if (latestid != null)
			{
				// Extract the date and sequence from the last AssetId
				string lastDatePart = latestid.Id.Substring(2, 6); // "240926" for PC240926
				string lastSequencePart = latestid.Id.Substring(8); // "01" for PC24092601

				if (lastDatePart == currentDate)
				{
					// If the date is the same, increment the sequence
					int newSequence = int.Parse(lastSequencePart) + 1;
					newAssetId = GetCategoryCode() + $"{currentDate}{newSequence:D2}"; // Ensure 2 digits for sequence (e.g., 01, 02)
				}
				else
				{
					// If it's a new date, start with sequence 01
					newAssetId = GetCategoryCode() + $"{currentDate}01";
				}
			}
			else
			{
				// No previous AssetId found, start with PCddMMyy01 for the current date
				newAssetId = GetCategoryCode() + $"{currentDate}01";
			}
		}
	}
}
