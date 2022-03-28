using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class RoomPriceFromFilter : IFilter<IQueryable<Room>>
    {
        private readonly int? priceFrom;

        public RoomPriceFromFilter(int? priceFrom)
        {
            this.priceFrom = priceFrom;
        }

        public IQueryable<Room> Execute(IQueryable<Room> input) => priceFrom == null
            ? input
            : input.Where(room => room.PricePerNight >= priceFrom);
    }
}