using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using BWERP.Models.SeedWork;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Assets
{
	public partial class AssetList
	{
		private AssetSearch assetSearch = new AssetSearch();
		private List<AssetView> assetView = new List<AssetView>();
		private List<AssetCategoryView> assetCategories = new List<AssetCategoryView>();

		public MetaData MetaData { get; set; } = new MetaData();
		[CascadingParameter]
		private Error? _error { get; set; }

		protected override async Task OnInitializedAsync()
		{
			assetCategories = await assetApiClient.GetCategory();
			await GetListAsset();
		}
		//GET DATA
		private async Task GetListAsset()
		{
			try
			{
				var pagingResponse = await assetApiClient.GetListAsset(assetSearch);
				assetView = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
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
		//PAGING
		private async Task SelectedPage(int page)
		{
			assetSearch.PageNumber = page;
			await GetListAsset();
		}
	}
}
