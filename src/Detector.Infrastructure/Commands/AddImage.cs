using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.Commands
{
    public class AddImage : ICommand
    {
        public string Test { get; set; }
        public IFormFile ImageOriginal { get; set; }
    }
}