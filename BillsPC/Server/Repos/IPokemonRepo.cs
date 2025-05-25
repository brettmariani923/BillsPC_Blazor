using BillsPC.Shared.Models;


namespace BillsPC.Repos
{
    public interface IPokemonRepo
    {
        Task<List<PokemonInfo>> GetAllPokemonInfoAsync();
        Task<List<PokemonInfo>> ReturnPokemonLikeAsync(string name, int limit = 25);
    }
}
