using System;
using System.Collections.Generic;

namespace Detector.Infrastructure.Charts
{
    public class ChartData
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string ChartType { get; set; }

        public Tuple<List<string>,List<int>> Data {get;set;}
    }
}