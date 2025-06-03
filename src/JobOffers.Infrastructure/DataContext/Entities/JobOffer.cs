using JobOffers.Domain.Models;

namespace JobOffers.Infrastructure.DataContext.Entities;

/// <summary>
/// Represents a job offer entity in the database.
/// </summary>
public class JobOffer
{
    public required Guid Id { get; set; }
    public required JobOfferSources Source { get; set; }
    public required string ExternalId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? CompanyName { get; set; }
    public string? Location { get; set; }
    public DateTime? PostedAt { get; set; }
    public string? ContractType { get; set; }
}