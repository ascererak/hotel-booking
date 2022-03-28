using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.AbstractClasses;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern
{
    internal class RoomsSelectionPipeline : Pipeline<IQueryable<Room>>
    {
        public override IQueryable<Room> Process(IQueryable<Room> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }
}