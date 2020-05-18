using System.Collections.Generic;

namespace Detector.Core.Domain
{
    public class GeneralStats
    {
        //public double AverageTime { get; set; }
        public int FoundByML { get; set; }
        public int CriticalMistakes { get; set; }
        public int SmallMistakes { get; set; }
        public int AllMistakes { get; set; }
        public int CorrectObjectsDetections { get; set; }
        public int IncorrectObjectsDetections { get; set; }
        public int NotFoundObjects { get; set; }
        public int MultipleObjectsDetections { get; set; }
    }

}