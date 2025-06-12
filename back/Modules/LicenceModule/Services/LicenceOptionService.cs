using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Dapper;

namespace Back.Modules.LicenceModule.Services
{
    public class LicenceOptionService : ILicenceOptionService
    {
        private readonly IDbConnection _db;

        public LicenceOptionService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<LicenceOption?> GetByIdAsync(string optionId)
        {
            const string query = @"
                SELECT * FROM licence_options
                WHERE option_id = @OptionId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<LicenceOption>(query, new { OptionId = optionId });
        }

        public async Task<IEnumerable<LicenceOption>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM licence_options
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<LicenceOption>(query);
        }

        public async Task<IEnumerable<LicenceOption>> GetByLicenceIdAsync(string licenceId)
        {
            const string query = @"
                SELECT * FROM licence_options
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            return await _db.QueryAsync<LicenceOption>(query, new { LicenceId = licenceId });
        }

        public async Task<string> CreateAsync(LicenceOption option)
        {
            const string query = @"
                INSERT INTO licence_options (option_id,licence_id, option_name, description, price, is_archived)
                VALUES (@OptionId,@LicenceId, @OptionName, @Description, @Price, false);
                SELECT LAST_INSERT_ID();
            ";

            return await _db.ExecuteScalarAsync<string>(query, option);
        }

        public async Task<bool> UpdateAsync(LicenceOption option)
        {
            const string query = @"
                UPDATE licence_options
                SET licence_id = @LicenceId,
                    option_name = @OptionName,
                    description = @Description,
                    price = @Price
                WHERE option_id = @OptionId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, option);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(string optionId)
        {
            const string query = @"
                UPDATE licence_options
                SET is_archived = true
                WHERE option_id = @OptionId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, new { OptionId = optionId });
            return affectedRows > 0;
        }
    }
}
