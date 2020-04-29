using System.Drawing;
using Detector.Infrastructure.Services;
using Detector.ML;

namespace Detector.Infrastructure.Services
{
    public interface IObjectDetectionService : IService
    {
        void DetectObjectsUsingModel(ImageInputData imageInputData);
        Image DrawBoundingBox(string imageFilePath);
    
    }
}