using JobOffers.Domain.Models;
using JobOffers.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JobOffers.Domain.Services.Impl;

public class JobOffersRetrieverService(IServiceProvider serviceProvider,
    IJobOffersDataRepository jobOffersDataRepository,
    ILogger<JobOffersRetrieverService> logger) : IJobOffersRetrieverService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IJobOffersDataRepository _jobOffersDataRepository = jobOffersDataRepository;
    private readonly ILogger<JobOffersRetrieverService> _logger = logger;

    public async Task RetrieveJobOffersAsync()
    {
        // NOTE : En utilisant directement le code INSEE de Paris (75056), l'API retourne une erreur 
        JobOfferRetrievalFilters filters = new()
        {
            InseeLocations = ["75101", "35238", "33063"], // INSEE codes for Paris (1er arr), Rennes, and Bordeaux
        };

        // NOTE : dans notre exemple, on n'utilise qu'un seul fournisseur d'offres d'emploi
        JobOfferSources source = JobOfferSources.FranceTravail;

        var service = _serviceProvider.GetRequiredKeyedService<IExternalJobOffersRetrieverRepository>(source);

        // Retrieve job offers from the specified source
        _logger.LogInformation("Retrieving job offers from {source}", source);
        var offres = await service.RetrieveJobOffersAsync(filters);

        // Store the retrieved job offers in the database
        _logger.LogInformation("Adding {count} job offers in data store", offres.Count());
        await StoreOffresAsync(offres);

        // Output the statistics of the retrieval
        await OutputOffresStatsAsync(offres);
    }

    private async Task StoreOffresAsync(IEnumerable<JobOfferModel> offres)
    {
        Dictionary<string, Task<bool>> addTasks = [];

        foreach (var offre in offres)
        {
            addTasks.Add(offre.ExternalId, _jobOffersDataRepository.AddAsync(offre));
        }

        await Task.WhenAll(addTasks.Values);

        _logger.LogInformation("Added {added} new job offers ({existing} already in data store)", addTasks.Count(t => t.Value.Result == true), addTasks.Count(t => t.Value.Result == false));
    }

    private async Task OutputOffresStatsAsync(IEnumerable<JobOfferModel> offres)
    {
        using var file = File.CreateText("job_offers_stats.log");

        await file.WriteLineAsync($"{offres.Count()} offres récupérées :");

        foreach (var offre in offres)
        {
            // NOTE : J'ai choisi de ne pas afficher le pays (champ qui n'est pas retourné par l'API France Travail)
            await file.WriteLineAsync($"- {offre.ExternalId} : {offre.ContractType} chez {offre.CompanyName}, situé à {offre.Location}");
        }
        
        await file.FlushAsync();
    }
}