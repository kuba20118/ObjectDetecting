using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;
using Detector.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Api.Controllers
{
    [ApiController]
    public class ImagesController : ApiControllerBase
    {
        private readonly IImageService _imageService;
        private readonly GeneralSettings _settings;
        public ImagesController(IImageService imageService, ICommandDispatcher commandDispatcher, GeneralSettings settings) : base(commandDispatcher)
        {
            _settings = settings;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddImage command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {  
            System.Console.WriteLine(_settings.Name);
            var image = await _imageService.GetImage(guid);
            return Ok(new { image, _settings.Name});
            //return Created(Get)
        }
    }
}