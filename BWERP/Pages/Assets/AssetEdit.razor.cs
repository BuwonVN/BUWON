using BWERP.Models.ExpenseCategory;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Repositories.Services;

namespace BWERP.Pages.Assets
{
	public partial class AssetEdit
	{
		[Parameter]
		public string assetid { set; get; }
		private AssetUpdateDto assetUpdate = new AssetUpdateDto();
		private List<AssetCategoryView> assetCategories = new List<AssetCategoryView>();
		private List<AssetStatus> assetStatuses = new List<AssetStatus>();
		protected override async Task OnInitializedAsync()
		{
			assetCategories = await assetApiClient.GetCategory();
			assetStatuses = await assetApiClient.GetAssetStatus();

			var fromDb = await assetApiClient.GetAssetById(assetid);
			assetUpdate.Id = assetid;
			assetUpdate.Name = fromDb.Name;
			assetUpdate.SerialNo = fromDb.SerialNo;
			assetUpdate.Location = fromDb.Location;
			assetUpdate.StatusId = fromDb.StatusId;
			assetUpdate.Description = fromDb.Description;
			assetUpdate.PurchasePrice = fromDb.PurchasePrice;
			assetUpdate.PurchaseDate = fromDb.PurchaseDate;
			assetUpdate.AssignedTo = fromDb.AssignedTo;
		}
		private async Task SubmitTask(EditContext context)
		{
			var result = await assetApiClient.UpdateAsset(assetid, assetUpdate);
			if (result)
			{
				toastService.ShowSuccess($"Asset has been Updated successfully.");
				NavigationManager.NavigateTo("/assets/assetlist");
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");

			}
		}
	}
}
