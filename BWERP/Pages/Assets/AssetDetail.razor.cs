using BWERP.Models.Asset;
using BWERP.Models.AssetHistory;
using BWERP.Models.SeedWork;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;

namespace BWERP.Pages.Assets
{
	public partial class AssetDetail
	{
		[Parameter]
		public string assetid { set; get; }
		private string QRCodeImage;

		private AssetView assetView = new AssetView();
		private List<AssetHistoryView> assetHistoryView = new List<AssetHistoryView>();
		private AssetHistorySearch historySearch = new AssetHistorySearch();

		public MetaData MetaData { get; set; } = new MetaData();

		[CascadingParameter]
		private Error? _error { get; set; }
		protected async override Task OnInitializedAsync()
		{

			await GetData();
			await GetAssetHistory();
		}
		private async Task GetData()
		{
			//GET ASSET DETAIL
			assetView = await assetApiClient.GetAssetById(assetid);

			//GENERATE QR CODE
			var assetUrl = $"{NavigationManager.BaseUri}assets/assetdetail/{assetid}";
			QRCodeImage = QRCodeService.GenerateQRCode(assetUrl, 150, 150);
		}
		private async Task GetAssetHistory()
		{
			try
			{
				var pagingResponse = await assetApiClient.GetAssetHistory(historySearch);
				assetHistoryView = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		//PAGING
		private async Task SelectedPage(int page)
		{
			historySearch.PageNumber = page;
			await GetAssetHistory();
		}
	}
}
