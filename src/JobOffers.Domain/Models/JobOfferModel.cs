namespace JobOffers.Domain.Models;

public record JobOfferModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the job offer.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the job offer.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the job offer.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the URL to apply to the job offer.
    /// </summary>
    public string? ApplyUrl { get; set; }

    /// <summary>
    /// Gets or sets the source from which the job offer was retrieved.
    /// </summary>
    public required JobOfferSources Source { get; init; }

    /// <summary>
    /// Gets or sets the name of the company offering the job.
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the date when the job offer was posted.
    /// </summary>
    public DateTime? PostedAt { get; set; }

    /// <summary>
    /// Gets or sets the location of the job offer.
    /// </summary>
    public string? Location { get; set; }
}