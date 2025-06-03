# Job Offers Retrieval

Cette application permet de récupérer les offres d'emploi de France Travail toutes les 24 heures, et de les stocker dans une base de données.

## Environnement de développement

- Linux ou WSL 2 sous Windows
- VSCode avec C# DevKit Extension
- dotnet sdk 9 et runtime

## Initialiser le projet

- Renseigner l'identifiant France Travail

```bash
cd src/JobOffers.Api/
dotnet user-secrets set JobOffersApi:FranceTravail:ClientId <Identifiant client>
dotnet user-secrets set JobOffersApi:FranceTravail:ClientSecret <Clé secrète>
```

- Créer/mettre à jour le schéma de base de données

```bash
cd src/JobOffers.Api/
dotnet ef database update
```

- Lancer l'application

```bash
cd src/JobOffers.Api/
dotnet run
```

## Résultats

- Les statistiques apparaissent dans le fichier `job_offers_stats.log`
- Les données sont stockées dans la base SQLite située dans le fichier `JobOffers.db`

