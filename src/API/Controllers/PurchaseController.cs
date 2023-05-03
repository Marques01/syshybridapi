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

        [HttpGet]
        [Route("purchases/products")]
        public async Task<ActionResult> PurchaseProducts([FromQuery] int id)
        {
            var purchaseProducts = await _uof.PurchaseProductRepository.GetByPurchaseIdAsync(id);

            return Ok(purchaseProducts);
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

		[HttpDelete]
		[Route("remove/product")]
		public async Task<ActionResult> Delete([FromQuery] int id)
		{
			await _uof.PurchaseProductRepository.DeleteProductAsync(id);

			await _uof.CommitAsync();

			return Ok("success");
		}
	}
}
