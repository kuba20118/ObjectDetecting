using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnnxObjectDetectionWeb.Infrastructure
{
    public interface IImageFileWriter
    {
        Task<string> UploadImageAsync(IFormFile file, string imagesTempFolder);
        void DeleteImageTempFile(string filePathName);
    }
}