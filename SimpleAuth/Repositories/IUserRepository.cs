using Chrominsky.Utils.Repositories.Base;
using SimpleAuth.Models;

namespace SimpleAuth.Repositories;

/// <summary>
/// Interface for User Repository.
/// It extends IBaseDatabaseRepository<see cref="User"/> and provides additional methods for user-related operations.
/// </summary>
public interface IUserRepository : IBaseDatabaseRepository<User>
{
    /// <summary>
    /// Retrieves a user by their username from the database asynchronously.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A Task that represents the asynchronous operation. The result of the Task is the User object if found, otherwise null.</returns>
    Task<User?> GetUserByUsernameAsync(string username);

    /// <summary>
    /// Deactivates a user in the database asynchronously.
    /// </summary>
    /// <param name="user">The unique identifier of the user to deactivate.</param>
    /// <returns>A Task that represents the asynchronous operation. The result of the Task is a boolean value indicating whether the user was successfully deactivated.</returns>
    Task<bool> DeactivateUserAsync(Guid user);
}