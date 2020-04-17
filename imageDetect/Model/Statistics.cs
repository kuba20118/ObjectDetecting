using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageDetect.Model
{
    public class Statistics
    {
        public int ID { get; set; }
        public String ObjectName { get; set; }
        public int NumberOfSearches { get; set; }
        public int ZeroObjects { get; set; }
        public int CorrectFinds { get; set; }
        public double AverageError { get; set; }
        public virtual List <Photos> RelatedPhotos { get; set; }
    }
}
