using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;

namespace Detector.Infrastructure.Handlers
{
    public class AddImageHandler : ICommandHandler<AddImage>
    {
        private readonly IImageService _imageService;

        public AddImageHandler(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task HandleAsync(AddImage command)
        {
            await _imageService.AddImage(command.ImageOriginal);
        }
    }
}