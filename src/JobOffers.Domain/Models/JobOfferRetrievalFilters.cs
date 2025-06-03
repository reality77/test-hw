namespace JobOffers.Domain.Models;

/// <summary>
/// Represents the filters used to retrieve job offers.
/// </summary>
public record JobOfferRetrievalFilters
{
    // TODO : Dans une implémentation correcte, il faudrait renseigner ici les villes, puis utiliser l'API INSEE pour déterminer les codes INSEE associés.
    /// <summary>
    /// Gets or sets the insee locations to filter job offers.
    /// </summary>
    public string[] InseeLocations { get; set; } = [];

    /// <summary>
    /// Gets or sets the type of contract to filter job offers.
    /// </summary>
    public string? ContractType { get; set; }
}