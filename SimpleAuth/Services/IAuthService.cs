using SimpleAuth.Models;

namespace SimpleAuth.Services;

/// <summary>
/// Interface for authentication and user management services.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with the given username and password.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains the authentication token if successful; otherwise, null.</returns>
    Task<string?> AuthenticateAsync(string username, string password);

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user object to register.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains the unique identifier of the newly registered user.</returns>
    Task<Guid> RegisterUserAsync(User user);

    /// <summary>
    /// Deactivates a user with the given user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains true if the user is successfully deactivated; otherwise, false.</returns>
    Task<bool> DeactivateUserAsync(Guid userId);

    /// <summary>
    /// Deletes a user with the given user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains true if the user is successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteUserAsync(Guid userId);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains the user object if found; otherwise, null.</returns>
    Task<User?> GetUserByIdAsync(Guid userId);

    /// <summary>
    /// Updates a user with the given user object.
    /// </summary>
    /// <param name="user">The updated user object.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains the updated user object.</returns>
    Task<User> UpdateUserAsync(User user);
    
    /// <summary>
    /// Validates the authentication token.
    /// </summary>
    /// <param name="token">The authentication token to validate.</param>
    /// <returns>True if the token is valid; otherwise, false.</returns>
    bool ValidateToken(string token);
}

