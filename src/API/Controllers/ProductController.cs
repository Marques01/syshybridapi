using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProductController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult> GetByName([FromQuery] string name)
        {
            var products = await _uof.ProductRepository.GetProductByNameAsync(name);            

            return Ok(products);
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
			var products = await _uof.ProductRepository.GetProductsAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product product)
        {
            await _uof.ProductRepository.CreateAsync(product);

            await _uof.CommitAsync();

            return Ok("success");
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            await _uof.ProductRepository.UpdateAsync(product);

            await _uof.CommitAsync();

			return Ok("success");
		}
    }
}
