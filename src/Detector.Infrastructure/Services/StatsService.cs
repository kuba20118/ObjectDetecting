using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Dtos;
using System.Collections.Generic;
using AutoMapper;
using Detector.Core.Repositories;
using Detector.Core.Domain;

namespace Detector.Infrastructure.Services
{
    public class StatsService : IStatsService
    {
        private readonly IMapper _mapper;
        private readonly IStatsRepository _statsRepository;
        private readonly IImageRepository _imageRepository;

        public StatsService(IMapper mapper, IStatsRepository statsRepo, IImageRepository imageRepository)
        {
            _statsRepository = statsRepo;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        public async Task AddStatsToImage(Guid id, Feedback stats)
        {
            var image = await _imageRepository.GetAsync(id);
            if (image == null)
                throw new Exception("Nie znaleziono odpowiedniego zdjęcia");
                
            var statistics = new Statistics(id,image.Description,stats);
            await _statsRepository.AddAsync(statistics);
        }

        public async Task<StatsDto> GetImageStats(Guid id)
        {
            var stats = await _statsRepository.GetAsync(id);
            var temp = new StatsDto();
            return temp;
        }

        public async Task<IEnumerable<StatsDto>> GetAllStats()
        {
            throw new NotImplementedException();
        }
    }
}