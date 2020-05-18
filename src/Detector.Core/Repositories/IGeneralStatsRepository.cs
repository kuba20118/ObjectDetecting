using System.Threading.Tasks;
using Detector.Core.Domain;

namespace Detector.Core.Repositories
{
    public interface IGeneralStatsRepository : IRepository
    {
        Task CreateAsync();
        Task UpdateAsync();
        Task<GeneralStats> GetAsync();
    }
}