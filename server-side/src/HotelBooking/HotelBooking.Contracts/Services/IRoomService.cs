using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Contracts.Dto.Data;
using HotelBooking.Contracts.Dto.Models.Search;

namespace HotelBooking.Contracts.Services
{
    public interface IRoomService
    {
        Task<IReadOnlyCollection<RoomData>> GetAsync();

        Task<RoomData> GetAsync(int id);

        Task<IReadOnlyCollection<RoomData>> SearchByFiltersAsync(RoomRequirements roomRequirements);

        Task<IReadOnlyCollection<RoomData>> GetByHotelIdAsync(int hotelId);
    }
}