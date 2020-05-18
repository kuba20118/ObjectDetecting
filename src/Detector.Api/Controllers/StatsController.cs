using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ApiControllerBase
    {
        private readonly IStatsService _statsService;
        public StatsController(ICommandDispatcher commandDispatcher, IStatsService statsService) : base(commandDispatcher)
        {
            _statsService = statsService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(AddStats command)
        {
            await CommandDispatcher.DispatchAsync(command);
            var statsToReturn = await _statsService.GetImageStats(command.ImageId);
            return Ok(statsToReturn);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Get()
        {
            var statsToReturn = await _statsService.GetSummaryStats();
            return Ok(statsToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var statsToReturn = await _statsService.GetAll();
            return Ok(statsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var imageStatsToReturn = await _statsService.GetImageStats(id);
            return Ok(imageStatsToReturn);
        }
    }
}