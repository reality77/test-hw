namespace JobOffers.Domain.Models;

/// <summary>
/// Represents the sources from which job offers can be retrieved.
/// </summary>
public enum JobOfferSources : byte
{
    FranceTravail = 1,

    // HellowWork,      // TODO : Evolution vers de la recherche d'offres d'emploi via d'autres sources

    // LinkedIn,        // TODO : Evolution vers de la recherche d'offres d'emploi via d'autres sources
}