using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Contracts.Dto.Data;
using HotelBooking.Contracts.Dto.Models.Search;

namespace HotelBooking.Contracts.Domain.Repositories
{
    public interface IHotelRepository
    {
        Task<IReadOnlyCollection<HotelData>> GetAsync();

        Task<HotelData> GetAsync(int id);

        Task<IReadOnlyCollection<HotelData>> GetByPageAsync(int page, int pageSize);

        Task<IReadOnlyCollection<HotelData>> GetSearchResultByPageAsync(DataForSearch dataForSearchData, int pageSize);

        Task<int> CountSearchResultsAsync(DataForSearch dataForSearch);
    }
}