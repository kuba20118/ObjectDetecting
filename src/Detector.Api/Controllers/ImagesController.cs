using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ICommandDispatcher _commandDispatcher;
        public ImagesController(IImageService imageService, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddImage command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var image = await _imageService.GetImage(guid);
            return Ok(image);
            //return Created(Get)
        }
    }
}