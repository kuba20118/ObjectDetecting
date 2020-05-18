using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Database;

namespace Detector.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        //private readonly DataContext _context;
        public StatsRepository()
        {
           // _context = context;
        }

        public Task AddAsync(Statistics statistics)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Statistics>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Statistics> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}