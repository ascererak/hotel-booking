using System.Threading.Tasks;
using HotelBooking.Contracts.Dto.Data;

namespace HotelBooking.Contracts.Domain.Repositories
{
    public interface ISessionRepository
    {
        Task CreateAsync(SessionData sessionData);

        Task RemoveAsync(SessionData sessionData);

        Task<SessionData> GetByTokenAsync(string token);
    }
}