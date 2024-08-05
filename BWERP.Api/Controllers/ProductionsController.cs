using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Production;
using BWERP.Models.WeelkyReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductionsController : ControllerBase
	{
		private readonly IProductionRepository _productionRepos;
		public ProductionsController(IProductionRepository productionRepos)
		{
			_productionRepos = productionRepos;
		}
		//Get Production Status on Dashboard
		[HttpGet("dashboardprod")]
		public async Task<ActionResult<List<ProductionViewDto>>> GetOutstandingReceivable(string dept, DateTime scandate)
		{
			var data = await _productionRepos.GetProductionStatus_Dashboard(dept, scandate);
			return Ok(data);
		}
	}
}
