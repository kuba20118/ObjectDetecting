using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepositoryTemporary : IImageRepository
    {
        private static ISet<Image> _images = new HashSet<Image>()
        {
            new Image(Guid.Parse("8b289ef3-0d5b-4722-9ef8-2733f3b0e68a"), new byte[] {0,1,2,3,100,200})
        };
        public async Task AddAsync(Image image)
        {
            _images.Add(image);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
            => await Task.FromResult(_images);

        public async Task<Image> GetAsync(Guid id)
            => await Task.FromResult(_images.SingleOrDefault(x => x.Id == id));
        public async Task RemoveAsync(Guid id)
        {
            var image = await GetAsync(id);
            _images.Remove(image);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Image id)
        {
            await Task.CompletedTask;
        }
    }
}