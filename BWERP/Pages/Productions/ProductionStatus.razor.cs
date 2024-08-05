using Blazored.Toast.Services;
using BWERP.Models.Production;
using BWERP.Repositories.Interfaces;
using BWERP.Shared;
using Microsoft.AspNetCore.Components;

namespace BWERP.Pages.Productions
{
	public partial class ProductionStatus
	{
		//VARIABLES
		private string osDept, ipDept;
		private double sumQtyOS, sumQtyIP;
		private string currentDate;
		private Timer timer;
		[Inject] private IProductionApiClient productionApiClient { get; set; }
		[Inject] IToastService toastService { set; get; }

		private List<ProductionViewDto> _ipViewDtos;
		private List<ProductionViewDto> _osViewDtos;

		[CascadingParameter]
		private Error? _error { get; set; }
		protected override async Task OnInitializedAsync()
		{
			osDept = "510";
			ipDept = "590";
			currentDate = DateTime.Now.Date.ToString("dd-MM-yyyy");

			await GetDataAsync();

			// Set up the timer to call GetDataAsync every minute (60000 ms)
			timer = new Timer(async (e) =>
			{
				await GetDataAsync();
				await InvokeAsync(StateHasChanged); // Refresh the UI
			}, null, 0, 60000);
		}
		private async Task GetDataAsync()
		{
			try
			{
				_ipViewDtos = await productionApiClient.GetProductionStatus_Dashboard(ipDept, DateTime.Now.Date);
				sumQtyIP = _ipViewDtos.Sum(s => s.ProdQty);

				_osViewDtos = await productionApiClient.GetProductionStatus_Dashboard(osDept, DateTime.Now.Date);
				sumQtyOS = _osViewDtos.Sum(s => s.ProdQty);
			}
			catch (Exception ex)
			{
				_error.ProcessError(ex);
			}
		}
		public void Dispose()
		{
			// Dispose of the timer when the component is disposed
			timer?.Dispose();
		}
	}
}
