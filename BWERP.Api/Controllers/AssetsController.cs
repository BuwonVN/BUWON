using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Asset;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
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

		[HttpGet("category")]
		public async Task<IActionResult> GetAssetCategory()
		{
			var result = await _assetRepository.GetAssetCategory();
			return Ok(result);
		}
		[HttpGet("status")]
		public async Task<IActionResult> GetAssetStatus()
		{
			var result = await _assetRepository.GetAssetStatus();
			return Ok(result);
		}
		[HttpGet("all")]
		public async Task<IActionResult> GetAssetAll()
		{
			var result = await _assetRepository.GetAssetAll();
			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetListExpense([FromQuery] AssetSearch assetSearch)
		{
			var pagedlist = await _assetRepository.GetListAsset(assetSearch);
			var assetDtos = pagedlist.Items.ToList();

			return Ok(new PagedList<AssetView>(assetDtos,
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
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
				AssignedTo = request.AssignedTo,
				CreatedDate = request.CreatedDate,
				CreatedUser = request.CreatedUser
			});
			return Ok();
		}

		//UPDATE DATA
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(string id, [FromBody] AssetUpdateDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var fromDb = await _assetRepository.GetAssetById(id);

			if (fromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			fromDb.CategoryId = request.CategoryId;
			fromDb.Description = request.Description;
			fromDb.Name = request.Name;
			fromDb.Location = request.Location;
			fromDb.StatusId = request.StatusId;
			fromDb.PurChasePrice = request.PurChasePrice;
			fromDb.PurchaseDate = request.PurChaseDate;
			fromDb.AssignedTo = request.AssignedTo;

			var result = await _assetRepository.Update(fromDb);

			return Ok();
		}
	}
}
