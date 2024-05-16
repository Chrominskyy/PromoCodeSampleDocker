using Chrominsky.Utils.Repositories.Base;
using SimpleTenant.Contexts;
using SimpleTenant.Models;

namespace SimpleTenant.Repositories;

public class TenantRepository : BaseDatabaseRepository<Tenant>, ITenantRepository
{
    public TenantRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }
}