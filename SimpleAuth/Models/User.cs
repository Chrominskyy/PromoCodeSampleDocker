using Chrominsky.Utils.Models.Base;

namespace SimpleAuth.Models;

/// <summary>
/// Represents a user in the application.
/// Inherits from BaseDatabaseEntity, which provides common properties for database entities.
/// </summary>
public class User : BaseDatabaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// Note: It's recommended to store hashed passwords for security reasons.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user is active or not.
    /// </summary>
    public bool IsActive { get; set; }
}