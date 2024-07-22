using NewWebApi.Models;

namespace NewWebApi.Repositories.Contracts
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCards(string descName);
    }
}