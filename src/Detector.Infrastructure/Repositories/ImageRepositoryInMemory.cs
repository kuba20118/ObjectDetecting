using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepositoryInMemory : IImageRepository
    {

        private static readonly ISet<Image> tempList = new HashSet<Image>();

        public ImageRepositoryInMemory()
        {
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