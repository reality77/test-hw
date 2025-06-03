namespace JobOffers.Infrastructure.Repositories.FranceTravail.Models;

public record AuthRequest
{
    /// <summary>
    /// Gets or sets the client identifier.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret.
    /// </summary>
    public required string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the scope for the access token.
    /// </summary>
    public required string Scope { get; set; }

    /// <summary>
    /// Gets or sets the grant type for the access token request.
    /// </summary>
    public required string GrantType { get; set; }
}