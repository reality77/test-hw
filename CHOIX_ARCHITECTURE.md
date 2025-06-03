# Architecture Job Offers

## Choix d'une API

Typiquement, ce type d'application serait réalisé sous la forme un batch exécutable planifié (via CRON par exemple).

Cependant, j'ai choisi de l'implémenter sous la forme d'un Background Service au sein d'une API, pour montrer qu'il est possible de gérer ces tâches autrement.
De plus, cette solution nous permet d'accéder à des informations liées à cette tâche, notamment : 
- des endpoints pour afficher les statistiques du lot traité en direct (ou du dernier lot traité)
- des endpoints pour gérer de la tâche elle-même (déclenchement manuel par exemple)
- un endpoint de healthcheck qui permet d'indiquer si l'application fonctionne bien (idéal dans un contexte containerisé)

Inconvénients de cette solution :
- Le code est un peu plus complexe, il faut notamment bien veiller à ce que le BackgroundService gère proprement toutes les erreurs pour qu'il ne s'arrête pas, et gérer la relance de l'hôte de l'API elle-même si celui-ci venait à s'arrêter en erreur.


## Choix d'une architecture Services/Repositories/API Client

- Permet de gérer rapidement d'autres fournisseurs d'offres d'emploi 
- Les API pourront être facilement déplacées dans une librairie externe (NuGet) afin de pouvoir les réutiliser sans lien avec les repositories
