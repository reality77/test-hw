using JobOffers.Domain.Models;
using JobOffers.Domain.Repositories;
using JobOffers.Infrastructure.Repositories.FranceTravail;

namespace JobOffers.Infrastructure.Repositories;

public class FranceTravailJobOfferRetrieverRepository(FranceTravailApiClient franceTravailApi) : IExternalJobOffersRetrieverRepository
{
    private readonly FranceTravailApiClient _franceTravailApi = franceTravailApi;

    public async Task<IEnumerable<JobOfferModel>> RetrieveJobOffersAsync(JobOfferRetrievalFilters filters)
    {
        var result = await _franceTravailApi.ListOffres(filters.InseeLocations);

        return result.Resultats.Select(offer => new JobOfferModel
        {
            Source = JobOfferSources.FranceTravail,
            Id = offer.Id,
            Title = offer.Intitule,
            Description = offer.Description,
            ApplyUrl = offer.Contact?.UrlPostulation,
            Location = offer.LieuTravail?.Libelle,
            CompanyName = offer.Entreprise?.Nom,
            PostedAt = offer.DateCreation,
        });
    }
}