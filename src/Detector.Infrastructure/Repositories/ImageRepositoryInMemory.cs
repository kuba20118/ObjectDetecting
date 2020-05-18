using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Database;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepositoryInMemory : IImageRepository, ISqlRepository
    {
        private readonly DataContext _context;

        private static readonly ISet<Image> tempList = new HashSet<Image>();

        public ImageRepositoryInMemory(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Image image)
        {
            tempList.Add(image);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        => await Task.FromResult(tempList);

        public async Task<Image> GetAsync(Guid id)
        {
            return await Task.FromResult(tempList.FirstOrDefault(x => x.Id == id));
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Image id)
        {
            await Task.CompletedTask;
        }
    }
}