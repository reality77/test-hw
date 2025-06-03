using JobOffers.Domain.Services;

namespace JobOffers.Api.BackgroundServices;

public class JobOffersRetrievalBatchService(IServiceProvider serviceProvider, ILogger<JobOffersRetrievalBatchService> logger) : BackgroundService
{
    private readonly ILogger _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private const int JobRetrievalIntervalInSeconds = 24 * 3600; // 1 day


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Started job retrieval background service");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            _logger.LogInformation("--> Retrieving job offers");

            var jobRetrievalService = scope.ServiceProvider.GetRequiredService<IJobOffersRetrieverService>();

            try
            {
                // TODO : Idéalement il faudrait passer le CancellationToken dans toutes les méthodes asynchrones
                await jobRetrievalService.RetrieveJobOffersAsync();

                _logger.LogInformation("-- Job offers retrieval completed successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Technical error while processing message");
            }

            await Task.Delay(TimeSpan.FromSeconds(JobRetrievalIntervalInSeconds), stoppingToken);
        }

        _logger.LogInformation("Stopped job retrieval background service");
    }
}
