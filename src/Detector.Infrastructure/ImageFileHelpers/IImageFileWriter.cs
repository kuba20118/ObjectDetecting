using System.Threading.Tasks;
using Detector.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.ImageFileHelpers
{
    public interface IImageFileWriter : IService
    {
        Task<string> UploadImageAsync(IFormFile file, string imagesTempFolder);
        void DeleteImageTempFile(string filePathName);
    }
}