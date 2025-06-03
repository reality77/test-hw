namespace JobOffers.Domain.Services;

/// <summary>
/// Interface for a service that retrieves job offers from various sources.
/// </summary>
public interface IJobOffersRetrieverService
{
    /// <summary>
    /// Retrieves job offers from the specified source and store them in the data store.
    /// </summary>
    /// <returns>A list of job offers.</returns>
    Task RetrieveJobOffersAsync();
}