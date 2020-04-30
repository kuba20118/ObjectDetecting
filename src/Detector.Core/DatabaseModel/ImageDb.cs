using System;
using System.Collections.Generic;

namespace Detector.Core.DatabaseModel
{
    public partial class ImageDb
    {
        public Guid Id { get; set; }
        public byte[] ImageBytes { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime? Added { get; set; }
        public int? StatsId { get; set; }

        public virtual StatisticsDb Stats { get; set; }
    }
}
