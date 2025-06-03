using System.Net.Http.Json;
using System.Text.Json;
using JobOffers.Infrastructure.Repositories.FranceTravail.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JobOffers.Infrastructure.Repositories.FranceTravail;

public class FranceTravailOAuthApiClient(HttpClient httpClient,
    IConfiguration configuration,
    ILogger<FranceTravailOAuthApiClient> logger)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<FranceTravailOAuthApiClient> _logger = logger;

    private readonly string _clientId = configuration.GetValue<string>("JobOffersApi:FranceTravail:ClientId")
        ?? throw new ArgumentNullException("configuration", "JobOffersApi:FranceTravail:ClientId configuration is missing.");

    private readonly string _clientSecret = configuration.GetValue<string>("JobOffersApi:FranceTravail:ClientSecret")
        ?? throw new ArgumentNullException("configuration", "JobOffersApi:FranceTravail:ClientSecret configuration is missing.");

    private readonly string _scope = configuration.GetValue<string>("JobOffersApi:FranceTravail:Scope")
        ?? throw new ArgumentNullException("configuration", "JobOffersApi:FranceTravail:Scope configuration is missing.");

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    /// <summary>
    /// Retrieves job offers from the France Travail API.
    /// </summary>
    /// <returns>A list of job offers.</returns>
    public async Task<string> GetAccessToken()
    {
        // TODO : Gérer un cache du token jusqu'à un délai proche de l'expiration.

        // TODO : Dans l'idéal, ajouter une trace de télémétrie (span) pour notamment mesurer le temps de réponse de l'API.

        var authRequest = new Dictionary<string, string>
        {
            { "client_id", _clientId }, 
            { "client_secret", _clientSecret },
            { "scope", _scope },
            { "grant_type", "client_credentials" }
        };

        var response = await _httpClient.PostAsync("/connexion/oauth2/access_token?realm=%2Fpartenaire", new FormUrlEncodedContent(authRequest));

        if (!response.IsSuccessStatusCode)
        {
            // TODO : Gérer les cas d'erreur de l'API, notamment l'expiration du token (dans ce cas, relancer la requête).
            _logger.LogError("Failed to retrieve access token from France Travail API. Status code: {StatusCode}, Reason: {ReasonPhrase}",
                response.StatusCode, await response.Content.ReadAsStringAsync());
            throw new HttpRequestException($"Failed to retrieve access token: {response.ReasonPhrase}");
        }


        var result = await response.Content.ReadFromJsonAsync<AuthResponse>(_jsonSerializerOptions)
            ?? throw new InvalidOperationException("Failed to deserialize response from France Travail API.");

        return result.AccessToken;
    }
}