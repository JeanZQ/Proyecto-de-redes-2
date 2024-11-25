-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- ContaminaDos.dbo.Game definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.Game;

CREATE TABLE ContaminaDos.dbo.Game (
	Id uniqueidentifier NOT NULL,
	Name nvarchar(20) COLLATE Latin1_General_CI_AI NOT NULL,
	GameStatus int NOT NULL,
	CreatedAt datetime2 NOT NULL,
	UpdatedAt datetime2 NOT NULL,
	Password nvarchar(20) COLLATE Latin1_General_CI_AI NULL,
	CurrentRoundId uniqueidentifier NULL,
	Owner nvarchar(MAX) COLLATE Latin1_General_CI_AI NOT NULL,
	CONSTRAINT PK_Game PRIMARY KEY (Id)
);
 CREATE  UNIQUE NONCLUSTERED INDEX IX_Game_Name ON dbo.Game (  Name ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- ContaminaDos.dbo.Players definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.Players;

CREATE TABLE ContaminaDos.dbo.Players (
	Id uniqueidentifier NOT NULL,
	GameId uniqueidentifier NOT NULL,
	PlayerName nvarchar(20) COLLATE Latin1_General_CI_AI NOT NULL,
	IsEnemy bit NULL,
	CONSTRAINT PK_Players PRIMARY KEY (Id)
);


-- ContaminaDos.dbo.Round definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.Round;

CREATE TABLE ContaminaDos.dbo.Round (
	Id uniqueidentifier NOT NULL,
	Leader nvarchar(MAX) COLLATE Latin1_General_CI_AI NOT NULL,
	Status int NOT NULL,
	[Result] int NOT NULL,
	Phase int NOT NULL,
	GameId uniqueidentifier NOT NULL,
	UpdatedAt datetime2 NOT NULL,
	CreatedAt datetime2 NOT NULL,
	CONSTRAINT PK_Round PRIMARY KEY (Id)
);


-- ContaminaDos.dbo.RoundGroup definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.RoundGroup;

CREATE TABLE ContaminaDos.dbo.RoundGroup (
	Id uniqueidentifier NOT NULL,
	RoundId uniqueidentifier NOT NULL,
	Player nvarchar(MAX) COLLATE Latin1_General_CI_AI NOT NULL,
	CONSTRAINT PK_RoundGroup PRIMARY KEY (Id)
);


-- ContaminaDos.dbo.RoundVote definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.RoundVote;

CREATE TABLE ContaminaDos.dbo.RoundVote (
	Id uniqueidentifier NOT NULL,
	RoundId uniqueidentifier NOT NULL,
	PlayerName nvarchar(MAX) COLLATE Latin1_General_CI_AI NOT NULL,
	Vote int NOT NULL,
	GroupVote int NULL,
	CONSTRAINT PK_RoundVote PRIMARY KEY (Id)
);


-- ContaminaDos.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE ContaminaDos.dbo.[__EFMigrationsHistory];

CREATE TABLE ContaminaDos.dbo.[__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE Latin1_General_CI_AI NOT NULL,
	ProductVersion nvarchar(32) COLLATE Latin1_General_CI_AI NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);