using System.Collections.Generic;

namespace Detector.Infrastructure.Dtos
{
    public class Result
    {
        public byte[] imageStringProcessed { get; set; }
        public byte[] imageStringOriginal { get; set; }
        public List<string> Description { get; set; }
        public long ElapsedTime { get; set; }
    }
}