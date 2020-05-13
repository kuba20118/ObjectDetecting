using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Database;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository, ISqlRepository
    {
        private readonly DataContext _context;

        public List<Image> tempList = new List<Image>();

        public ImageRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Image image)
        {
            tempList.Add(image);
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(Guid id)
        {
            return tempList.FirstOrDefault(x => x.Id == id);
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