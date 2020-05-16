using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;

namespace Detector.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private static readonly ISet<Statistics> tempList = new HashSet<Statistics>();

        public async Task AddAsync(Statistics statistics)
        => await Task.FromResult(tempList.Add(statistics));

        public async Task<IEnumerable<Statistics>> GetAllAsync()
        => await Task.FromResult(tempList);

        public async Task<Statistics> GetAsync(Guid id)
        => await Task.FromResult(tempList.FirstOrDefault(x => x.ImageId == id));



    }
}