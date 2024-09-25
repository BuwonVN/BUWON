using BWERP.Models.AssetHistory;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BWERP.Pages.Assets
{
	public partial class AssetHistoryCreate
	{
		[Parameter]
		public string assetid { set; get; }

		private AssetHistoryCreateDto assetHistoryCreateDto = new AssetHistoryCreateDto();

		protected override async Task OnInitializedAsync()
		{

		}

		private async Task SubmitTask(EditContext context)
		{
			assetHistoryCreateDto.AssetId = assetid;
			assetHistoryCreateDto.Date = DateTime.Now;
			var result = await assetApiClient.CreateAssetHistory(assetHistoryCreateDto);
			if (result)
			{
				toastService.ShowSuccess($"Upload successfully.");
				await GoBack();
			}
			else
			{
				toastService.ShowError($"An error occurred in progress. Please contact to administrator.");

			}
		}
		private async Task GoBack()
		{
			await JS.InvokeVoidAsync("goBack");
		}
	}
}
