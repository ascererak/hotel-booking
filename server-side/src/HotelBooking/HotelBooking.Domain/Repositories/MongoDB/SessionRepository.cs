using System.Threading.Tasks;
using HotelBooking.Contracts.Domain.Repositories;
using HotelBooking.Contracts.Dto.Data;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HotelBooking.Domain.Repositories.MongoDB
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly IApplicationDbContext context;

        public SessionRepository(IApplicationDbContext context)
        {
            var client = new MongoClient(
                "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false"
            );
            this.context = context;
        }

        public async Task CreateAsync(SessionData sessionData)
        {
            var session = new Session
            {
                UserId = sessionData.UserId,
                Token = sessionData.Token
            };

            await context.Sessions.AddAsync(session);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(SessionData sessionData)
        {
            Session session = await context.Sessions.FirstOrDefaultAsync(record =>
                record.Token == sessionData.Token);

            context.Sessions.Remove(session);
            await context.SaveChangesAsync();
        }

        public async Task<SessionData> GetByTokenAsync(string token)
            => Map(await context.Sessions.FirstOrDefaultAsync(session => session.Token == token));

        private SessionData Map(Session session)
            => (session == null) ? null : new SessionData
            {
                Id = session.Id,
                Token = session.Token,
                UserId = session.UserId,
            };

        private Session Map(SessionData sessionData)
            => new Session
            {
                Token = sessionData.Token,
                UserId = sessionData.UserId,
            };
    }
}