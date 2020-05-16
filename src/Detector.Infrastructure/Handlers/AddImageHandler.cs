using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;

namespace Detector.Infrastructure.Handlers
{
    public class AddImageHandler : ICommandHandler<AddImage>
    {
        private readonly IImageMLService _imageMLService;

        public AddImageHandler(IImageMLService imageMLService)
        {
            _imageMLService = imageMLService;
        }

        public async Task HandleAsync(AddImage command)
        {
            await _imageMLService.IdentifyObjects(command.ImageOriginal, command.Id);
        }
    }
}