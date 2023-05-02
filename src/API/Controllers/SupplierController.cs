using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SupplierController : ControllerBase
	{
		private readonly IUnitOfWork _uof;

        public SupplierController(IUnitOfWork uof)
        {
            _uof = uof;
        }

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] Supplier supplier)
		{
			await _uof.SupplierRepository.CreateAsync(supplier);

			await _uof.CommitAsync();

			return Ok("success");
		}
    }
}
