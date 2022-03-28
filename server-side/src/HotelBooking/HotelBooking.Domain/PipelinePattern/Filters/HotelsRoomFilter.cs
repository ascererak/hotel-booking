using System.Linq;
using HotelBooking.Contracts.Dto.Models.Search;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class HotelsRoomFilter : IFilter<IQueryable<Hotel>>
    {
        private readonly RoomRequirements roomRequirements;
        private readonly IQueryable<Room> rooms;
        private readonly IQueryable<Order> orders;

        public HotelsRoomFilter(RoomRequirements roomRequirements, IQueryable<Room> rooms, IQueryable<Order> orders)
        {
            this.roomRequirements = roomRequirements;
            this.rooms = rooms;
            this.orders = orders;
        }

        public IQueryable<Hotel> Execute(IQueryable<Hotel> input)
        {
            if (!roomRequirements.Exists())
            {
                return input;
            }

            var roomsSelectionPipeline = new RoomsSelectionPipeline();
            roomsSelectionPipeline
                .Register(new RoomPriceFromFilter(roomRequirements.PriceFrom))
                .Register(new RoomPriceToFilter(roomRequirements.PriceTo))
                .Register(new RoomCapacityFilter(roomRequirements.RequiredCapacity))
                .Register(
                    new RoomAvailabilityFilter(roomRequirements.CheckIn, roomRequirements.CheckOut, orders));
            IQueryable<Room> filteredRooms = roomsSelectionPipeline.Process(rooms);

            var result = input.Where(hotel => filteredRooms.Any(room => room.HotelId == hotel.Id));

            return result;
        }
    }
}