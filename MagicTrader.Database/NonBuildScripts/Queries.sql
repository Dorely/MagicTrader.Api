-- make a global temp tables for inserting
DROP TABLE MagicSets_TEMP;
DROP TABLE MagicCards_TEMP;
SELECT * INTO MagicSets_TEMP FROM [MagicSets] WHERE 1=0;
SELECT * INTO MagicCards_TEMP FROM [MagicCards] WHERE 1=0;


-- merge statements for updating data
MERGE MagicSets AS TARGET
USING MagicSets_TEMP AS SOURCE 
ON (TARGET.SetCode = SOURCE.SetCode)
WHEN MATCHED THEN 
UPDATE 
SET 
TARGET.SetName = SOURCE.SetName,
TARGET.ApiUri = SOURCE.ApiUri,
TARGET.ReleaseDate = SOURCE.ReleaseDate,
TARGET.IconSvgUri = SOURCE.IconSvgUri
WHEN NOT MATCHED BY TARGET THEN
INSERT (SetCode, SetName, ApiUri, ReleaseDate, IconSvgUri) 
VALUES (SOURCE.SetCode, SOURCE.SetName, SOURCE.ApiUri, SOURCE.ReleaseDate, SOURCE.IconSvgUri)
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;


MERGE MagicCards AS TARGET
USING MagicCards_TEMP AS SOURCE 
ON (TARGET.ScryfallId = SOURCE.ScryfallId)
WHEN MATCHED THEN 
UPDATE 
SET 
TARGET.SetCode = SOURCE.SetCode,
TARGET.OracleId = SOURCE.OracleId,
TARGET.MultiverseId = SOURCE.MultiverseId,
TARGET.CardName = SOURCE.CardName,
TARGET.ApiUri = SOURCE.ApiUri,
TARGET.ImageUri = SOURCE.ImageUri,
TARGET.TypeLine = SOURCE.TypeLine,
TARGET.OracleText = SOURCE.OracleText,
TARGET.ManaCost = SOURCE.ManaCost,
TARGET.CollectorNumber = SOURCE.CollectorNumber,
TARGET.Rarity = SOURCE.Rarity,
TARGET.FlavorText = SOURCE.FlavorText,
TARGET.Artist = SOURCE.Artist,
TARGET.PurchaseUri = SOURCE.PurchaseUri,
TARGET.PriceUsd = SOURCE.PriceUsd
WHEN NOT MATCHED BY TARGET THEN
INSERT (ScryfallId, SetCode, OracleId, MultiverseId, CardName, ApiUri, ImageUri, TypeLine, OracleText, ManaCost, CollectorNumber, Rarity, FlavorText, Artist, PurchaseUri, PriceUsd) 
VALUES (SOURCE.ScryfallId, SOURCE.SetCode, SOURCE.OracleId, SOURCE.MultiverseId, SOURCE.CardName, SOURCE.ApiUri, SOURCE.ImageUri, SOURCE.TypeLine, SOURCE.OracleText, SOURCE.ManaCost, SOURCE.CollectorNumber, SOURCE.Rarity, SOURCE.FlavorText, SOURCE.Artist, SOURCE.PurchaseUri, SOURCE.PriceUsd)
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;




