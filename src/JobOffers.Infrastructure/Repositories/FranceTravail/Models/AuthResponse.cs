namespace JobOffers.Infrastructure.Repositories.FranceTravail.Models;


public record AuthResponse
{
    /// <summary>
    /// Gets or sets the scope of the access token.
    /// </summary>
    public required string Scope { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token in seconds.
    /// </summary>
    public required int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the type of the access token.
    /// </summary>
    public required string TokenType { get; set; }

    /// <summary>
    /// Gets or sets the access token value.
    /// </summary>
    public required string AccessToken { get; set; }
}
