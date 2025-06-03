using System.Text.Json;
using System.Text.Json.Serialization;
using JobOffers.Api.DependencyInjection;
using JobOffers.Domain.Services;
using JobOffers.Domain.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add Open API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repositories and Data context
builder.Services.AddDataLayer();

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

// ---- Build the application ----
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/offres", async (IJobOffersRetrieverService jobOffersRetrieverService) =>
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

app.Run();
