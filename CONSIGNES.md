# Test technique

Le but de cet exercice est de construire une petite application de récupération d’offres
d’emploi.
Cette application doit
- Récupérer les offres d’emploi de Rennes, Bordeaux et Paris à partir de l’API de Pôle
Emploi : pole-emploi.io ;
- Stocker les détails importants dans une base de données locale (Sqlite, MongoDB...) avec
notamment la description et l’url pour postuler ;
- Être capable d’enrichir la DB avec de nouvelles offres : mise à jour incrémentale ;
Afficher un rapport (en console ou en fichier) des statistiques de récupération affichant :
  * le type de contrat ;
  * l’entreprise ;
  * le pays.

## Précisions

### Language

Vous être libre de choisir votre language de prédilection !

### Base de données
Nous recommandons l’usage de MongoDB ou Sqlite, mais vous êtes libre d’en choisir une
autre.

### Packaging
Il faut que nous soyons capable de tester le projet localement rapidement, mais vous êtes
libre sur la manière de nous l’envoyer.

### Durée
L’idée est de ne pas dépasser 3 heures de travail, même si votre application n’est pas
terminée.

Sentez-vous libre d’ajouter votre touche personnelle à cette application si vous le
souhaitez !