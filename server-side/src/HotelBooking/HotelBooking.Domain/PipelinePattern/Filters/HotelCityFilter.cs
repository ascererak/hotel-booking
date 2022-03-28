using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class HotelCityFilter : IFilter<IQueryable<Hotel>>
    {
        private readonly string cityName;

        public HotelCityFilter(string cityName)
        {
            this.cityName = cityName;
        }

        public IQueryable<Hotel> Execute(IQueryable<Hotel> input) => string.IsNullOrEmpty(cityName)
            ? input
            : input.Where(hotel => hotel.City.ToLower().Contains(cityName.ToLower()));
    }
}