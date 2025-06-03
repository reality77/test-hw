using System.Net.Http.Json;
using System.Text.Json;
using JobOffers.Infrastructure.Repositories.FranceTravail.Models;
using JobOffers.Infrastructure.Utils;
using Microsoft.Extensions.Logging;

namespace JobOffers.Infrastructure.Repositories.FranceTravail;

public class FranceTravailApiClient(HttpClient httpClient,
    FranceTravailOAuthApiClient franceTravailOAuthApi,
    ILogger<FranceTravailApiClient> logger)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<FranceTravailApiClient> _logger = logger;
    private readonly FranceTravailOAuthApiClient _franceTravailOAuthApi = franceTravailOAuthApi;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Retrieves job offers from the France Travail API.
    /// </summary>
    /// <returns>A list of job offers.</returns>
    public async Task<ResultatOffres> ListOffres(string[] inseeCommuneIds)
    {
        // TODO : Dans l'idéal, ajouter une trace de télémétrie (span) pour notamment mesurer le temps de réponse de l'API.

        // TODO : Gérer la pagination si nécessaire

        var accessToken = await _franceTravailOAuthApi.GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync("partenaire/offresdemploi/v2/offres/search?commune=" + string.Join(",", inseeCommuneIds));

        if (!response.IsSuccessStatusCode)
        {
            // TODO : Gérer les cas d'erreur de l'API
            _logger.LogError("Failed to retrieve job offers from France Travail API. Status code: {StatusCode}, Reason: {ReasonPhrase}",
                response.StatusCode, await response.Content.ReadAsStringAsync());
            throw new HttpRequestException($"Failed to retrieve job offers: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<ResultatOffres>(_jsonSerializerOptions)
            ?? throw new InvalidOperationException("Failed to deserialize response from France Travail API.");
    }
}