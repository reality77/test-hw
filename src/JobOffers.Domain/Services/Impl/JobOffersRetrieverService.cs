using JobOffers.Domain.Models;
using JobOffers.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JobOffers.Domain.Services.Impl;

public class JobOffersRetrieverService(IServiceProvider serviceProvider, ILogger<JobOffersRetrieverService> logger) : IJobOffersRetrieverService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<JobOffersRetrieverService> _logger = logger;

    /// <summary>
    /// Retrieves job offers from the specified source.
    /// </summary>
    /// <param name="source">The source from which to retrieve job offers.</param>
    /// <returns>A list of job offers.</returns>
    public async Task RetrieveJobOffersAsync()
    {
        // NOTE : En utilisant directement le code INSEE de Paris (75056), l'API retourne une erreur 
        JobOfferRetrievalFilters filters = new()
        {
            InseeLocations = [ "75101", "35238", "33063"], // INSEE codes for Paris (1er arr), Rennes, and Bordeaux
        };

        // NOTE : dans notre exemple, on n'utilise qu'un seul fournisseur d'offres d'emploi
        JobOfferSources source = JobOfferSources.FranceTravail;

        var service = _serviceProvider.GetRequiredKeyedService<IExternalJobOffersRetrieverRepository>(source);

        var offres = await service.RetrieveJobOffersAsync(filters);

        // TODO : Traiter les offres récupérées (par exemple, les enregistrer dans une base de données)

        foreach (var offre in offres)
        {
            Console.WriteLine(offre);
        }
    }
}