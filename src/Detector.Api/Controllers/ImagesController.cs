using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Api.Controllers
{
    [ApiController]
    public class ImagesController : ApiControllerBase
    {
        private readonly IImageService _imageService;
        public ImagesController(IImageService imageService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
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
            var image = await _imageService.GetImage(guid);
            return Ok(image);
            //return Created(Get)
        }
    }
}