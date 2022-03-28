using System;
using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.Interfaces;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern.Filters
{
    internal class RoomAvailabilityFilter : IFilter<IQueryable<Room>>
    {
        private readonly DateTime? checkInNullable;
        private readonly DateTime? checkOutNullable;
        private readonly IQueryable<Order> orders;

        public RoomAvailabilityFilter(DateTime? checkInNullable, DateTime? checkOutNullable, IQueryable<Order> orders)
        {
            this.checkInNullable = checkInNullable;
            this.checkOutNullable = checkOutNullable;
            this.orders = orders;
        }

        public IQueryable<Room> Execute(IQueryable<Room> input)
        {
            if (checkInNullable == null || checkOutNullable == null)
            {
                return input;
            }

            DateTime checkIn = checkInNullable.Value;
            DateTime checkOut = checkOutNullable.Value;
            var today = DateTime.UtcNow;

            var result = input.Where(room =>
                !orders.Any(order => order.RoomId == room.Id && DateTime.Compare(order.CheckOut, today) >= 0) ||
                orders.Any(order => order.RoomId == room.Id && DateTime.Compare(order.CheckOut, today) > 0 &&
                                    ((DateTime.Compare(checkIn, order.CheckIn) < 0 &&
                                      DateTime.Compare(checkOut, order.CheckIn) < 0) ||
                                     (DateTime.Compare(order.CheckOut, checkIn) < 0 &&
                                      DateTime.Compare(order.CheckOut, checkIn) < 0))));
            return result;
        }
    }
}