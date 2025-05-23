Pasos CQRS
1. Models
2. IRepository
3. Repository
4. Query
5. Command
6. Handler
7. Controller/API
8. Program.cs

Para entrar al swagger
https://localhost:7047/swagger/index.html

Para ejecutar la aplicacion con https
dotnet run --launch-profile https

Migracion a la DB
Cuando se modifica Models se puede ejecutar el siguiente comando para crear una img
dotnet ef migrations add InitialCreate
Despues de la img se manda a la DB de la U
dotnet ef database update

Diagrama de la DB
+------------------+
|       Game       |
+------------------+
| Id (PK)          |   <-- Identificador único del juego (gameId)
| Name             |   <-- Nombre del juego (gameName)
| Status           |   <-- Estado del juego (gameStatus)
| gamePwd          |   <-- Contraseña del juego (minLength: 3, maxLength: 20)
| CurrentRoundId   |   <-- Identificador de la ronda actual (FK)
| IdOwner          |   <-- Dueno de la sala
+------------------+
        |
        |
        | 1
        |
        | N
+------------------+
|   Players        |
+------------------+
| Id (PK)          |   <-- Identificador único del participante
| GameId (FK)      |   <-- Identificador del juego al que pertenece el participante (gameId)
| PlayerName       |   <-- Nombre del participante (minLength: 3, maxLength: 20)
| IsEnemy          |   <-- Indica si es un enemigo (boolean)
+------------------+
        |
        |
        | 1
        |
        | N
+------------------+
|      Round       |
+------------------+
| Id (PK)          |   <-- Identificador único de la ronda (roundId)
| Leader           |   <-- Nombre del líder de la ronda (debe ser un PlayerName)
| Status           |   <-- Estado de la ronda
| Result           |   <-- Resultado de la ronda
| Phase            |   <-- Fase actual de la ronda
| GameId (FK)      |   <-- Identificador del juego al que pertenece la ronda (gameId)
+------------------+
        |
        |
        | 1
        |
        | N
+------------------+
|   RoundGroup     |
+------------------+
| Id (PK)          |   <-- Identificador único del grupo
| RoundId (FK)     |   <-- Identificador de la ronda (roundId)
| PlayerName       |   <-- Nombre del jugador en el grupo (minLength: 3, maxLength: 20)
+------------------+
        |
        |
        | 1
        |
        | N
+------------------+
|   RoundVote      |
+------------------+
| Id (PK)          |   <-- Identificador único del voto
| RoundId (FK)     |   <-- Identificador de la ronda (roundId)
| Vote             |
| GroupVote        |   
+------------------+