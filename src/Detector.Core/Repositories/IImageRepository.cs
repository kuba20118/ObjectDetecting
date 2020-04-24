using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;

namespace Detector.Core.Repositories
{
    public interface IImageRepository : IRepository
    {
        Task AddAsync(Image image);
        Task<Image> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task<IEnumerable<Image>> GetAllAsync();
        Task UpdateAsync(Image id);

    }
}