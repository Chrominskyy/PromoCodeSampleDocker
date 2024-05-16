using Chrominsky.Utils.Helpers;
using SimpleAuth.Models;
using SimpleAuth.Repositories;

namespace SimpleAuth.Services;

/// <summary>
/// AuthService class provides authentication, registration, user management functionalities.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly BCryptHelper _bcryptHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="bcryptHelper">The bcrypt helper.</param>
    public AuthService(IUserRepository userRepository, BCryptHelper bcryptHelper)
    {
        _userRepository = userRepository;
        _bcryptHelper = bcryptHelper;
    }

    /// <summary>
    /// Authenticates a user with the provided username and password.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>A string indicating the authentication result. Returns "ValidatedUser" if successful, otherwise null.</returns>
    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return null;
        return _bcryptHelper.VerifyPassword(password, user.Password)? "ValidatedUser" : null;
    }
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <returns>The ID of the newly registered user.</returns>
    public async Task<Guid> RegisterUserAsync(User user)
    {
        user.Id = Guid.NewGuid();
        user.Password = _bcryptHelper.HashPassword(user.Password);
        return await _userRepository.AddAsync(user);
    }

    /// <summary>
    /// Deactivates a user.
    /// </summary>
    /// <param name="userId">The ID of the user to deactivate.</param>
    /// <returns>True if the user is successfully deactivated, otherwise false.</returns>
    public async Task<bool> DeactivateUserAsync(Guid userId)
    {
        var userToUpdate = await _userRepository.GetByIdAsync<User>(userId);
        userToUpdate.IsActive = false;
        await _userRepository.UpdateAsync(userToUpdate);
        return true;
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>True if the user is successfully deleted, otherwise false.</returns>
    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        return await _userRepository.DeleteAsync<User>(userId);
    }

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync<User>(userId);
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <returns>The updated user.</returns>
    public async Task<User> UpdateUserAsync(User user)
    {
        var userToUpdate = await _userRepository.GetByIdAsync<User>(user.Id);
        userToUpdate.Username = user.Username;
        userToUpdate.Password = _bcryptHelper.HashPassword(user.Password);
        userToUpdate.Email = user.Email;
        userToUpdate.UpdatedAt = DateTime.UtcNow;
        userToUpdate.IsDeleted = user.IsDeleted;
        userToUpdate.IsActive = user.IsActive;
        return await _userRepository.UpdateAsync(userToUpdate);
    }

    /// <summary>
    /// Validates a token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>True if the token is valid, otherwise false. In this case, the token is considered valid if it equals "ValidatedUser".</returns>
    public bool ValidateToken(string token)
    {
        return token == "ValidatedUser";
    }
}