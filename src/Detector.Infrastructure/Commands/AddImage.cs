using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.Commands
{
    public class AddImage : ICommand
    {
        public IFormFile ImageOriginal { get; set; }
    }
}