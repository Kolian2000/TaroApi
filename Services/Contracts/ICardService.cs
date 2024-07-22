using NewWebApi.Models;

namespace NewWebApi.Services.Contracts
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetCards(string descName);
    }
}