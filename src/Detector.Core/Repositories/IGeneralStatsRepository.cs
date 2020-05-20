using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;

namespace Detector.Core.Repositories
{
    public interface IGeneralStatsRepository : IRepository
    {
        Task CreateAsync();
        Task UpdateAsync(Feedback feedback, int numberOfObjects, List<string> foundObjects, long time);
        Task<GeneralStats> GetAsync();
    }
}