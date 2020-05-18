using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Detector.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Statistics> Stats => _database.GetCollection<Statistics>("Statistics");
        public StatsRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Statistics statistics)
            => await Stats.InsertOneAsync(statistics);

        public async Task<IEnumerable<Statistics>> GetAllAsync()
            => await Stats.AsQueryable().ToListAsync();

        public async Task<Statistics> GetAsync(Guid id)
            => await Stats.AsQueryable().FirstOrDefaultAsync(x => x.ImageId == id);
    }
}