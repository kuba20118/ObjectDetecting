using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;

        }

        public async Task AddImage(byte[] image)
        {
            if(image == null || image.Length == 0)
            {
                throw new Exception("Nieprawidłowy obraz");
            }
            var guid = Guid.NewGuid(); 
            var newImage = new Image(guid, image);

            await _imageRepository.AddAsync(newImage);
        }

        public async Task<IEnumerable<ImageDto>> GetAll()
        {
            var images = await _imageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ImageDto>>(images);
        }
        public async Task<ImageDto> GetImage(Guid guid)
        {
            var image = await _imageRepository.GetAsync(guid);
            //return _mapper.Map<ImageDto>(image); 
            return new ImageDto
            {
                ImageOriginal = image.ImageOriginal,
                Id = image.Id
            };
        }
    }
}