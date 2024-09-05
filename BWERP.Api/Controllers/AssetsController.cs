using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Asset;
using BWERP.Models.Exppense;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssetsController : ControllerBase
	{
		private readonly IAssetRepository _assetRepository;
        public AssetsController(IAssetRepository assetRepository)
        {
			_assetRepository = assetRepository;
		}

		//CREATE DATA
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AssetCreateDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _assetRepository.Create(new Asset()
			{
				Id = request.Id,
				//Description = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
				Name = request.Name,
				CategoryId = request.CategoryId,
				Location = request.Location,
				StatusId = request.StatusId,
				Description = request.Description,
				PurchaseDate = request.PurchaseDate,
				PurChasePrice = request.PurChasePrice,
				AssignedTo = request.AssignedTo
			});
			return Ok();
		}
	}
}
