using System;
using System.Collections.Generic;

namespace Detector.Core.DatabaseModel
{
    public partial class StatisticsDb
    {
        public StatisticsDb()
        {
            Image = new HashSet<ImageDb>();
        }

        public int Id { get; set; }
        public string Stat1 { get; set; }
        public string Stat2 { get; set; }
        public string Stat3 { get; set; }

        public virtual ICollection<ImageDb> Image { get; set; }
    }
}
