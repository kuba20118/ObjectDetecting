using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;

namespace Detector.Core.Repositories
{
    public interface IStatsRepository : IRepository
    {
        Task AddAsync(Statistics statistics);
        Task<Statistics> GetAsync(Guid id);
        Task<IEnumerable<Statistics>> GetAllAsync();

    }
}