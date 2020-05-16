using System;
using System.Collections.Generic;

namespace Detector.Infrastructure.Charts
{
    public class ChartData
    {
        public string Key { get; set; }
        public string ChartType { get; set; }

        public Tuple<List<string>,List<int>> Data {get;set;}

        // public List<string> Labels { get; set; }
        // public List<int> Values { get; set; }
    }
}