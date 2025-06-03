
using JobOffers.Domain.Models;

namespace JobOffers.Domain.Repositories;

public interface IExternalJobOffersRetrieverRepository
{
    /// <summary>
    /// Retrieves job offers based on the specified filters.
    /// This method is designed to be implemented by various external job offer providers.
    /// </summary>
    /// <param name="filters">Filters to apply when retrieving job offers.</param>
    /// <returns>A collection of job offers (asynchronous).</returns>
    Task<IEnumerable<JobOfferModel>> RetrieveJobOffersAsync(JobOfferRetrievalFilters filters);
}