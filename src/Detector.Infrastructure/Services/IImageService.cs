using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Services
{
    public interface IImageService
    {
        Task<ImageDto> GetImage(Guid guid);
        Task<IEnumerable<ImageDto>> GetAll();
        Task AddImage(byte[] image);
    }
}