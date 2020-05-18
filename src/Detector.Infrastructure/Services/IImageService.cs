using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.Services
{
    public interface IImageService : IService
    {
        Task<ImageDto> GetImage(Guid guid);
        Task<IEnumerable<Image>> GetAll();
        Task AddImage(Guid id, Result result);
    }
}