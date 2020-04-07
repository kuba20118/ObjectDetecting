using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageDetect.Model
{
    public class Photos : Value
    {
        public byte[] Photo { get; set; }
        public int FoundNumber { get; set; }
        public int RealNumber { get; set; }
        public int ObjectID { get; set; }
        public virtual Statistics PhotoStatistics { get; set; }
    }
}
