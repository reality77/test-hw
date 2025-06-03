using JobOffers.Domain.Models;
using JobOffers.Domain.Repositories;
using JobOffers.Infrastructure.DataContext;
using JobOffers.Infrastructure.Repositories;
using JobOffers.Infrastructure.Repositories.FranceTravail;

namespace JobOffers.Api.DependencyInjection;

public static class AppBuilders
{
    /// <summary>
    /// Adds the data layer services to the service collection.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Updated service collection</returns>
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JobOffersDbContext>(options =>
        {
            JobOffersDbContext.SetDbContextOptionsBuilder(options, configuration);
        });

        services.AddScoped<IJobOffersDataRepository, JobOffersDataRepository>();

        return services;
    }

    /// <summary>
    /// Adds the France Travail repository to the service collection.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Updated service collection</returns>
    public static IServiceCollection AddFranceTravailRepository(this IServiceCollection services, IConfiguration configuration)
    {
        // NOTE : Le système des Keyed Services nous permettra de récupérer des données à partir de différents moteurs de recherche d'offres d'emploi
        services.AddKeyedScoped<IExternalJobOffersRetrieverRepository, FranceTravailJobOfferRetrieverRepository>(JobOfferSources.FranceTravail);

        services.AddHttpClient<FranceTravailApiClient>(client =>
        {
            var baseUri = configuration.GetValue<string>("JobOffersApi:FranceTravail:BaseUri")
                ?? throw new ArgumentNullException(nameof(configuration), "JobOffersApi:FranceTravail:BaseUri configuration is missing.");

            client.BaseAddress = new Uri(baseUri);
        });

        services.AddHttpClient<FranceTravailOAuthApiClient>(client =>
        {
            var baseUri = configuration.GetValue<string>("JobOffersApi:FranceTravail:AuthUri")
                ?? throw new ArgumentNullException(nameof(configuration), "JobOffersApi:FranceTravail:AuthUri configuration is missing.");

            client.BaseAddress = new Uri(baseUri);
        });

        return services;
    }
}