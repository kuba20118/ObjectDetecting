using System.Threading.Tasks;
using AutoMapper;
using Detector.Core.Domain;
using Detector.Infrastructure.Commands;
using Detector.Infrastructure.Dtos;
using Detector.Infrastructure.Services;

namespace Detector.Infrastructure.Handlers
{
    public class AddStatsHandler : ICommandHandler<AddStats>
    {
        private readonly IStatsService _statsService;
        private readonly IMapper _mapper;

        public AddStatsHandler(IStatsService statsService, IMapper mapper)
        {
            _mapper = mapper;
            _statsService = statsService;
        }
        public async Task HandleAsync(AddStats command)
        {
            var imageStats = await _statsService.GetImageStats(command.ImageId);
            if(imageStats != null)
                return;

            var stats = new Feedback(command.Correct, command.Incorrect, command.NotFound, command.MultipleFound, command.IncorrectBox);
            await _statsService.AddStatsToImage(command.ImageId, stats);
            //await _statsService.UpdateSummaryStats(stats);
        }
    }
}