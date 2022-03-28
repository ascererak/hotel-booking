using System.Linq;
using HotelBooking.Contracts.Services.PipelinePattern.AbstractClasses;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.PipelinePattern
{
    internal class HotelSelectionPipeline : Pipeline<IQueryable<Hotel>>
    {
        public override IQueryable<Hotel> Process(IQueryable<Hotel> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }
}