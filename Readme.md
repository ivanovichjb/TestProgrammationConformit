# REST ENDPOINTS TestProgrammationConformit
## 
Cette application ouvre des points de terminaison REST pour permettre la création, la mise à jour, et la suppression d'enregistrements pour un événement qui est décrit par l'ID de l'événement, le sujet, l'expéditeur. Chaque événement a un commentaire décrit par l'ID de la description du commentaire la date d'envoi. 

ivanovichjb/TestProgrammationConformit
[![N|GitHub](https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5FtWop0sp2E8gDQCzBHRoESzIvLJHLo5VF9RlhHk&usqp=CAU)](https://github.com/ivanovichjb/TestProgrammationConformit)(Click)

> Il existe deux contrôleurs qui implémentent les opérations de base `CRUD de manière asynchrone`. Il y a une relation un à un par commentaire d'événement créé. Utilisation d'Entity Framework et Linq pour l'intégration de l'option de requête. Le projet a implémenté la norme DTO pour le transfert de données et la gestion des réponses http des exceptions. Las migraciones de bases Postgres de datos se realizaron con Entity Framework Core y esta disponible las creacion de las entidades


## Controller Evenement

| URLEndpoint | Operation | Paramètres | Function | Retour
| ------ | ------ | ------ | ------ | ------ |
| api/evenement | GET | | Récupérer les détails des événement | JSON objet de réponse |
| api/evenement/{id} | GET | id : evenmentID correspondant aux informations d'événement à récupérer| Récupérer le détail d' événement |  JSON objet de réponse |
| api/evenement | POST | id : evenmentID ID correspondant aux informations d'événement à récupérer| Crée un nouvel enregistrement d'événement dans la base de données | Response Created|
| api/evenement/{id} | PUT | id : evenmentID ID Pour modifier l'événement | Modifier Le titre et la personne qui l'a envoyé | Response No Content
| api/evenement/{id} | DELETE | id : evenmentID ID Pour supprimer l'événement entier | Supprimer l'événement entier de la base (cela inclut le commentaire en raison de la relation en cascade un-à-un) | Response No Content

## Controller Comentaire

| URLEndpoint | Operation | Paramètres | Function | Retour
| ------ | ------ | ------ | ------ | ------ |
| api/commentaire | GET | | Récupérer les détails des commentaire | JSON objet de réponse |
| api/commentaire/{id} | GET | id : commentaireID correspondant aux informations de commentaire à récupérer| Récupérer le détail de commentaire |  JSON objet de réponse |
| api/commentaire | POST | id : commentaireID ID correspondant aux informations de commentaire à récupérer| Crée un nouvel enregistrement de commentaire dans la base de données | Response Created|
| api/commentaire/{id} | PUT | id : comentaireID ID Pour modifier l'commentaire | Modifier seule la description du commentaire est modifiée car la date de création ne doit pas être une donnée à modifier | Response No Content
| api/commentaire/{id} | DELETE | id : commentaireID ID Pour supprimer le commentaire entier | Supprimer le commentaire entier de la base | Response No Content

## Exécution
Exécution du projet spécifié:
```sh
dotnet run --project ./TestProgrammationConformit//TestProgrammationConformit.csproj
```
Restaurez les dépendances et les outils du projet dans le répertoire actuel pour afficher uniquement la sortie minimale, puis exécutez le projet:
```sh
dotnet run --verbosity m
```

> Note: Vérifiez le déploiement en accédant à l'adresse de votre serveur dans
votre navigateur préféré.

