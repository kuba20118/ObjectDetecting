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
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddImage command)
        {
            await _imageService.AddImage(command.ImageOriginal);
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