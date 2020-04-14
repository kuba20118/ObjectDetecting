using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageDetect.Model
{
    public class Statistics
    {
        public int NumberOfSearches { get; set; }
        public int ZeroObjects { get; set; }
        public double AverageError { get; set; }
        public virtual ICollection <Photos> RelatedPhotos { get; set; }
    }
}
