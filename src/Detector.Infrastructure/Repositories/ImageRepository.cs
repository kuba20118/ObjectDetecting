using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Detector.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection <Image> Images => _database.GetCollection<Image>("Images");

        public ImageRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public Task AddAsync(Image image)
            => Images.InsertOneAsync(image);

        public async Task<IEnumerable<Image>> GetAllAsync()
            => await Images.AsQueryable().ToListAsync();

        public async Task<Image> GetAsync(Guid id)
            => await Images.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
            => await Images.DeleteOneAsync(x=> x.Id == id);

        public async Task UpdateAsync(Image image)
            => await Images.ReplaceOneAsync(x=>x.Id == image.Id, image);
    }
}