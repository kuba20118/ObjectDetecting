using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Database;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository, ISqlRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Image image)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Image id)
        {
            throw new NotImplementedException();
        }
    }
}