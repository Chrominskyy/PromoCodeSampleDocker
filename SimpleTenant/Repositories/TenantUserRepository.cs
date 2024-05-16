using Chrominsky.Utils.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using SimpleTenant.Models;

namespace SimpleTenant.Repositories;

public class TenantUserRepository : BaseDatabaseRepository<TenantUser>, ITenantUserRepository
{
    public TenantUserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}