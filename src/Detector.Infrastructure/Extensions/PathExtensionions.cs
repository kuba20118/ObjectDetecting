using Detector.Infrastructure.Services;
using System.IO;

namespace Detector.Infrastructure.Extensions
{
    public static class PathExtensionions
    {
        public static string GetAbsolutePath(string relativePath)
        {            
            FileInfo _dataRoot = new FileInfo(typeof(ImageMLService).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);
            return fullPath;
        }
    }
}
