using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Infrastructure.Charts;
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Services
{
    public interface IStatsService : IService
    {
        Task AddStatsToImage(Guid id, Feedback stats);
        Task<StatsDto> GetImageStats(Guid id);
        Task<IEnumerable<Statistics>> GetAll();
        Task<SummaryStats> GetSummaryStats();
        Task UpdateGeneralStats(Feedback stats);
    }
}