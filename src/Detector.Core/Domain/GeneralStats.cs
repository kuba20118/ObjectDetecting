using System;
using System.Collections.Generic;

namespace Detector.Core.Domain
{
    public class GeneralStats
    {
        //public double AverageTime { get; set; }
        public Guid Id { get; set; }
        public string Key { get; set; } = "GeneralStats";
        public int Detections { get; set; }
        public int ObjectsFoundByML { get; set; }
        public int CriticalMistakes { get; set; }
        public int SmallMistakes { get; set; }
        public int AllMistakes { get; set; }
        public int CorrectObjectsDetections { get; set; }
        public int IncorrectObjectsDetections { get; set; }
        public int NotFoundObjects { get; set; }
        public int MultipleObjectsDetections { get; set; }
        public int IncorrectBoxDetections { get; set; }     
        public long Time { get; set; }
        public long AverageTime { get; set; }


        public GeneralStats()
        {
            Id = Guid.NewGuid();
        }
    }

}