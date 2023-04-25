using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public RolesController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Roles roles)
        {
            await _uof.RoleRepository.CreateAsync(roles);

            await _uof.CommitAsync();

            return Ok();
        }

        [HttpPost]
        [Route("UserRole")]
        public async Task<ActionResult> AssociateUserRole([FromBody] UserRoles userRoles)
        {
            await _uof.UserRoleRepository.CreateAsync(userRoles);

            await _uof.CommitAsync();

            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> RemoveAssociation(int id)
        {
            await _uof.UserRoleRepository.DeleteAsync(id);

            await _uof.CommitAsync();

            return Ok();
        }
    }
}
