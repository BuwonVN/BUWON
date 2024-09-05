using BWERP.Models.Asset;
using BWERP.Models.AssetCategory;
using Microsoft.AspNetCore.Components.Forms;

namespace BWERP.Pages.Assets
{
	public partial class AssetCreate
	{
		private AssetCreateDto assetCreateDto = new AssetCreateDto();
		private List<AssetCategoryView> categoryViews = new List<AssetCategoryView>();
		protected override async Task OnInitializedAsync()
		{
			
		}
		private async Task SubmitTask(EditContext context)
		{
			
		}
	}
}
