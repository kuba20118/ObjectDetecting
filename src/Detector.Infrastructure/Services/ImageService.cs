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

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        { 
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task AddImage(Guid id, Result result)
        {
            if (result.imageStringOriginal == null || result.imageStringOriginal.Length == 0)
            {
                throw new Exception("Nieprawidłowy obraz");
            }
            if (result.imageStringProcessed == null || result.imageStringProcessed.Length == 0)
            {
                throw new Exception("Nieprawidłowy obraz");
            }
            var guid = id;

            var newImage = new Image(guid, result.imageStringOriginal, result.imageStringProcessed, result.Description, result.ElapsedTime);

            await _imageRepository.AddAsync(newImage);
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            var images = await _imageRepository.GetAllAsync();
            return images;
            //return _mapper.Map<IEnumerable<ImageDto>>(images);
        }
        public async Task<ImageDto> GetImage(Guid guid)
        {
            var image = await _imageRepository.GetAsync(guid);
            var imageToReturn = _mapper.Map<ImageDto>(image); 

            return imageToReturn;
        }
    }
}