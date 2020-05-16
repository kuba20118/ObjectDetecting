using System;

namespace Detector.Infrastructure.Dtos
{
    public class StatsDto
    {
        public Guid ImageId { get; set; }
        public int Incorrect { get; set; }
        public int NotFound { get; set; }
        public int MultipleFound { get; set; }
        public int IncorrectBox { get; set; }
    }
}