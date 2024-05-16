using Microsoft.AspNetCore.Mvc;
using SimpleTenant.Models;
using SimpleTenant.Services;

namespace SimpleTenant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        
        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> Get()
        {
            return Ok(await _tenantService.GetAllTenants());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Tenant>> Get(Guid id)
        {
            var tenant = await _tenantService.GetTenantById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<Tenant>> Post(Tenant tenant)
        {
            var tenantId = await _tenantService.CreateTenant(tenant);
            return CreatedAtAction(nameof(Get), new { id = tenantId }, tenant);
        }

        [HttpPut]
        public async Task<ActionResult<Tenant>> Put(Tenant tenant)
        {
            var ret = await _tenantService.UpdateTenant(tenant);

            return Ok(ret);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var ret = await _tenantService.DeleteTenant(id);
            if (!ret)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}