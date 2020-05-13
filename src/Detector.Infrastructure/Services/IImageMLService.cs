using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using static Detector.Infrastructure.Services.ImageMLService;

namespace Detector.Infrastructure.Services
{
    public interface IImageMLService : IService
    {
        Task IdentifyObjects(IFormFile imageFile, Guid Id);
    }
}