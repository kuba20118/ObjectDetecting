using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;
using Detector.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnnxObjectDetectionWeb.Infrastructure;
using OnnxObjectDetectionWeb.Services;

namespace Detector.Api.Controllers
{
    [ApiController]
    public class ImagesController : ApiControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ITestService _test;

        public ImagesController(IImageService imageService, ICommandDispatcher commandDispatcher, ITestService test) : base(commandDispatcher)
        {
            _imageService = imageService;
            _test = test;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromForm(Name = "File")] IFormFile command)
        {
            //await CommandDispatcher.DispatchAsync(command);
            return Ok( await _test.IdentifyObjects(command));
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {  
            //var image = await _imageService.GetImage(guid);
            return Ok();
        }
    }
}