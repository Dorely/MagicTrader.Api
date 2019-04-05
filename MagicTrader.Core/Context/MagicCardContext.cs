

using MagicTrader.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrader.Core.Context
{
    public interface IMagicCardContext
    {
        Task<List<MagicCard>> GetCards(MagicCard parameters);
        Task<MagicCard> GetCard(string setCode, string cardName);
        Task<MagicCard> GetCard(Guid scryfallId);
        Task<bool> InsertCardPage(List<MagicCard_Temp> cardPage);
        Task<bool> MergeCards();
        Task<bool> ResetTempTable();

    }
    public class MagicCardContext : IMagicCardContext
    {
        private readonly MagicTraderContext _dbContext;
        public MagicCardContext(MagicTraderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MagicCard> GetCard(string setCode, string cardName)
        {
            return await _dbContext.MagicCards.FirstOrDefaultAsync(x => x.SetCode == setCode && x.CardName == cardName);
        }

        public async Task<MagicCard> GetCard(Guid scryfallId)
        {
            return await _dbContext.MagicCards.FirstOrDefaultAsync(x => x.ScryfallId == scryfallId);
        }

        public async Task<List<MagicCard>> GetCards(MagicCard parameters)
        {
            var predicate = _dbContext.MagicCards.Where(x=>true);

            if (!string.IsNullOrEmpty(parameters.SetCode))
            {
                predicate = predicate.Where(x => x.SetCode == parameters.SetCode);
            }
            if (!string.IsNullOrEmpty(parameters.CardName))
            {
                predicate = predicate.Where(x => x.CardName.ToLower().Contains(parameters.CardName.ToLower()));
            }

            return await predicate.ToListAsync();
        }

        public async Task<bool> InsertCardPage(List<MagicCard_Temp> cardPage)
        {
            try
            {
                _dbContext.MagicCards_Temp.AddRange(cardPage);
                await _dbContext.SaveChangesAsync();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> MergeCards()
        {
            try
            {

                await _dbContext.Database.ExecuteSqlCommandAsync
                    (@"MERGE MagicCards AS TARGET
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
                        DELETE;");
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ResetTempTable()
        {
            try
            {

                await _dbContext.Database.ExecuteSqlCommandAsync (@"DROP TABLE MagicCards_TEMP");
                await _dbContext.Database.ExecuteSqlCommandAsync (@"SELECT * INTO MagicCards_TEMP FROM [MagicCards] WHERE 1=0;");
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
