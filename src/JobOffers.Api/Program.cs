using System.Text.Json;
using System.Text.Json.Serialization;
using JobOffers.Api.BackgroundServices;
using JobOffers.Api.DependencyInjection;
using JobOffers.Domain.Services;
using JobOffers.Domain.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add Open API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repositories and Data context
builder.Services.AddDataLayer(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(o => {
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.WriteIndented = true;
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add API
builder.Services.AddFranceTravailRepository(builder.Configuration);

// Add services
builder.Services.AddScoped<IJobOffersRetrieverService, JobOffersRetrieverService>();

// Add background services
builder.Services.AddHostedService<JobOffersRetrievalBatchService>();

// ---- Build the application ----
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO : Ajouter la gestion des erreurs globales, la sécurité (CORS, authentification, autorisation, HTTPS)


// ---- Configure the HTTP request pipeline ----


app.MapPost("/job-offers/retrieve", async (IJobOffersRetrieverService jobOffersRetrieverService) =>
{
    try
    {
        await jobOffersRetrieverService.RetrieveJobOffersAsync();
        return Results.Ok("Job offers retrieved successfully.");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/health", () =>
{
    try
    {
        // TODO : Il faudrait vérifier que la connexion à la base de données est opérationnelle 
        // et que le service  JobOffersRetrievalBatchService soit en cours d'exécution
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/statistics", () =>
{
    try
    {
        // TODO : Il faudrait ajouter un endpoint pour récupérer les statistiques de la dernière récupération
        // de données (nombre d'offres récupérées, nombre d'offres mises à jour, etc.)
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
