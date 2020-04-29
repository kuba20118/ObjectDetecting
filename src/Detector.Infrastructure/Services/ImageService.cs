using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using Detector.Infrastructure.Dtos;
using Detector.Infrastructure.ImageFileHelpers;
using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly IImageFileWriter _imageWriter;

        public ImageService(IImageRepository imageRepository, IMapper mapper, IImageFileWriter imageWriter)
        {
            _mapper = mapper;
            _imageWriter = imageWriter;
            _imageRepository = imageRepository;
        }

        public async Task AddImage(IFormFile image)
        {

            if (image == null || image.Length == 0)
            {
                throw new Exception("Nieprawidłowy obraz");
            }
            var guid = Guid.NewGuid();

            MemoryStream imageMemoryStream = new MemoryStream();
            await image.CopyToAsync(imageMemoryStream);
            //Check that the image is valid
            byte[] imageData = imageMemoryStream.ToArray();          

            var newImage = new Image(guid, imageData);

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