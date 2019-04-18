CREATE TABLE [dbo].[MagicSets]
(
	[SetCode] NVARCHAR(10) NOT NULL,
    [SetName] NVARCHAR(100) NOT NULL,
    [ApiUri] NVARCHAR(350),
    [ReleaseDate] DATETIME2(7),
    [IconSvgUri] NVARCHAR(350),
	CONSTRAINT [MagicSets_PK] PRIMARY KEY ([SetCode])
)
GO

CREATE NONCLUSTERED INDEX [MagicSets_NDX1] ON [MagicSets] ([SetName])
GO