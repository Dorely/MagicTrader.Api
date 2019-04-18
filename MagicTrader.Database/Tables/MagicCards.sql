CREATE TABLE [dbo].[MagicCards]
(
	[ScryfallId] UNIQUEIDENTIFIER NOT NULL,
    [SetCode] NVARCHAR(10),
    [OracleId] UNIQUEIDENTIFIER NOT NULL,
    [MultiverseId] INT,
    [CardName] NVARCHAR(150) NOT NULL,
    [ApiUri] NVARCHAR(350),
    [ImageUri] NVARCHAR(350),
    [ImageUri2] NVARCHAR(350),
    [TypeLine] NVARCHAR(100),
    [OracleText] NVARCHAR(1000),
    [ManaCost] NVARCHAR(100),
    [CollectorNumber] INT,
    [Rarity] NVARCHAR(15),
    [FlavorText] NVARCHAR(600),
    [Artist] NVARCHAR(100),
    [PurchaseUri] NVARCHAR(350),
    [PriceUsd] DECIMAL(15,2),
    [PriceUsdFoil] DECIMAL(15,2),
	[Language] NVARCHAR(30)
	CONSTRAINT MagicCards_PK PRIMARY KEY ([ScryfallId]),
	CONSTRAINT MagicCards_FK1 FOREIGN KEY ([SetCode]) REFERENCES MagicSets([SetCode])
)
GO

CREATE NONCLUSTERED INDEX [MagicCards_NDX1] ON [MagicCards] ([CardName])
GO