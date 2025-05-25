using Dapper;
using System.Data;
using BillsPC.Shared.Models;

namespace BillsPC.Repos
{
    public class PokemonRepo : IPokemonRepo
    {
        private readonly IDbConnection _db;

        public PokemonRepo(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<PokemonInfo>> GetAllPokemonInfoAsync()
        {
            var sql = "SELECT * FROM dbo.Pokemon";
            var result = await _db.QueryAsync<PokemonInfo>(sql);
            return result.AsList();
        }

        public async Task<List<PokemonInfo>> ReturnPokemonLikeAsync(string name, int limit = 25)
        {
            var result = await _db.QueryAsync<PokemonInfo>(
                @"SELECT TOP (@Limit) * FROM dbo.Pokemon WHERE Name LIKE @NamePattern",
                new { Limit = limit, NamePattern = $"%{name}%" });
            return result.AsList();
        }
    }
}
