using SimpleTenant.Models;
using SimpleTenant.Repositories;

namespace SimpleTenant.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;
    
    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }
    
    public async Task<Guid> CreateTenant(Tenant tenant)
    {
        tenant.TenantId = Guid.NewGuid();
        return await _tenantRepository.AddAsync<Tenant>(tenant);
    }

    public async Task<Tenant?> GetTenantById(Guid tenantId)
    {
        return await _tenantRepository.GetByIdAsync<Tenant>(tenantId);
    }

    public async Task<IEnumerable<Tenant>> GetAllTenants()
    {
        throw new NotImplementedException();
    }

    public async Task<Tenant> UpdateTenant(Tenant tenant)
    {
        return await _tenantRepository.UpdateAsync<Tenant>(tenant);
    }

    public async Task<bool> DeleteTenant(Guid tenantId)
    {
        return await _tenantRepository.DeleteAsync<Tenant>(tenantId);
    }
}