using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using static Detector.Infrastructure.Services.TestService;

namespace Detector.Infrastructure.Services
{
    public interface ITestService : IService
    {
        Task<Result> IdentifyObjects(IFormFile imageFile);
    }
}