using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PurchaseController : ControllerBase
	{
		private readonly IUnitOfWork _uof;

        public PurchaseController(IUnitOfWork uof)
        {
			_uof = uof;
        }

        [HttpPost]
		public async Task<ActionResult> Create([FromBody] Purchase purchase)
		{
			await _uof.PurchaseRepository.CreateAsync(purchase);

			await _uof.CommitAsync();

			return Ok("success");
		}

		[HttpPost]
		[Route("add/product")]
		public async Task<ActionResult> AddProducts([FromBody] PurchaseProduct purchaseProduct)
		{
			await _uof.PurchaseProductRepository.AddProductAsync(purchaseProduct);

			await _uof.CommitAsync();

			return Ok("success");
		}
	}
}
