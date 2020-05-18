using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Detector.Core.DatabaseModel;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Database;
using Detector.Infrastructure.Services;
using Detector.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Api.Controllers
{
    [ApiController]
    public class ImagesController : ApiControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IImageMLService _test;
       //private readonly DataContext context;

        public ImagesController(IImageService imageService, ICommandDispatcher commandDispatcher, IImageMLService test) : base(commandDispatcher)
        {
            _imageService = imageService;
            _test = test;
            //this.context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromForm(Name = "File")] IFormFile image)
        {
            var command = new AddImage
            {
                ImageOriginal = image
            };

            await CommandDispatcher.DispatchAsync(command);
            var resultImage = await _imageService.GetImage(command.Id);

            return Ok(resultImage);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var image = await _imageService.GetImage(guid);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var images = await _imageService.GetAll();
            return Ok(images);
        }
    }
}