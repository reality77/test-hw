
using JobOffers.Domain.Models;

namespace JobOffers.Domain.Repositories;

public interface IJobOffersDataRepository
{
    /// <summary>
    /// Adds a new job offer to the repository.
    /// </summary>
    /// <param name="jobOffer">The job offer to add.</param>
    /// <returns>TRUE if the job offer was added successfully, FALSE is it already exists in the data store.</returns>
    Task<bool> AddAsync(JobOfferModel jobOffer);
}