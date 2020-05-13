using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;

namespace Detector.Infrastructure.Handlers
{
    public class AddImageHandler : ICommandHandler<AddImage>
    {
        private readonly IImageMLService _imageMLService;
        private readonly IImageService _imageService;

        public AddImageHandler(IImageMLService imageMLService, IImageService imageService)
        {
            _imageService = imageService;
            _imageMLService = imageMLService;
        }

        public async Task HandleAsync(AddImage command)
        {
            await _imageMLService.IdentifyObjects(command.ImageOriginal, command.Id);
            //await _imageService.AddImage(command.ImageOriginal);
        }
    }
}