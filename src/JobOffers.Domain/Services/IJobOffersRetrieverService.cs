namespace JobOffers.Domain.Services;

public interface IJobOffersRetrieverService
{
    /// <summary>
    /// Retrieves job offers from the specified source.
    /// </summary>
    /// <param name="source">The source from which to retrieve job offers.</param>
    /// <returns>A list of job offers.</returns>
    Task RetrieveJobOffersAsync();
}