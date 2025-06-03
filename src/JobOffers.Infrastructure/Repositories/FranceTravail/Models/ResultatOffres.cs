
namespace JobOffers.Infrastructure.Repositories.FranceTravail.Models;

// TODO : Séparer dans plusieurs fichiers si nécessaire

public record ResultatOffres
{
    /// <summary>
    /// Gets or sets the list of job offers.
    /// </summary>
    public required IEnumerable<Offre> Resultats { get; set; }
}

public record Offre
{
    /// <summary>
    /// Gets or sets the unique identifier of the job offer.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the job offer.
    /// </summary>
    public required string Intitule { get; set; }

    /// <summary>
    /// Gets or sets the description of the job offer.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the contact information for the job offer.
    /// </summary>
    public Contact? Contact { get; set; }

    /// <summary>
    /// Gets or sets the work location of the job offer.
    /// </summary>
    public LieuTravail? LieuTravail { get; set; }

    /// <summary>
    /// Gets or sets the company information for the job offer.
    /// </summary>
    public Entreprise? Entreprise { get; set; }

    /// <summary>
    /// Gets or sets the date when the job offer was created.
    /// </summary>
    public DateTime? DateCreation { get; set; }

    /// <summary>
    /// Gets or sets the type of contract for the job offer (e.g., CDI, CDD, ...).
    /// </summary>
    public string? TypeContrat { get; set; }
}

public record LieuTravail
{
    /// <summary>
    /// Gets or sets the label of the work location.
    /// </summary>
    public required string Libelle { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the work location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the work location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the work location.
    /// </summary>
    public string? CodePostal { get; set; }

    /// <summary>
    /// Gets or sets the commune code of the work location.
    /// </summary>
    public string? Commune { get; set; }
}

public record Entreprise
{
    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Gets or sets the description of the company.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the logo URL of the company.
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// Gets or sets the URL of the company's website.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the company is adapted for people with disabilities.
    /// </summary>
    public bool EntrepriseAdaptee { get; set; }
}

public record Contact
{
    /// <summary>
    /// Gets or sets the email address of the contact.
    /// </summary>
    public string? Courriel { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the contact.
    /// </summary>
    public string? Telephone { get; set; }

    /// <summary>
    /// Gets or sets the URL to apply to the offer.
    /// </summary>
    public string? UrlPostulation { get; set; }
}