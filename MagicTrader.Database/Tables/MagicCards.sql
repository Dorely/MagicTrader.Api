CREATE TABLE [dbo].[MagicCards]
(
	[ScryfallId] UNIQUEIDENTIFIER NOT NULL,
    [SetCode] VARCHAR(10),
    [OracleId] UNIQUEIDENTIFIER NOT NULL,
    [MultiverseId] INT,
    [CardName] VARCHAR(150) NOT NULL,
    [ApiUri] VARCHAR(350),
    [ImageUri] VARCHAR(350),
    [TypeLine] VARCHAR(100),
    [OracleText] VARCHAR(600),
    [ManaCost] VARCHAR(100),
    [CollectorNumber] INT,
    [Rarity] VARCHAR(15),
    [FlavorText] VARCHAR(600),
    [Artist] VARCHAR(100),
    [PurchaseUri] VARCHAR(350),
    [PriceUsd] DECIMAL(15,2),
	CONSTRAINT MagicCards_PK PRIMARY KEY ([ScryfallId]),
	CONSTRAINT MagicCards_FK1 FOREIGN KEY ([SetCode]) REFERENCES MagicSets([SetCode])
)
GO

CREATE NONCLUSTERED INDEX [MagicCards_NDX1] ON [MagicCards] ([CardName])
GO