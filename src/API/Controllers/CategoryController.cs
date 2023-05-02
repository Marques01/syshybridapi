using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoryController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _uof.CategoryRepository.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Category category)
        {
            await _uof.CategoryRepository.CreateAsync(category);

            await _uof.CommitAsync();

            return Ok("success");
        }
    }
}
