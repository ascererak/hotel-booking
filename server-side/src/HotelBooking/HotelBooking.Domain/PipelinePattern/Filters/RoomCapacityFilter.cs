using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class RoomCapacityFilter : IFilter<IQueryable<Room>>
    {
        private readonly int? requiredCapacity;

        public RoomCapacityFilter(int? requiredCapacity)
        {
            this.requiredCapacity = requiredCapacity;
        }

        public IQueryable<Room> Execute(IQueryable<Room> input) => requiredCapacity == null
            ? input
            : input.Where(room => room.NumberOfPeople >= requiredCapacity);
    }
}