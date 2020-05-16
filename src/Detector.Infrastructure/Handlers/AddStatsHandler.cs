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
            var stats = _mapper.Map<Feedback>(command);
            await _statsService.AddStatsToImage(command.ImageId, stats);
        }
    }
}