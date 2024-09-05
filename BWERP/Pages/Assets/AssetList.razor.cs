using Blazored.Toast.Services;
using BWERP.Models.Asset;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Assets
{
	public partial class AssetList
	{
		private AssetSearch assetSearch = new AssetSearch();
		private List<AssetView> assetView = new List<AssetView>();

		private async Task GetListAsset()
		{

		}
		//SEARCH DATA
		private async Task SearchForm(EditContext editContext)
		{
			assetView.Clear();
			await GetListAsset();
			if (assetView.Count <= 0)
			{
				toastService.ShowInfo($"No data found.");
			}
		}
	}
}
