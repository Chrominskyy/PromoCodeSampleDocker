using Chrominsky.Utils.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Contexts;
using SimpleAuth.Models;

namespace SimpleAuth.Repositories;

/// <summary>
/// Represents a repository for managing user data.
/// </summary>
public class UserRepository : BaseDatabaseRepository<User>, IUserRepository
{
    private readonly AuthDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The database context for user data.</param>
    public UserRepository(AuthDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a user by their username, if the user is active.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>The user if found and active, otherwise null.</returns>
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        return user;
    }

    /// <summary>
    /// Deactivates a user by their unique identifier.
    /// </summary>
    /// <param name="user">The unique identifier of the user to deactivate.</param>
    /// <returns>True if the user was found and deactivated, otherwise false.</returns>
    public async Task<bool> DeactivateUserAsync(Guid user)
    {
        var entity = await _context.Users.FindAsync(user);
        if(entity == null)
            return false;
        entity.IsActive = false;
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}