using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Infrastructure.Dtos;
using Newtonsoft.Json;
using System.Linq;

namespace Detector.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IImageService _imageService;
        private readonly IStatsService _statsService;

        public DataInitializer(IImageService imageService, IStatsService statsService)
        {
            _imageService = imageService;
            _statsService = statsService;
        }

        public async Task SeedAsync()
        {
            var images = await _imageService.GetAll();
            if (images.Count() > 0)
                return;
        }
    }
}