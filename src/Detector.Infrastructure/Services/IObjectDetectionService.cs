using System.Drawing;
using Detector.Infrastructure.Dtos;
using Detector.Infrastructure.Services;
using Detector.ML;

namespace Detector.Infrastructure.Services
{
    public interface IObjectDetectionService : IService
    {
        void DetectObjectsUsingModel(ImageInputData imageInputData);
        ProcessedImage DrawBoundingBox(string imageFilePath);
    
    }
}