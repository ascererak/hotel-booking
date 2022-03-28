using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class HotelNameFilter : IFilter<IQueryable<Hotel>>
    {
        private readonly string substring;

        public HotelNameFilter(string substring)
        {
            this.substring = substring;
        }

        public IQueryable<Hotel> Execute(IQueryable<Hotel> input) => string.IsNullOrEmpty(substring)
            ? input
            : input.Where(hotel => hotel.Name.ToLower().Contains(substring.ToLower()));
    }
}