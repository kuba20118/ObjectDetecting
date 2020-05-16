using System;

namespace Detector.Infrastructure.Commands
{
    public class AddStats : ICommand
    {
        public Guid ImageId { get; set; }
        public int Correct { get; set; }

        public int Incorrect { get; set; }
        public int NotFound { get; set; }
        public int MultipleFound { get; set; }
        public int IncorrectBox { get; set; }
    }
}