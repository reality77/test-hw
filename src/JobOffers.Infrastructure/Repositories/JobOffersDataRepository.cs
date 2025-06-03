using JobOffers.Domain.Models;
using JobOffers.Domain.Repositories;
using JobOffers.Infrastructure.DataContext;
using JobOffers.Infrastructure.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobOffers.Infrastructure.Repositories;

public class JobOffersDataRepository(JobOffersDbContext dbContext) : IJobOffersDataRepository
{
    private readonly JobOffersDbContext _dbContext = dbContext;

    public async Task<bool> AddAsync(JobOfferModel jobOffer)
    {
        // Find if the job offer already exists in the database
        // NOTE : C'est la version la plus simple de la vérification d'existence, mais elle peut être améliorée
        var doesOfferAlreadyExists = await _dbContext.JobOffers
            .AsNoTracking()
            .AnyAsync(o => o.ExternalId == jobOffer.ExternalId && o.Source == jobOffer.Source);

        if (doesOfferAlreadyExists)
        {
            // If it exists, we do not add it again
            return false;
        }

        var entity = new JobOffer
        {
            Id = Guid.NewGuid(),
            Source = jobOffer.Source,
            ExternalId = jobOffer.ExternalId,
            Title = jobOffer.Title,
            Description = jobOffer.Description,
            CompanyName = jobOffer.CompanyName,
            Location = jobOffer.Location,
            PostedAt = jobOffer.PostedAt,
            ContractType = jobOffer.ContractType
        };

        // NOTE : On peut faire plus performant si on traite de grosses quantités de données en une seule fois

        await _dbContext.JobOffers.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}