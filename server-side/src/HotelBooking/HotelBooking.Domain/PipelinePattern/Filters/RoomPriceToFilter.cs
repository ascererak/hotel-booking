using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class RoomPriceToFilter : IFilter<IQueryable<Room>>
    {
        private readonly int? priceTo;

        public RoomPriceToFilter(int? priceTo)
        {
            this.priceTo = priceTo;
        }

        public IQueryable<Room> Execute(IQueryable<Room> input) => priceTo == null
        ? input
        : input.Where(room => room.PricePerNight <= priceTo);
    }
}