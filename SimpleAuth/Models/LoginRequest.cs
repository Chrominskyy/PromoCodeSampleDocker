using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Models;

/// <summary>
/// Represents a request for user login.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username for the login request.
    /// </summary>
    [Required]
    public string Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for the login request.
    /// </summary>
    [Required]
    public string Password { get; set; }
}