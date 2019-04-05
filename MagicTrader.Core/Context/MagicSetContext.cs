

using MagicTrader.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrader.Core.Context
{
    public interface IMagicSetContext
    {
        Task<List<MagicSet>> GetSets(MagicSet parameters);
        Task<MagicSet> GetSet(string setCode);
        Task<bool> InsertSetPage(List<MagicSet_Temp> setPage);
        Task<bool> MergeSets();
        Task<bool> ResetTempTable();

    }
    public class MagicSetContext : IMagicSetContext
    {
        private readonly MagicTraderContext _dbContext;
        public MagicSetContext(MagicTraderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MagicSet> GetSet(string setCode)
        {
            return await _dbContext.MagicSets.SingleAsync(x => x.SetCode == setCode);
        }

        public async Task<List<MagicSet>> GetSets(MagicSet parameters)
        {
            var predicate = _dbContext.MagicSets.Where(x=>true);

            if (!string.IsNullOrEmpty(parameters.SetCode))
            {
                predicate = predicate.Where(x => x.SetCode == parameters.SetCode);
            }
            if (!string.IsNullOrEmpty(parameters.SetName))
            {
                predicate = predicate.Where(x => x.SetName.ToLower().Contains(parameters.SetName.ToLower()));
            }


            return await predicate.ToListAsync();
        }

        public async Task<bool> InsertSetPage(List<MagicSet_Temp> setPage)
        {
            try
            {
                _dbContext.MagicSets_Temp.AddRange(setPage);
                await _dbContext.SaveChangesAsync();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> MergeSets()
        {
            try
            {

                await _dbContext.Database.ExecuteSqlCommandAsync
                    (@"MERGE MagicSets AS TARGET
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

                await _dbContext.Database.ExecuteSqlCommandAsync (@"DROP TABLE MagicSets_Temp");
                await _dbContext.Database.ExecuteSqlCommandAsync (@"SELECT * INTO MagicSets_TEMP FROM [MagicSets] WHERE 1=0");
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
