using BWERP.Api.Repositories.Interfaces;
using BWERP.Api.Repositories.Services;
using BWERP.Models.Asset;
using BWERP.Models.AssetHistory;
using BWERP.Models.Exppense;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
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
		//ASSET CATEGORY
		[HttpGet("category")]
		public async Task<IActionResult> GetAssetCategory()
		{
			var result = await _assetRepository.GetAssetCategory();
			return Ok(result);
		}
		//ASSET STATUS
		[HttpGet("status")]
		public async Task<IActionResult> GetAssetStatus()
		{
			var result = await _assetRepository.GetAssetStatus();
			return Ok(result);
		}
		//GET ALL ASSET
		[HttpGet("getid")]
		public async Task<IActionResult> GetLatestId()
		{
			var result = await _assetRepository.GetLatestId();
			return Ok(result);
		}
		//GET PAGING LIST ASSET
		[HttpGet]
		public async Task<IActionResult> GetListAsset([FromQuery] AssetSearch assetSearch)
		{
			var pagedlist = await _assetRepository.GetListAsset(assetSearch);
			var assetDtos = pagedlist.Items.ToList();

			return Ok(new PagedList<AssetView>(assetDtos,
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
		}
		//GET ASSET BY ID
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetAssetById(string id)
		{
			var result = await _assetRepository.GetAssetById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}
			return Ok(new AssetView()
			{
				Id = result.Id,
				Name = result.Name,
				SerialNo = result.SerialNo,
				CategoryId = result.CategoryId,
				Location = result.Location,
				StatusId = result.StatusId,
				Description = result.Description,
				PurchaseDate = result.PurchaseDate,
				PurchasePrice = result.PurchasePrice,
				AssignedTo = result.AssignedTo
			});
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
				SerialNo = request.SerialNo,
				CategoryId = request.CategoryId,
				Location = request.Location,
				StatusId = request.StatusId,
				Description = request.Description,
				PurchaseDate = request.PurchaseDate,
				PurchasePrice = request.PurChasePrice,
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

			fromDb.Description = request.Description;
			fromDb.Name = request.Name;
			fromDb.SerialNo = request.SerialNo;
			fromDb.Location = request.Location;
			fromDb.StatusId = request.StatusId;
			fromDb.PurchasePrice = request.PurchasePrice;
			fromDb.PurchaseDate = request.PurchaseDate;
			fromDb.AssignedTo = request.AssignedTo;

			var result = await _assetRepository.Update(fromDb);

			return Ok();
		}
		//GET ASSET HISTORY
		[HttpGet("history")]
		public async Task<IActionResult> GetAssetHistory(int pageNumber = 1, int pageSize = 10)
		{
			var result = await _assetRepository.GetAssetHistory(pageNumber, pageSize);
			return Ok(result);
		}
		//CREATE ASSET HISTORY
		[HttpPost("history")]
		public async Task<IActionResult> CreateHistory([FromBody] AssetHistoryCreateDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _assetRepository.CreateHistory(new AssetHistory()
			{
				//Id = request.Id,
				//Description = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
				AssetId = request.AssetId,
				Description = request.Description,
				Date = request.Date,
				Event = request.Event
			});
			return Ok();
		}
	}
}
