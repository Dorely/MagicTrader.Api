CREATE TABLE [dbo].[MagicSets]
(
	[SetCode] VARCHAR(10) NOT NULL,
    [SetName] VARCHAR(100) NOT NULL,
    [ApiUri] VARCHAR(350),
    [ReleaseDate] DATETIME2(7),
    [IconSvgUri] VARCHAR(350),
	CONSTRAINT [MagicSets_PK] PRIMARY KEY ([SetCode])
)
GO

CREATE NONCLUSTERED INDEX [MagicSets_NDX1] ON [MagicSets] ([SetName])
GO