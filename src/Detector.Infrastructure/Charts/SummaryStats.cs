using System.Collections.Generic;

namespace Detector.Infrastructure.Charts
{
    public class SummaryStats
    {
        public List<ChartData> ChartsData { get; set; }
        public double AverageTime { get; set; }       
        public double Effectiveness { get; set; }   
    }
}